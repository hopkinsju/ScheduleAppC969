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

namespace MakeContact
{
    public partial class MakeContact : Form
    {
        public MakeContact()
        {
            InitializeComponent();
        }

        DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DataLayer.customer cust = new DataLayer.customer();
            cust.customerName = textBoxName.Text;

            Validate(); // validate the input fields                       

            // try to save changes
            try
            {
                dbcontext.SaveChanges(); // write changes to database file
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("FirstName and LastName must contain values",
                   "Entity Validation Exception");
            }


        }

    }
}
