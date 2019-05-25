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
    public partial class DisplayAppointments : Form
    {
        public DisplayAppointments()
        {
            InitializeComponent();
        }

        // Create DbContext so that we can bind to the database
        private DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        private void DisplayAppointments_Load(object sender, EventArgs e)
        {
            dbcontext.appointments
                .OrderBy(appt => appt.start)
                .Load();

            foreach (var appt in dbcontext.appointments.Local) 
            {
                textBoxAppointmentList.AppendText(appt.ToString());
            }
        }

        private void buttonNewAppt_Click(object sender, EventArgs e)
        {
            var newMDIChild = new MakeAppointment.MakeAppointment();
            this.Hide();
            newMDIChild.Show();
        }

        private void buttonContacts_Click(object sender, EventArgs e)
        {
            var newMDIChild = new MakeContact.MakeContact();
            this.Hide();
            newMDIChild.Show();
        }
    }
}
