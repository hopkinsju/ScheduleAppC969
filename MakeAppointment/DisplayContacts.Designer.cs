namespace DisplayContacts
{
    partial class DisplayContacts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelDisplayContacts = new System.Windows.Forms.Label();
            this.textBoxContactList = new System.Windows.Forms.TextBox();
            this.buttonAppointments = new System.Windows.Forms.Button();
            this.buttonNewContact = new System.Windows.Forms.Button();
            this.ContactBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ContactBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDisplayContacts
            // 
            this.labelDisplayContacts.AutoSize = true;
            this.labelDisplayContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDisplayContacts.Location = new System.Drawing.Point(13, 13);
            this.labelDisplayContacts.Name = "labelDisplayContacts";
            this.labelDisplayContacts.Size = new System.Drawing.Size(97, 25);
            this.labelDisplayContacts.TabIndex = 0;
            this.labelDisplayContacts.Text = "Contacts";
            // 
            // textBoxContactList
            // 
            this.textBoxContactList.Location = new System.Drawing.Point(12, 51);
            this.textBoxContactList.Multiline = true;
            this.textBoxContactList.Name = "textBoxContactList";
            this.textBoxContactList.ReadOnly = true;
            this.textBoxContactList.Size = new System.Drawing.Size(776, 319);
            this.textBoxContactList.TabIndex = 1;
            // 
            // buttonAppointments
            // 
            this.buttonAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAppointments.Location = new System.Drawing.Point(12, 376);
            this.buttonAppointments.Name = "buttonAppointments";
            this.buttonAppointments.Size = new System.Drawing.Size(124, 37);
            this.buttonAppointments.TabIndex = 2;
            this.buttonAppointments.Text = "Appointments";
            this.buttonAppointments.UseVisualStyleBackColor = true;
            this.buttonAppointments.Click += new System.EventHandler(this.buttonContacts_Click);
            // 
            // buttonNewContact
            // 
            this.buttonNewContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewContact.Location = new System.Drawing.Point(142, 376);
            this.buttonNewContact.Name = "buttonNewContact";
            this.buttonNewContact.Size = new System.Drawing.Size(124, 37);
            this.buttonNewContact.TabIndex = 3;
            this.buttonNewContact.Text = "New Contact";
            this.buttonNewContact.UseVisualStyleBackColor = true;
            this.buttonNewContact.Click += new System.EventHandler(this.buttonNewContact_Click);
            // 
            // ContactBindingSource
            // 
            this.ContactBindingSource.DataSource = typeof(DataLayer.customer);
            // 
            // DisplayContacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonNewContact);
            this.Controls.Add(this.buttonAppointments);
            this.Controls.Add(this.textBoxContactList);
            this.Controls.Add(this.labelDisplayContacts);
            this.Name = "DisplayContacts";
            this.Text = "Display Contact Table";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DisplayContacts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ContactBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource ContactBindingSource;
        private System.Windows.Forms.Label labelDisplayContacts;
        private System.Windows.Forms.TextBox textBoxContactList;
        private System.Windows.Forms.Button buttonAppointments;
        private System.Windows.Forms.Button buttonNewContact;
    }
}

