namespace DisplayAppointments
{
    partial class DisplayAppointments
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
            this.labelDisplayAppointments = new System.Windows.Forms.Label();
            this.textBoxAppointmentList = new System.Windows.Forms.TextBox();
            this.buttonContacts = new System.Windows.Forms.Button();
            this.buttonNewAppt = new System.Windows.Forms.Button();
            this.appointmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDisplayAppointments
            // 
            this.labelDisplayAppointments.AutoSize = true;
            this.labelDisplayAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDisplayAppointments.Location = new System.Drawing.Point(13, 13);
            this.labelDisplayAppointments.Name = "labelDisplayAppointments";
            this.labelDisplayAppointments.Size = new System.Drawing.Size(143, 25);
            this.labelDisplayAppointments.TabIndex = 0;
            this.labelDisplayAppointments.Text = "Appointments";
            // 
            // textBoxAppointmentList
            // 
            this.textBoxAppointmentList.Location = new System.Drawing.Point(12, 51);
            this.textBoxAppointmentList.Multiline = true;
            this.textBoxAppointmentList.Name = "textBoxAppointmentList";
            this.textBoxAppointmentList.ReadOnly = true;
            this.textBoxAppointmentList.Size = new System.Drawing.Size(776, 319);
            this.textBoxAppointmentList.TabIndex = 1;
            // 
            // buttonContacts
            // 
            this.buttonContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonContacts.Location = new System.Drawing.Point(12, 376);
            this.buttonContacts.Name = "buttonContacts";
            this.buttonContacts.Size = new System.Drawing.Size(102, 37);
            this.buttonContacts.TabIndex = 2;
            this.buttonContacts.Text = "Contacts";
            this.buttonContacts.UseVisualStyleBackColor = true;
            this.buttonContacts.Click += new System.EventHandler(this.buttonContacts_Click);
            // 
            // buttonNewAppt
            // 
            this.buttonNewAppt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewAppt.Location = new System.Drawing.Point(120, 376);
            this.buttonNewAppt.Name = "buttonNewAppt";
            this.buttonNewAppt.Size = new System.Drawing.Size(102, 37);
            this.buttonNewAppt.TabIndex = 3;
            this.buttonNewAppt.Text = "New Appt";
            this.buttonNewAppt.UseVisualStyleBackColor = true;
            this.buttonNewAppt.Click += new System.EventHandler(this.buttonNewAppt_Click);
            // 
            // appointmentBindingSource
            // 
            this.appointmentBindingSource.DataSource = typeof(DataLayer.appointment);
            // 
            // DisplayAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonNewAppt);
            this.Controls.Add(this.buttonContacts);
            this.Controls.Add(this.textBoxAppointmentList);
            this.Controls.Add(this.labelDisplayAppointments);
            this.Name = "DisplayAppointments";
            this.Text = "Display Appointment Table";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DisplayAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource appointmentBindingSource;
        private System.Windows.Forms.Label labelDisplayAppointments;
        private System.Windows.Forms.TextBox textBoxAppointmentList;
        private System.Windows.Forms.Button buttonContacts;
        private System.Windows.Forms.Button buttonNewAppt;
    }
}

