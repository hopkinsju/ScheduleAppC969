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
            showLogin();
        }

        public void showLogin()
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

        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void buttonWeekView_Click(object sender, EventArgs e)
        {

        }

        private void MainScreen_Load(object sender, EventArgs e)
        {

            refreshData();

        }

        public string getCustomerName(int id)
        {
            return dbcontext.users.Find(id).userName;
        }

        private void refreshData()
        {
            // super reliable refresh...
            dbcontext.Dispose();
            dbcontext = new DataLayer.ScheduleEntities();
           
            int range = radioButtonWeekView.Checked ? 7 : 30;
            DateTime rangeStart = DateTime.Now.ToUniversalTime();
            DateTime rangeEnd = DateTime.Now.AddDays(range).ToUniversalTime();

            using (var ctx = new DataLayer.ScheduleEntities())
            {
                var innerJoin = (from appt in dbcontext.appointments
                                 join cust in dbcontext.customers on appt.customerId equals cust.customerId
                                 where appt.end >= rangeStart && appt.start < rangeEnd
                                 orderby appt.start
                                 select new
                                 {
                                     appt.appointmentId,
                                     appt.start,
                                     appt.end,
                                     appt.customerId,
                                     appt.title,
                                     appt.location,
                                     customerName = cust.customerName
                                 }).ToList();

                // Data binding directly to a store query (DbSet, DbQuery, DbSqlQuery, DbRawSqlQuery) is not supported. Instead populate a DbSet with data, for example by calling Load on the DbSet, and then bind to local data. For WPF bind to DbSet.Local. For WinForms bind to DbSet.Local.ToBindingList(). 

                appointmentBindingSource.DataSource = innerJoin;
            }


            dbcontext.appointments
                .Where(appt => appt.end >= rangeStart && appt.start < rangeEnd)
                .OrderBy(appt => appt.start)
                .Load();

 
            //appointmentBindingSource.DataSource = dbcontext.appointments.Local;

            dbcontext.customers
                .OrderBy(cust => cust.customerName)
                .Load();

            customerBindingSource.DataSource = dbcontext.customers.Local;

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

                //dbcontext.Remove(context.Authors.Single(a => a.AuthorId == 1));
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

                //dbcontext.Remove(context.Authors.Single(a => a.AuthorId == 1));
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
            catch (InvalidOperationException)
            {
                MessageBox.Show("You cannot delete a customer who has existing appointments.");
            }
            refreshData();
        }
    }
}
