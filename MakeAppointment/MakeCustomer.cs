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
using System.Text.RegularExpressions;

namespace ScheduleApp
{
    public partial class MakeCustomer : Form
    {
        private MainScreen mainForm;
        private DataLayer.customer cust = new DataLayer.customer();
        private DataLayer.ScheduleEntities dbcontext =
            new DataLayer.ScheduleEntities();
        private DataLayer.address addr = new DataLayer.address();
        private DataLayer.city city = new DataLayer.city();
        private string CurrentUserName;

        public MakeCustomer()
        {
            InitializeComponent();
            this.AutoValidate = AutoValidate.Disable;
            textBoxName.Validating += new CancelEventHandler(textBox_Validating);
        }
        public MakeCustomer(MainScreen mainForm)
            : this()
        {
            CurrentUserName = dbcontext.users.Find(mainForm.CurrentUser).userName;

            this.mainForm = mainForm;
            dbcontext.cities
                .OrderBy(city => city.cityId)
                .Load();

            cityBindingSource.DataSource = dbcontext.cities.Local;

            dbcontext.countries
                .OrderBy(country => country.countryId)
                .Load();

            countryBindingSource.DataSource = dbcontext.countries.Local;

            //if (cust.customerId == 0)
            //{
            //    comboBoxCity.SelectedIndex = -1;
            //    comboBoxCity.Text = "Select a city";
            //    comboBoxCountry.SelectedIndex = -1;
            //    comboBoxCountry.Text = "Select a city";
            //}
            

        }

        public MakeCustomer(MainScreen mainForm, int custId)
            : this(mainForm)
        {
            try
            {
                this.cust = dbcontext.customers
                .Where(c => c.customerId == custId)
                .FirstOrDefault();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("An appointment to modify could not be loaded");
                this.Hide();
            }

            using (var ctx = new DataLayer.ScheduleEntities())
            {
                var addressJoin = (from a in dbcontext.addresses
                                   join c in dbcontext.cities
                                   on a.cityId equals c.cityId
                                   join cn in dbcontext.countries
                                   on c.countryId equals cn.countryId
                                   where a.addressId == cust.addressId
                                   //orderby appt.start
                                   select new
                                   {
                                       a.addressId,
                                       a.address1,
                                       a.address2,
                                       a.phone,
                                       postCode = a.postalCode,
                                       cityIndex = c.cityId,
                                       countryIndex = cn.countryId
                                   }).ToList().First();
                //`customerId`, `customerName`, `addressId`, `active`, `createDate`, `createdBy`, `lastUpdate`, `lastUpdateBy`
                textBoxName.Text = cust.customerName;
                textBoxAddress1.Text = addressJoin.address1;
                textBoxAddress2.Text = addressJoin.address2;
                textBoxPostCode.Text = addressJoin.postCode;
                comboBoxCity.SelectedIndex = addressJoin.cityIndex - 1;
                comboBoxCountry.SelectedIndex = addressJoin.countryIndex - 1;
                textBoxPhone.Text = addressJoin.phone;
                addr.addressId = addressJoin.addressId;
                checkBoxActive.Checked = cust.active;

            }


        }

        private void updateExistingCustomer(DataLayer.customer cust)
        {
            using (var ctx = new DataLayer.ScheduleEntities())
            {
                var customerUpdate = (from cs in ctx.customers
                                      join a in ctx.addresses
                                      on cs.addressId equals a.addressId
                                      join c in ctx.cities
                                      on a.cityId equals c.cityId
                                      join cn in ctx.countries
                                      on c.countryId equals cn.countryId
                                      where cs.customerId == cust.customerId
                                      //orderby appt.start
                                      select new
                                      {
                                          cs,
                                          a,
                                          c,
                                          cn
                                      }).Single();
                //`customerId`, `customerName`, `addressId`, `active`, `createDate`, `createdBy`, `lastUpdate`, `lastUpdateBy`

                customerUpdate.cs.customerName = textBoxName.Text;
                customerUpdate.a.address1 = textBoxAddress1.Text;
                customerUpdate.a.address2 = textBoxAddress2.Text;
                customerUpdate.a.postalCode = textBoxPostCode.Text;
                customerUpdate.a.cityId = comboBoxCity.SelectedIndex + 1;
                customerUpdate.cs.active = checkBoxActive.Checked;
                customerUpdate.a.phone = textBoxPhone.Text;
                customerUpdate.cs.lastUpdate = DateTime.Now.ToUniversalTime();
                customerUpdate.cs.lastUpdateBy = CurrentUserName;
                if (customerUpdate.cs.createdBy is null)
                {
                    customerUpdate.cs.createdBy = CurrentUserName;
                }
                Validate(); // validate the input fields                       
                            //appointmentBindingSource.EndEdit();

                // try to save changes
                try
                {
                    ctx.SaveChanges(); // write changes to database file 
                    this.Close();
                    DialogResult = DialogResult.OK;
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("Something has gone wrong saving this data",
                       "Entity Validation Exception");
                }
            }
        }

        private void createNewCustomer()
        {
            using (var ctx = new DataLayer.ScheduleEntities())
            {
                DataLayer.customer newCust = new DataLayer.customer();
                newCust.customerName = textBoxName.Text;
                newCust.active = checkBoxActive.Checked;
                newCust.addressId = createNewAddress();
                newCust.lastUpdate = DateTime.Now.ToUniversalTime();
                newCust.lastUpdateBy = CurrentUserName;
                newCust.createdBy = CurrentUserName;
                newCust.createDate = DateTime.Now.ToUniversalTime();

                                       
                customerBindingSource.EndEdit();
                // try to save changes
                try
                {
                    ctx.customers.Add(newCust);
                    ctx.SaveChanges(); // write changes to database file 
                    this.Close();
                    DialogResult = DialogResult.OK;
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("Something has gone wrong saving this data",
                       "Entity Validation Exception");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Unable to update the db", e.Message);
                }
                
                
            }
            
        }

        private int createNewAddress()
        {
            using (var ctx = new DataLayer.ScheduleEntities())
            {
                DataLayer.address newAddress = new DataLayer.address();
                newAddress.address1 = textBoxAddress1.Text;
                newAddress.address2 = textBoxAddress2.Text;
                newAddress.cityId = comboBoxCity.SelectedIndex + 1;
                newAddress.createDate = DateTime.Now.ToUniversalTime();
                newAddress.createdBy = CurrentUserName;
                newAddress.postalCode = textBoxPostCode.Text;
                newAddress.lastUpdate = DateTime.Now.ToUniversalTime();
                newAddress.lastUpdateBy = CurrentUserName;
                newAddress.phone = textBoxPhone.Text;
                


                Validate(); // validate the input fields                       
                //addressBindingSource.EndEdit();

                // try to save changes
                try
                {
                    ctx.addresses.Add(newAddress);
                    ctx.SaveChanges(); // write changes to database file 
                    
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("Something has gone wrong saving this data",
                       "Entity Validation Exception");
                }
                return newAddress.addressId;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                DialogResult = DialogResult.None;
                return;
            }

            if (cust.customerId == 0)
            {
                createNewCustomer();
            }
            else
            {
                using (var ctx = new DataLayer.ScheduleEntities())
                {
                    var customerUpdate = (from cs in ctx.customers
                                          join a in ctx.addresses
                                          on cs.addressId equals a.addressId
                                          join c in ctx.cities
                                          on a.cityId equals c.cityId
                                          join cn in ctx.countries
                                          on c.countryId equals cn.countryId
                                          where cs.customerId == cust.customerId
                                          //orderby appt.start
                                          select new
                                          {
                                              cs,
                                              a,
                                              c,
                                              cn
                                          }).Single();
                    //`customerId`, `customerName`, `addressId`, `active`, `createDate`, `createdBy`, `lastUpdate`, `lastUpdateBy`

                    customerUpdate.cs.customerName = textBoxName.Text;
                    customerUpdate.a.address1 = textBoxAddress1.Text;
                    customerUpdate.a.address2 = textBoxAddress2.Text;
                    customerUpdate.a.postalCode = textBoxPostCode.Text;
                    customerUpdate.a.cityId = comboBoxCity.SelectedIndex + 1;
                    customerUpdate.cs.active = checkBoxActive.Checked;
                    customerUpdate.a.phone = textBoxPhone.Text;
                    customerUpdate.cs.lastUpdate = DateTime.Now.ToUniversalTime();
                    customerUpdate.cs.lastUpdateBy = CurrentUserName;
                    if (customerUpdate.cs.createdBy is null)
                    {
                        customerUpdate.cs.createdBy = CurrentUserName;
                    }

                    // try to save changes
                    try
                    {
                        ctx.SaveChanges(); // write changes to database file 
                        this.Close();
                        DialogResult = DialogResult.OK;
                    }
                    catch (DbEntityValidationException)
                    {
                        MessageBox.Show("Something has gone wrong saving this data",
                           "Entity Validation Exception");
                    }
                }
            }
        }

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxCity.SelectedIndex)
            {
                case 0:
                    comboBoxCountry.SelectedIndex = 0;
                    break;
                case 1:
                    comboBoxCountry.SelectedIndex = 1;
                    break;
                case 2:
                    comboBoxCountry.SelectedIndex = 2;
                    break;
                case 3:
                    comboBoxCountry.SelectedIndex = 3;
                    break;
                default:
                    break;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            string msg = String.Empty;
            List<TextBox> boxes = new List<TextBox>(new TextBox[] { textBoxName, textBoxAddress1, textBoxPostCode, textBoxPhone });
            // multiple lambda functions used to operate on a number of elements matching certain criteria - justified by clarity and efficiency
            boxes.Where(a => a.Text == String.Empty)
                .ToList()
                .ForEach(a =>
                {
                    a.BackColor = Color.Pink;
                    e.Cancel = true;
                });
            if (e.Cancel)
            {
                msg = "Please supply values for all highlighted fields." + Environment.NewLine;
            }
           
            numBox_Validating(sender, e, msg);
        }

        private void numBox_Validating(object sender, CancelEventArgs e, string msg)
        {
            bool isValid = true;
            List<TextBox> boxes = new List<TextBox>(new TextBox[] { textBoxPostCode, textBoxPhone });
            Regex pattern = new Regex(@"[^\d-]");
            // Lamdas used to filter and operate on numerous items - justified by clarity and efficiency
            boxes.Where(a => pattern.IsMatch(a.Text))
                .ToList()
                .ForEach(a =>
                {
                    a.BackColor = Color.Yellow;
                    e.Cancel = true;
                    isValid = false;
                });
            if (!isValid)
            {
                msg += "Yellow fields require numeric values";
            }
            if (e.Cancel)
            {
                MessageBox.Show(msg);
            }
        }
    }
}
