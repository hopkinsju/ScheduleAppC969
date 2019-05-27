namespace ScheduleApp
{
    partial class MainScreen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabAppointments = new System.Windows.Forms.TabPage();
            this.radioButtonMonthView = new System.Windows.Forms.RadioButton();
            this.radioButtonWeekView = new System.Windows.Forms.RadioButton();
            this.buttonRemoveAppt = new System.Windows.Forms.Button();
            this.buttonModifyAppt = new System.Windows.Forms.Button();
            this.buttonAddAppt = new System.Windows.Forms.Button();
            this.appointmentDataGridView = new System.Windows.Forms.DataGridView();
            this.appointmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabCustomers = new System.Windows.Forms.TabPage();
            this.buttonRemoveCust = new System.Windows.Forms.Button();
            this.buttonModifyCust = new System.Windows.Forms.Button();
            this.buttonAddCust = new System.Windows.Forms.Button();
            this.customerDataGridView = new System.Windows.Forms.DataGridView();
            this.customerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appointmentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.addressId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.custDgvCustId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource)).BeginInit();
            this.tabCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customerDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 469);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(911, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabAppointments);
            this.tabControlMain.Controls.Add(this.tabCustomers);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(911, 469);
            this.tabControlMain.TabIndex = 4;
            this.tabControlMain.Tag = "DisplayCustomers";
            // 
            // tabAppointments
            // 
            this.tabAppointments.BackColor = System.Drawing.Color.DarkGray;
            this.tabAppointments.Controls.Add(this.radioButtonMonthView);
            this.tabAppointments.Controls.Add(this.radioButtonWeekView);
            this.tabAppointments.Controls.Add(this.buttonRemoveAppt);
            this.tabAppointments.Controls.Add(this.buttonModifyAppt);
            this.tabAppointments.Controls.Add(this.buttonAddAppt);
            this.tabAppointments.Controls.Add(this.appointmentDataGridView);
            this.tabAppointments.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabAppointments.Location = new System.Drawing.Point(4, 33);
            this.tabAppointments.Name = "tabAppointments";
            this.tabAppointments.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppointments.Size = new System.Drawing.Size(903, 432);
            this.tabAppointments.TabIndex = 0;
            this.tabAppointments.Tag = "DisplayAppointments";
            this.tabAppointments.Text = "Appointments";
            // 
            // radioButtonMonthView
            // 
            this.radioButtonMonthView.AutoSize = true;
            this.radioButtonMonthView.Location = new System.Drawing.Point(764, 391);
            this.radioButtonMonthView.Name = "radioButtonMonthView";
            this.radioButtonMonthView.Size = new System.Drawing.Size(128, 28);
            this.radioButtonMonthView.TabIndex = 5;
            this.radioButtonMonthView.Text = "Month View";
            this.radioButtonMonthView.UseVisualStyleBackColor = true;
            // 
            // radioButtonWeekView
            // 
            this.radioButtonWeekView.AutoSize = true;
            this.radioButtonWeekView.Checked = true;
            this.radioButtonWeekView.Location = new System.Drawing.Point(609, 391);
            this.radioButtonWeekView.Name = "radioButtonWeekView";
            this.radioButtonWeekView.Size = new System.Drawing.Size(124, 28);
            this.radioButtonWeekView.TabIndex = 4;
            this.radioButtonWeekView.TabStop = true;
            this.radioButtonWeekView.Text = "Week View";
            this.radioButtonWeekView.UseVisualStyleBackColor = true;
            this.radioButtonWeekView.CheckedChanged += new System.EventHandler(this.radioButtonWeekView_CheckedChanged);
            // 
            // buttonRemoveAppt
            // 
            this.buttonRemoveAppt.Location = new System.Drawing.Point(297, 386);
            this.buttonRemoveAppt.Name = "buttonRemoveAppt";
            this.buttonRemoveAppt.Size = new System.Drawing.Size(138, 39);
            this.buttonRemoveAppt.TabIndex = 3;
            this.buttonRemoveAppt.Text = "Remove";
            this.buttonRemoveAppt.UseVisualStyleBackColor = true;
            this.buttonRemoveAppt.Click += new System.EventHandler(this.buttonRemoveAppt_Click);
            // 
            // buttonModifyAppt
            // 
            this.buttonModifyAppt.Location = new System.Drawing.Point(153, 387);
            this.buttonModifyAppt.Name = "buttonModifyAppt";
            this.buttonModifyAppt.Size = new System.Drawing.Size(138, 39);
            this.buttonModifyAppt.TabIndex = 2;
            this.buttonModifyAppt.Text = "Modify";
            this.buttonModifyAppt.UseVisualStyleBackColor = true;
            this.buttonModifyAppt.Click += new System.EventHandler(this.buttonModifyAppt_Click);
            // 
            // buttonAddAppt
            // 
            this.buttonAddAppt.Location = new System.Drawing.Point(9, 387);
            this.buttonAddAppt.Name = "buttonAddAppt";
            this.buttonAddAppt.Size = new System.Drawing.Size(138, 39);
            this.buttonAddAppt.TabIndex = 1;
            this.buttonAddAppt.Text = "Add";
            this.buttonAddAppt.UseVisualStyleBackColor = true;
            this.buttonAddAppt.Click += new System.EventHandler(this.buttonAddAppt_Click);
            // 
            // appointmentDataGridView
            // 
            this.appointmentDataGridView.AllowUserToAddRows = false;
            this.appointmentDataGridView.AllowUserToDeleteRows = false;
            this.appointmentDataGridView.AutoGenerateColumns = false;
            this.appointmentDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.appointmentDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.appointmentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.appointmentDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn8,
            this.appointmentId,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn3,
            this.customerId,
            this.customerName,
            this.dataGridViewTextBoxColumn5});
            this.appointmentDataGridView.DataSource = this.appointmentBindingSource;
            this.appointmentDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.appointmentDataGridView.Location = new System.Drawing.Point(3, 3);
            this.appointmentDataGridView.MultiSelect = false;
            this.appointmentDataGridView.Name = "appointmentDataGridView";
            this.appointmentDataGridView.ReadOnly = true;
            this.appointmentDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.appointmentDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.appointmentDataGridView.Size = new System.Drawing.Size(897, 377);
            this.appointmentDataGridView.TabIndex = 0;
            this.appointmentDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.appointmentDataGridView_CellFormatting);
            // 
            // appointmentBindingSource
            // 
            this.appointmentBindingSource.DataSource = typeof(DataLayer.appointment);
            // 
            // tabCustomers
            // 
            this.tabCustomers.BackColor = System.Drawing.Color.DarkGray;
            this.tabCustomers.Controls.Add(this.buttonRemoveCust);
            this.tabCustomers.Controls.Add(this.buttonModifyCust);
            this.tabCustomers.Controls.Add(this.buttonAddCust);
            this.tabCustomers.Controls.Add(this.customerDataGridView);
            this.tabCustomers.Location = new System.Drawing.Point(4, 33);
            this.tabCustomers.Name = "tabCustomers";
            this.tabCustomers.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomers.Size = new System.Drawing.Size(903, 432);
            this.tabCustomers.TabIndex = 1;
            this.tabCustomers.Tag = "DisplayCustomers";
            this.tabCustomers.Text = "Customers";
            // 
            // buttonRemoveCust
            // 
            this.buttonRemoveCust.Location = new System.Drawing.Point(296, 380);
            this.buttonRemoveCust.Name = "buttonRemoveCust";
            this.buttonRemoveCust.Size = new System.Drawing.Size(138, 39);
            this.buttonRemoveCust.TabIndex = 6;
            this.buttonRemoveCust.Text = "Remove";
            this.buttonRemoveCust.UseVisualStyleBackColor = true;
            this.buttonRemoveCust.Click += new System.EventHandler(this.buttonRemoveCust_Click);
            // 
            // buttonModifyCust
            // 
            this.buttonModifyCust.Location = new System.Drawing.Point(152, 381);
            this.buttonModifyCust.Name = "buttonModifyCust";
            this.buttonModifyCust.Size = new System.Drawing.Size(138, 39);
            this.buttonModifyCust.TabIndex = 5;
            this.buttonModifyCust.Text = "Modify";
            this.buttonModifyCust.UseVisualStyleBackColor = true;
            this.buttonModifyCust.Click += new System.EventHandler(this.buttonModifyCust_Click);
            // 
            // buttonAddCust
            // 
            this.buttonAddCust.Location = new System.Drawing.Point(8, 381);
            this.buttonAddCust.Name = "buttonAddCust";
            this.buttonAddCust.Size = new System.Drawing.Size(138, 39);
            this.buttonAddCust.TabIndex = 4;
            this.buttonAddCust.Text = "Add";
            this.buttonAddCust.UseVisualStyleBackColor = true;
            this.buttonAddCust.Click += new System.EventHandler(this.buttonAddCust_Click);
            // 
            // customerDataGridView
            // 
            this.customerDataGridView.AllowUserToAddRows = false;
            this.customerDataGridView.AllowUserToDeleteRows = false;
            this.customerDataGridView.AutoGenerateColumns = false;
            this.customerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customerDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.active,
            this.addressId,
            this.dataGridViewTextBoxColumn2,
            this.custDgvCustId,
            this.dataGridViewTextBoxColumn12});
            this.customerDataGridView.DataSource = this.customerBindingSource;
            this.customerDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.customerDataGridView.Location = new System.Drawing.Point(3, 3);
            this.customerDataGridView.MultiSelect = false;
            this.customerDataGridView.Name = "customerDataGridView";
            this.customerDataGridView.ReadOnly = true;
            this.customerDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.customerDataGridView.Size = new System.Drawing.Size(897, 372);
            this.customerDataGridView.TabIndex = 0;
            // 
            // customerBindingSource
            // 
            this.customerBindingSource.DataSource = typeof(DataLayer.customer);
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "start";
            this.dataGridViewTextBoxColumn8.HeaderText = "Start Time";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 119;
            // 
            // appointmentId
            // 
            this.appointmentId.DataPropertyName = "appointmentId";
            this.appointmentId.HeaderText = "AppointmentID";
            this.appointmentId.Name = "appointmentId";
            this.appointmentId.ReadOnly = true;
            this.appointmentId.Visible = false;
            this.appointmentId.Width = 159;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "end";
            this.dataGridViewTextBoxColumn9.HeaderText = "End Time";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 118;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "title";
            this.dataGridViewTextBoxColumn3.HeaderText = "Title";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 70;
            // 
            // customerId
            // 
            this.customerId.DataPropertyName = "customerId";
            this.customerId.HeaderText = "customerId";
            this.customerId.Name = "customerId";
            this.customerId.ReadOnly = true;
            this.customerId.Visible = false;
            this.customerId.Width = 128;
            // 
            // customerName
            // 
            this.customerName.DataPropertyName = "customerName";
            this.customerName.HeaderText = "Customer";
            this.customerName.Name = "customerName";
            this.customerName.ReadOnly = true;
            this.customerName.Width = 116;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "location";
            this.dataGridViewTextBoxColumn5.HeaderText = "Location";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 106;
            // 
            // active
            // 
            this.active.DataPropertyName = "active";
            this.active.HeaderText = "active";
            this.active.Name = "active";
            this.active.ReadOnly = true;
            // 
            // addressId
            // 
            this.addressId.DataPropertyName = "addressId";
            this.addressId.HeaderText = "addressId";
            this.addressId.Name = "addressId";
            this.addressId.ReadOnly = true;
            this.addressId.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "customerName";
            this.dataGridViewTextBoxColumn2.HeaderText = "Customer Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 157;
            // 
            // custDgvCustId
            // 
            this.custDgvCustId.DataPropertyName = "customerId";
            this.custDgvCustId.HeaderText = "Column1";
            this.custDgvCustId.Name = "custDgvCustId";
            this.custDgvCustId.ReadOnly = true;
            this.custDgvCustId.Visible = false;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "address";
            this.dataGridViewTextBoxColumn12.HeaderText = "address";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 491);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainScreen";
            this.Text = "Schedule App Main Screen";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabAppointments.ResumeLayout(false);
            this.tabAppointments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentBindingSource)).EndInit();
            this.tabCustomers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customerDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabAppointments;
        private System.Windows.Forms.TabPage tabCustomers;
        private System.Windows.Forms.DataGridView appointmentDataGridView;
        private System.Windows.Forms.BindingSource appointmentBindingSource;
        private System.Windows.Forms.DataGridView customerDataGridView;
        private System.Windows.Forms.BindingSource customerBindingSource;
        private System.Windows.Forms.Button buttonRemoveAppt;
        private System.Windows.Forms.Button buttonModifyAppt;
        private System.Windows.Forms.Button buttonAddAppt;
        private System.Windows.Forms.Button buttonRemoveCust;
        private System.Windows.Forms.Button buttonModifyCust;
        private System.Windows.Forms.Button buttonAddCust;
        private System.Windows.Forms.RadioButton radioButtonMonthView;
        private System.Windows.Forms.RadioButton radioButtonWeekView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn appointmentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn active;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn custDgvCustId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    }
}



