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

namespace DisplayContacts
{
    public partial class DisplayContacts : Form
    {
        public DisplayContacts()
        {
            InitializeComponent();
        }

        // Create DbContext so that we can bind to the database
        private DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        private void DisplayContacts_Load(object sender, EventArgs e)
        {
            dbcontext.customers
                .OrderBy(contact => contact.customerName)
                .Load();

            foreach (var Contact in dbcontext.customers.Local) 
            {
                textBoxContactList.AppendText(Contact.ToString());
            }
        }

        private void buttonNewContact_Click(object sender, EventArgs e)
        {
            var newMDIChild = new MakeContact.MakeContact();
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
