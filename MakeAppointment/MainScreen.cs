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
    public partial class MainScreen : Form
    {
        private int childFormNumber = 0;
        private Form DisplayAppointments = new DisplayAppointments.DisplayAppointments();
        private Form DisplayContacts = new DisplayContacts.DisplayContacts();
        private DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();

        public MainScreen()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void MainScreen_MdiChildActivate(object sender, EventArgs e)
        {
            this.ActiveMdiChild.WindowState =
                FormWindowState.Maximized;
            // Child form always maximized 

            // If child form is new and no has tabPage, 
            // create new tabPage 
            if (this.ActiveMdiChild.Tag == null)
            {
                // Add a tabPage to tabControl with child 
                // form caption 
                TabPage tp = new TabPage(this.ActiveMdiChild
                                         .Text);
                tp.Tag = this.ActiveMdiChild;
                tp.Parent = tabControlMain;
                tabControlMain.SelectedTab = tp;

                this.ActiveMdiChild.Tag = tp;
                //this.ActiveMdiChild.FormClosed +=
                //    new FormClosedEventHandler(
                //                    ActiveMdiChild_FormClosed);
            }

        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ((tabControlMain.SelectedTab != null) &&
            //    (tabControlMain.SelectedTab.Tag != null))
            //    (tabControlMain.SelectedTab.Tag as Form).Select();
        }

        private void buttonWeekView_Click(object sender, EventArgs e)
        {

        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            dbcontext.appointments
                //.OrderBy(appt => appt.start)
                .Load();

            appointmentBindingSource.DataSource = dbcontext.appointments.Local;

            dbcontext.customers
                .OrderBy(cust => cust.customerName)
                .Load();
            customerBindingSource.DataSource = dbcontext.customers.Local;
        }

        private void buttonAddAppt_Click(object sender, EventArgs e)
        {
            Form newApptForm = new MakeAppointment();
            newApptForm.Show();
        }
    }
}
