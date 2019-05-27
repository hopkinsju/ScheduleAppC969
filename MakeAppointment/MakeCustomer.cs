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

            comboBoxCity.SelectedIndex = -1;
            comboBoxCity.Text = "Select a city";
            comboBoxCountry.SelectedIndex = -1;
            comboBoxCountry.Text = "Select a city";

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
                                       cityName = c.city1,
                                       countryName = cn.country1
                                   }).ToList().First();
                //`customerId`, `customerName`, `addressId`, `active`, `createDate`, `createdBy`, `lastUpdate`, `lastUpdateBy`
                textBoxName.Text = cust.customerName;
                textBoxAddress1.Text = addressJoin.address1;
                textBoxAddress2.Text = addressJoin.address2;
                textBoxPostCode.Text = addressJoin.postCode;
                comboBoxCity.SelectedValue = addressJoin.cityName;
                comboBoxCountry.SelectedValue = addressJoin.countryName;
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
                customerUpdate.c.countryId = comboBoxCountry.SelectedIndex + 1;
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
            cust.customerName = textBoxName.Text;
            cust.active = checkBoxActive.Checked;
            cust.addressId = createNewAddress();
            cust.lastUpdate = DateTime.Now.ToUniversalTime();
            cust.lastUpdateBy = CurrentUserName;
            cust.createdBy = CurrentUserName;
            



            Validate(); // validate the input fields                       
            customerBindingSource.EndEdit();

            // try to save changes
            try
            {
                dbcontext.SaveChanges(); // write changes to database file 
                this.Close();
                DialogResult = DialogResult.OK;
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Something has gone wrong saving this data",
                   "Entity Validation Exception");
            }
        }

        private int createNewAddress()
        {
            return 1;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
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
                    customerUpdate.c.countryId = comboBoxCountry.SelectedIndex + 1;
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
    }
}
