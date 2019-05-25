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

namespace MakeAppointment
{
    public partial class MakeAppointment : Form
    {
        public MakeAppointment()
        {
            InitializeComponent();
        }

        DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DataLayer.appointment appt = new DataLayer.appointment();
            appt.title = textBoxTitle.Text;
            appt.description = textBoxDesc.Text;
            appt.customerId = 1;
            appt.contact = textBoxContact.Text;
            appt.type = comboBoxType.Text;
            appt.contact = textBoxContact.Text;
            appt.start = dateTimePickerStart.Value;
            appt.end = dateTimePickerEnd.Value;
            appt.location = "online";
            appt.url = "google.com";
            appt.createdBy = "1";
            appt.lastUpdateBy = "1";
            
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
