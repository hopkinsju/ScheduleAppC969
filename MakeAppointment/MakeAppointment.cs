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
    public partial class MakeAppointment : Form
    {
        private Main mainForm;
        public MakeAppointment(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string CurrentUserName = dbcontext.users.Find(mainForm.CurrentUser).userName;

            DataLayer.appointment appt = new DataLayer.appointment();
            appt.title = textBoxTitle.Text;
            appt.description = textBoxDesc.Text;
            appt.customerId = 1;
            appt.contact = textBoxContact.Text;
            appt.type = comboBoxType.Text;
            appt.contact = textBoxContact.Text;
            appt.start = dateTimePickerStart.Value;
            appt.end = dateTimePickerEnd.Value;
            appt.location = textBoxLocation.Text;
            appt.url = textBoxURL.Text;
            appt.createdBy = CurrentUserName;
            appt.lastUpdateBy = CurrentUserName;


            Validate(); // validate the input fields                       
            appointmentBindingSource.EndEdit();

            // try to save changes
            try {


                dbcontext.appointments.Add(appt);
                dbcontext.SaveChanges(); // write changes to database file 

            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Something has gone wrong saving this data",
                   "Entity Validation Exception");
                throw;
            }

            appointmentBindingSource.ResetBindings(true);
            this.Hide();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
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
