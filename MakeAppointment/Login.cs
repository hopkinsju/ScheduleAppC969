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

        private bool isValidUser = false;
        public int userId = 0;
        public Login()
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
            this.ActiveControl = textBoxUsername;
            


        }

        private DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            // If username checks out, show the main screen
            userId = CheckLogin(username, password);
            if (userId > 0)
            {
                isValidUser = true;
                this.DialogResult = DialogResult.OK;
            }
            // otherwise throw a message and clear the form.
            else
            {
                MessageBox.Show(Properties.Resources.LoginExceptionText,Properties.Resources.LoginFail, MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private int CheckLogin(string username, string password)
        {
            try
            {
                // Get the user and return the ID
                DataLayer.user user = dbcontext.users
                    .Where(u => u.userName == username)
                    .FirstOrDefault();
                if (user.password == password)
                {
                    return user.userId;
                }
                return 0;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!isValidUser)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
