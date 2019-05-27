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
                this.appt = dbcontext.appointments
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string CurrentUserName = dbcontext.users.Find(mainForm.CurrentUser).userName;

            //DataLayer.appointment appt = new DataLayer.appointment();
            //appt.appointmentId = apptId;
            appt.title = textBoxTitle.Text;
            appt.description = textBoxDesc.Text;
            appt.customerId = 1;
            appt.contact = textBoxContact.Text;
            appt.type = comboBoxType.Text;
            appt.contact = textBoxContact.Text;
            appt.start = dateTimePickerStart.Value.ToUniversalTime();
            appt.end = dateTimePickerEnd.Value.ToUniversalTime();
            appt.location = textBoxLocation.Text;
            appt.url = textBoxURL.Text;
            appt.createdBy = CurrentUserName;
            appt.lastUpdateBy = CurrentUserName;
            appt.userId = mainForm.CurrentUser;
            appt.lastUpdate = DateTime.Now.ToUniversalTime();


            Validate(); // validate the input fields                       
            appointmentBindingSource.EndEdit();

            // try to save changes
            try {
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
