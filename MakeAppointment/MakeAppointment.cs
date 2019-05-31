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
using System.Data.Entity.Infrastructure;

namespace ScheduleApp
{
    public partial class MakeAppointment : Form
    {
        private MainScreen mainForm;
        private int apptId;
        private DataLayer.appointment appt = new DataLayer.appointment();
        private DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        public MakeAppointment(MainScreen mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        public MakeAppointment(MainScreen mainForm, int apptId)
            : this(mainForm)
        {
            try
            {
                // Lambda with LINQ query for concise and efficient queries
                appt = dbcontext.appointments
                .Where(a => a.appointmentId == apptId)
                .FirstOrDefault();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("An appointment to modify could not be loaded");
                this.Hide();
            }

            // fill form fields
            textBoxTitle.Text = appt.title;
            textBoxDesc.Text = appt.description;
            comboBoxCustomer.SelectedValue = appt.customerId;
            textBoxContact.Text = appt.contact;
            comboBoxType.SelectedItem = appt.type;
            dateTimePickerStart.Value = appt.start.ToLocalTime();
            dateTimePickerEnd.Value = appt.end.ToLocalTime();
            textBoxLocation.Text = appt.location;
            textBoxURL.Text = appt.url;
            this.apptId = appt.appointmentId;

        }

        [Serializable]
        class WorkingTooHardException : Exception
        {
            public WorkingTooHardException()
            {

            }

            public WorkingTooHardException(string msg)
                : base(String.Format("{0}", msg))
            {

            }

        }
        [Serializable]
        class TimeTravelImpossibleException : Exception
        {
            public TimeTravelImpossibleException()
            {

            }

            public TimeTravelImpossibleException(string msg)
                : base(String.Format("{0}", msg))
            {

            }

        }

        private void ValidateAppt(DataLayer.appointment appt)
        {
            using (var ctx = new DataLayer.ScheduleEntities())
            {
                string CurrentUserName = dbcontext.users.Find(mainForm.CurrentUser).userName;
                // Lambda with LINQ query for concise and efficient queries
                bool overlapping = ctx.appointments
                    .Where(a =>
                    a.createdBy == CurrentUserName && (a.start > appt.start && a.start < appt.end)
                    || (a.end < appt.end && a.start > appt.start)
                    )
                    .Any();
                if (overlapping)
                {
                    throw new WorkingTooHardException("Appointment overlaps with existing appointment");
                }
                if (
                    appt.start.ToLocalTime().Hour < 9 ||
                    appt.start.ToLocalTime().Hour > 17 ||
                    appt.start.ToLocalTime().Day != appt.end.ToLocalTime().Day
                    )
                {
                    throw new WorkingTooHardException("Appointments must be within business hours");
                }
                if (appt.start.ToLocalTime() > appt.end.ToLocalTime())
                {
                    throw new TimeTravelImpossibleException("Appointment end cannot be before appointment start");
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                DialogResult = DialogResult.None;
                return;
            }
            string CurrentUserName = dbcontext.users.Find(mainForm.CurrentUser).userName;


            //DataLayer.appointment appt = new DataLayer.appointment();
            
            appt.title = textBoxTitle.Text;
            appt.description = textBoxDesc.Text;
            appt.customerId = int.Parse(comboBoxCustomer.SelectedValue.ToString());
            appt.contact = textBoxContact.Text;
            appt.type = comboBoxType.Text;
            appt.contact = textBoxContact.Text;
            appt.start = dateTimePickerStart.Value.ToUniversalTime();
            appt.end = dateTimePickerEnd.Value.ToUniversalTime();
            appt.location = textBoxLocation.Text;
            appt.url = textBoxURL.Text;
            appt.createdBy = CurrentUserName;
            appt.createDate = DateTime.Now.ToUniversalTime();
            appt.lastUpdateBy = CurrentUserName;
            appt.userId = mainForm.CurrentUser;
            appt.lastUpdate = DateTime.Now.ToUniversalTime();


            try
            {
                ValidateAppt(appt); // validate the input fields   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Your appointment was not saved: {ex.Message}");
                return;
            }
                               
            appointmentBindingSource.EndEdit();

            // try to save changes
            try {
                if (apptId == 0)
                {
                    dbcontext.appointments.Add(appt);
                }
                
                dbcontext.SaveChanges(); // write changes to database file 
                this.Close();
                DialogResult = DialogResult.OK;
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Something has gone wrong saving this data",
                   "Entity Validation Exception");
            }

            

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MakeAppointment_Load(object sender, EventArgs e)
        {
            dbcontext.customers
                .OrderBy(cust => cust.customerName)
                .Load();
            customerBindingSource.DataSource = dbcontext.customers.Local;
        }
    }
}
