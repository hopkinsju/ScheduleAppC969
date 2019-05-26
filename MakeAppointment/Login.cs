using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace ScheduleApp
{
    public partial class Login : Form
    {
        private Main mainForm;
        public Login(Main mainForm)
        {
            InitializeComponent();
            string region = RegionInfo.CurrentRegion.TwoLetterISORegionName;
            if (region != "US")
            {
                Properties.Resources.Culture = new CultureInfo("es-MX");
            }


            labelAppTitle.Text = Properties.Resources.AppTitleText;
            labelUsername.Text = Properties.Resources.UsernameText;
            labelPassword.Text = Properties.Resources.PasswordText;
            buttonLogin.Text = Properties.Resources.LoginButtonText;

            this.mainForm = mainForm;


        }

        private DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
 
            // If username checks out, show the main screen
            if (CheckLogin(username, password))
            {
                this.DialogResult = DialogResult.OK;
                mainForm.CurrentUser = mainForm.GetUserIDByUsername(username);
                this.Close();
            }
            // otherwise throw a message and clear the form.
            else
            {
            MessageBox.Show(Properties.Resources.LoginExceptionText);

                Action<Control.ControlCollection> clear = null;

                clear = (controls) =>
                {
                    foreach (Control control in controls)
                        if (control is TextBox)
                            (control as TextBox).Clear();
                        else
                            clear(control.Controls);
                };

                clear(Controls);
                    
            }

        }

        private bool CheckLogin(string username, string password)
        {
            try
            {
                // .Any returns true if any database rows match the where clause
                return dbcontext.users
                    .Where(u => u.userName == username && u.password == password)
                    .Any();
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(Properties.Resources.LoginExceptionText, e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
