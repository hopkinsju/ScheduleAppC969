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
using System.IO;

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
            // Second location supported is MX - to make testing easier
            // select MX if not US. Setting the Region and Regional Format will
            // trigger this.
            if (region == "MX")
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
            // record login activity
            string logfilePath = @"auth_log.txt";
            if (!File.Exists(logfilePath))
            {
                // Create a file to write to.
                using (StreamWriter logfile = File.CreateText(logfilePath))
                {
                    logfile.WriteLine("Log of user authentication activitity:");
                }
            }
            using (StreamWriter logfile = File.AppendText(logfilePath))
            {
                try
                {
                    // Get the user and return the ID
                    DataLayer.user user = dbcontext.users
                        .Where(u => u.userName == username)
                        .FirstOrDefault();
                    if (user.password == password)
                    {
                        logfile.WriteLine($"{DateTime.Now.ToUniversalTime()}(UTC) user: {username}, result: success");
                        return user.userId;
                    }
                    logfile.WriteLine($"{DateTime.Now.ToUniversalTime()}(UTC) user: {username}, result: failure");
                    return 0;
                }
                catch (NullReferenceException)
                {
                    logfile.WriteLine($"{DateTime.Now.ToUniversalTime()}(UTC) user: {username}, result: error");
                    return 0;
                }
                catch
                {
                    logfile.WriteLine($"{DateTime.Now.ToUniversalTime()}(UTC) user: {username} result: error");
                    MessageBox.Show("Unable to verify login, possibly due to network failure.");
                    return 0;
                }
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
