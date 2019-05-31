using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;

namespace ScheduleApp
{
    public partial class MainScreen : Form
    {
        private DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();
        private int currentUser;

        public int CurrentUser
        {
            get { return currentUser; }
            set {
                if (currentUser == 0)
                {
                    this.currentUser = value;
                }
                else {
                    throw new InvalidOperationException("Value can be set only once");
                }
            }

        }

        public MainScreen()
        {
            InitializeComponent();
            ShowLogin();
            CheckForNotify();
            
        }

        public void ShowLogin()
        {
            using (Login login = new Login())
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    Environment.Exit(0);
                }
                CurrentUser = login.userId;
            }
        }

        public int GetUserIDByUsername(string username)
        {
            var UserID = 
                (
                    from user in dbcontext.users
                    where user.userName == username
                    select user.userId
                )
                .FirstOrDefault();

            return UserID; 
        }

        public string GetUsernameByUserID(int userid)
        {
            var UserName =
                (
                    from user in dbcontext.users
                    where user.userId == userid
                    select user.userName
                )
                .FirstOrDefault();

            return UserName;

        }

        private void TabControlMain_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void ButtonWeekView_Click(object sender, EventArgs e)
        {

        }

        private void MainScreen_Load(object sender, EventArgs e)
        {

            refreshData();

        }

        public string GetCustomerName(int id)
        {
            return dbcontext.users.Find(id).userName;
        }

        public string FormatAddressString(int addressId)
        {
            string addressString;
            using (var ctx = new DataLayer.ScheduleEntities())
            {
                var addressJoin = (from a in dbcontext.addresses
                                   join c in dbcontext.cities
                                   on a.cityId equals c.cityId
                                   join cn in dbcontext.countries
                                   on c.countryId equals cn.countryId
                                   where a.addressId == addressId
                                   //orderby appt.start
                                   select new
                                   {
                                       a.address1,
                                       a.address2,
                                       postCode = a.postalCode,
                                       cityName = c.city1,
                                       countryName = cn.country1
                                   }).ToList().First();
                addressString = $"{addressJoin.address1} {addressJoin.address2} {addressJoin.cityName}, {addressJoin.countryName}";
            }
            return addressString;
        }

        public void CheckForNotify() 
        {
            using (var ctx = new DataLayer.ScheduleEntities())
            {
                DateTime notifyStart = DateTime.Now.ToUniversalTime();
                DateTime notifyEnd = DateTime.Now.ToUniversalTime().AddMinutes(15);
                string notificationMessage = string.Empty;
                var upcomingAppts = (from a in ctx.appointments
                                     join c in ctx.customers
                                     on a.customerId equals c.customerId
                                     where a.start >= notifyStart
                                     && a.start <= notifyEnd
                                     select new {
                                         a, c
                                     }).ToList();
                if (upcomingAppts.Count() > 0)
                {
                    // lambda function justitified effeciently produces formatted message output
                    upcomingAppts.ForEach(
                        appt => 
                        notificationMessage += $"{appt.a.start.ToLocalTime()}: " +
                        $"{appt.a.title} - " +
                        $"{appt.c.customerName}" +
                        $"{Environment.NewLine}"
                        );
                    MessageBox.Show("Meetings beginning in less than 15 minutes:" + Environment.NewLine + notificationMessage);
                }
            }
            

        }

        private void refreshData()
        {
            // super reliable refresh...
            dbcontext.Dispose();
            dbcontext = new DataLayer.ScheduleEntities();
           
            int range = radioButtonWeekView.Checked ? 7 : 30;
            DateTime rangeStart = DateTime.Now.ToUniversalTime();
            DateTime rangeEnd = DateTime.Now.ToUniversalTime().AddDays(range);

            using (var ctx = new DataLayer.ScheduleEntities())
            {
                string currentUserName = GetUsernameByUserID(CurrentUser);

                var innerJoin = (from appt in dbcontext.appointments
                                 join cust in dbcontext.customers on appt.customerId equals cust.customerId
                                 where appt.end >= rangeStart && appt.start < rangeEnd && appt.createdBy == currentUserName
                                 orderby appt.start
                                 select new
                                 {
                                     appt.appointmentId,
                                     appt.start,
                                     appt.end,
                                     appt.customerId,
                                     appt.title,
                                     appt.location,
                                     cust.customerName
                                 }).ToList();

                appointmentBindingSource.DataSource = innerJoin;
            }

            dbcontext.customers
                .OrderBy(cust => cust.customerName)
                .Load();

            customerBindingSource.DataSource = dbcontext.customers.Local;

            // Show local and UTC time - useful for troubleshooting
            //labelTime.Text = $"Current Local Time: {DateTime.Now.ToLocalTime()}{Environment.NewLine}Current UTC Time: {DateTime.Now.ToUniversalTime()}";

            RefreshReports();
        }

        private void RefreshReports()
        {
            RefreshMeetingsByType();
            RefreshConsultantSchedule();
            RefreshCustomerSchedule();
        }

        private void RefreshMeetingsByType()
        {
            textBoxMeetingsByType.Clear();
            string output = string.Empty;
            using (var ctx = new DataLayer.ScheduleEntities())
            {
                for (int i = 1; i <= 12; i++)
                {
                    // get a list of appts for this month
                    var apptsForMonth = ctx.appointments
                        .Where(a => a.start.Month == i)
                        .Select( a => a.type).ToList();

                    // group the list by type and produce a count
                    var grouped = apptsForMonth.GroupBy(
                        type => type.ToString(),
                        (outputs) => new
                        {
                            Key = outputs.ToString(),
                            Count = outputs.Count()
                        });

                    output += Environment.NewLine + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i) + Environment.NewLine;
                    if (apptsForMonth.Count() == 0)
                    {
                        output += $"No meetings scheduled for this month.{Environment.NewLine}";
                    }
                    foreach (var a in grouped)
                    {
                        output += $"{a.Key}: {a.Count()}{Environment.NewLine}";
                    }
                }
            }
            

            

            textBoxMeetingsByType.Text = output;
        }


        private void RefreshCustomerSchedule()
        {
            textBoxCustomerSchedules.Clear();
            using (var ctx = new DataLayer.ScheduleEntities())
            {
                var appointments = from customer in ctx.customers
                                   from appointment in ctx.appointments
                            orderby customer.customerName, appointment.start
                            select new {
                                customer.customerName,
                                customer.customerId,
                                apptCustId = appointment.customerId,
                                appointment.start,
                                appointment.end,
                                appointment.title };

                foreach (var appt in appointments)
                {
                    if (appt.apptCustId == appt.customerId)
                    {
                        textBoxCustomerSchedules.AppendText($"{appt.customerName}: {appt.start.ToLocalTime()} - {appt.end.ToLocalTime()} {appt.title}{Environment.NewLine}");

                    }
                }

            }

        }

        private void RefreshConsultantSchedule()
        {
            textBoxConsultantSchedules.Clear();
            using (var ctx = new DataLayer.ScheduleEntities())
            {
                var appointments = from user in ctx.users
                                   from appointment in ctx.appointments
                                   orderby user.userName, appointment.start
                                   select new
                                   {
                                       user.userName,
                                       user.userId,
                                       apptUserId = appointment.userId,
                                       appointment.start,
                                       appointment.end,
                                       appointment.title
                                   };

                foreach (var appt in appointments)
                {
                    if (appt.apptUserId == appt.userId)
                    {
                        textBoxConsultantSchedules.AppendText($"{appt.userName}: {appt.start.ToLocalTime()} - {appt.end.ToLocalTime()} {appt.title}{Environment.NewLine}");

                    }
                }

            }
        }

        private void buttonAddAppt_Click(object sender, EventArgs e)
        {
            bool refresh = false;
            using (MakeAppointment newApptForm = new MakeAppointment(this))
            {
                if (newApptForm.ShowDialog() == DialogResult.OK)
                {
                    refresh = true;
                }
            }
            if (refresh)
            {
                refreshData();
            }
        }

        private void buttonModifyAppt_Click(object sender, EventArgs e)
        {
            bool refresh = false;
            DataGridViewRow row = appointmentDataGridView.SelectedRows[0];
            int apptId = Convert.ToInt32(row.Cells["appointmentId"].Value);
            
            using (MakeAppointment modifyApptForm = new MakeAppointment(this, apptId))
            {
                if (modifyApptForm.ShowDialog() == DialogResult.OK)
                {
                    refresh = true;
                }
            }
            if (refresh)
            {
                refreshData();
            }
        }

        private void buttonRemoveAppt_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = appointmentDataGridView.SelectedRows[0];
            int apptId = Convert.ToInt32(row.Cells["appointmentId"].Value);

            try
            {

                var appt = dbcontext.appointments
                    .Where(a => a.appointmentId == apptId)
                    .FirstOrDefault();

                dbcontext.appointments.Remove(appt);

                dbcontext.SaveChanges();

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No row was selected, or an error occurred removing it.");
            }
            refreshData();
        }

        private void radioButtonWeekView_CheckedChanged(object sender, EventArgs e)
        {
            refreshData();
        }

        private void buttonAddCust_Click(object sender, EventArgs e)
        {
            bool refresh = false;
            using (MakeCustomer newCustForm = new MakeCustomer(this))
            {
                if (newCustForm.ShowDialog() == DialogResult.OK)
                {
                    refresh = true;
                }
            }
            if (refresh)
            {
                refreshData();
            }
        }

        private void buttonModifyCust_Click(object sender, EventArgs e)
        {
            bool refresh = false;
            DataGridViewRow row = customerDataGridView.SelectedRows[0];
            int customerId = Convert.ToInt32(row.Cells["custDgvCustId"].Value);

            using (MakeCustomer modifyCustomerForm = new MakeCustomer(this, customerId))
            {
                if (modifyCustomerForm.ShowDialog() == DialogResult.OK)
                {
                    refresh = true;
                }
            }
            if (refresh)
            {
                refreshData();
            }
        }

        private void buttonRemoveCust_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = customerDataGridView.SelectedRows[0];
            int customerId = Convert.ToInt32(row.Cells["custDgvCustId"].Value);

            try
            {

                var customer = dbcontext.customers
                    .Where(c => c.customerId == customerId)
                    .FirstOrDefault();

                dbcontext.customers.Remove(customer);

                dbcontext.SaveChanges();

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No row was selected, or an error occurred removing it.");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show("You cannot delete a customer who has existing appointments.");
            }
            catch
            {
                MessageBox.Show("Unknown error occurred saving to the database.");
            }
            refreshData();
        }

        private void appointmentDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DateTime)
            {
                DateTime value = (DateTime)e.Value;
                switch (value.Kind)
                {
                    case DateTimeKind.Local:
                        break;
                    case DateTimeKind.Unspecified:
                        e.Value = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToLocalTime();
                        break;
                    case DateTimeKind.Utc:
                        e.Value = value.ToLocalTime();
                        break;
                }
            }
        }

        private void customerDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is DataLayer.address)
            {
                DataLayer.address thisAddress = (DataLayer.address)e.Value;
                e.Value = FormatAddressString(thisAddress.addressId);
            }
        }
    }
}
