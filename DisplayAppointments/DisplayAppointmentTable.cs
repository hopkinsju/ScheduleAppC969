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

namespace DisplayAppointments
{
    public partial class DisplayAppointmentTable : Form
    {
        public DisplayAppointmentTable()
        {
            InitializeComponent();
        }

        // Create DbContext so that we can bind to the database
        private DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        private void DisplayAppointmentTable_Load(object sender, EventArgs e)
        {
            dbcontext.appointments
                .OrderBy(appt => appt.start)
                .Load();

            appointmentBindingSource.DataSource = dbcontext.appointments.Local;
        }

        private void appointmentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            appointmentBindingSource.EndEdit();

            try
            {
                dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Entity validate failed", "Entity Validation Exception");
            }
        }
    }
}
