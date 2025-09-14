namespace PTMngVSIX.ToolWindow.Forms
{
	partial class OptionForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;

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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupbox_additional_infor = new System.Windows.Forms.GroupBox();
			this.checkbox_selected_text = new System.Windows.Forms.CheckBox();
			this.checkbox_class = new System.Windows.Forms.CheckBox();
			this.checkbox_error = new System.Windows.Forms.CheckBox();
			this.checkbox_fill_in_middle = new System.Windows.Forms.CheckBox();
			this.checkbox_function = new System.Windows.Forms.CheckBox();
			this.checkbox_active_document = new System.Windows.Forms.CheckBox();
			this.checkbox_project_structure = new System.Windows.Forms.CheckBox();
			this.checkbox_solution_structure = new System.Windows.Forms.CheckBox();
			this.groupbox_additional_infor.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(412, 245);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 30);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(493, 245);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 30);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupbox_additional_infor
			// 
			this.groupbox_additional_infor.Controls.Add(this.checkbox_selected_text);
			this.groupbox_additional_infor.Controls.Add(this.checkbox_class);
			this.groupbox_additional_infor.Controls.Add(this.checkbox_error);
			this.groupbox_additional_infor.Controls.Add(this.checkbox_fill_in_middle);
			this.groupbox_additional_infor.Controls.Add(this.checkbox_function);
			this.groupbox_additional_infor.Controls.Add(this.checkbox_active_document);
			this.groupbox_additional_infor.Controls.Add(this.checkbox_project_structure);
			this.groupbox_additional_infor.Controls.Add(this.checkbox_solution_structure);
			this.groupbox_additional_infor.Location = new System.Drawing.Point(12, 12);
			this.groupbox_additional_infor.Name = "groupbox_additional_infor";
			this.groupbox_additional_infor.Size = new System.Drawing.Size(556, 227);
			this.groupbox_additional_infor.TabIndex = 6;
			this.groupbox_additional_infor.TabStop = false;
			this.groupbox_additional_infor.Text = "Additional title";
			// 
			// checkbox_selected_text
			// 
			this.checkbox_selected_text.AutoSize = true;
			this.checkbox_selected_text.Location = new System.Drawing.Point(6, 134);
			this.checkbox_selected_text.Name = "checkbox_selected_text";
			this.checkbox_selected_text.Size = new System.Drawing.Size(143, 17);
			this.checkbox_selected_text.TabIndex = 7;
			this.checkbox_selected_text.Text = "Selected text đang chọn";
			this.checkbox_selected_text.UseVisualStyleBackColor = true;
			// 
			// checkbox_class
			// 
			this.checkbox_class.AutoSize = true;
			this.checkbox_class.Location = new System.Drawing.Point(6, 88);
			this.checkbox_class.Name = "checkbox_class";
			this.checkbox_class.Size = new System.Drawing.Size(106, 17);
			this.checkbox_class.TabIndex = 6;
			this.checkbox_class.Text = "Class đang chọn";
			this.checkbox_class.UseVisualStyleBackColor = true;
			// 
			// checkbox_error
			// 
			this.checkbox_error.AutoSize = true;
			this.checkbox_error.Location = new System.Drawing.Point(302, 19);
			this.checkbox_error.Name = "checkbox_error";
			this.checkbox_error.Size = new System.Drawing.Size(110, 17);
			this.checkbox_error.TabIndex = 5;
			this.checkbox_error.Text = "Thêm thông tin lỗi";
			this.checkbox_error.UseVisualStyleBackColor = true;
			// 
			// checkbox_fill_in_middle
			// 
			this.checkbox_fill_in_middle.AutoSize = true;
			this.checkbox_fill_in_middle.Location = new System.Drawing.Point(6, 157);
			this.checkbox_fill_in_middle.Name = "checkbox_fill_in_middle";
			this.checkbox_fill_in_middle.Size = new System.Drawing.Size(215, 17);
			this.checkbox_fill_in_middle.TabIndex = 4;
			this.checkbox_fill_in_middle.Text = "Hàm đang chọn định dạng Fill In Middle";
			this.checkbox_fill_in_middle.UseVisualStyleBackColor = true;
			// 
			// checkbox_function
			// 
			this.checkbox_function.AutoSize = true;
			this.checkbox_function.Location = new System.Drawing.Point(6, 111);
			this.checkbox_function.Name = "checkbox_function";
			this.checkbox_function.Size = new System.Drawing.Size(103, 17);
			this.checkbox_function.TabIndex = 3;
			this.checkbox_function.Text = "Hàm đang chọn";
			this.checkbox_function.UseVisualStyleBackColor = true;
			// 
			// checkbox_active_document
			// 
			this.checkbox_active_document.AutoSize = true;
			this.checkbox_active_document.Location = new System.Drawing.Point(6, 65);
			this.checkbox_active_document.Name = "checkbox_active_document";
			this.checkbox_active_document.Size = new System.Drawing.Size(139, 17);
			this.checkbox_active_document.TabIndex = 2;
			this.checkbox_active_document.Text = "File Document đang mở";
			this.checkbox_active_document.UseVisualStyleBackColor = true;
			// 
			// checkbox_project_structure
			// 
			this.checkbox_project_structure.AutoSize = true;
			this.checkbox_project_structure.Location = new System.Drawing.Point(6, 42);
			this.checkbox_project_structure.Name = "checkbox_project_structure";
			this.checkbox_project_structure.Size = new System.Drawing.Size(152, 17);
			this.checkbox_project_structure.TabIndex = 1;
			this.checkbox_project_structure.Text = "Cây thư mục dự án hiện tại";
			this.checkbox_project_structure.UseVisualStyleBackColor = true;
			// 
			// checkbox_solution_structure
			// 
			this.checkbox_solution_structure.AutoSize = true;
			this.checkbox_solution_structure.Location = new System.Drawing.Point(6, 19);
			this.checkbox_solution_structure.Name = "checkbox_solution_structure";
			this.checkbox_solution_structure.Size = new System.Drawing.Size(124, 17);
			this.checkbox_solution_structure.TabIndex = 0;
			this.checkbox_solution_structure.Text = "Cây thư mục solution";
			this.checkbox_solution_structure.UseVisualStyleBackColor = true;
			// 
			// OptionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 288);
			this.Controls.Add(this.groupbox_additional_infor);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Option";
			this.groupbox_additional_infor.ResumeLayout(false);
			this.groupbox_additional_infor.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupbox_additional_infor;
		private System.Windows.Forms.CheckBox checkbox_solution_structure;
		private System.Windows.Forms.CheckBox checkbox_active_document;
		private System.Windows.Forms.CheckBox checkbox_project_structure;
		private System.Windows.Forms.CheckBox checkbox_function;
		private System.Windows.Forms.CheckBox checkbox_fill_in_middle;
		private System.Windows.Forms.CheckBox checkbox_error;
		private System.Windows.Forms.CheckBox checkbox_class;
		private System.Windows.Forms.CheckBox checkbox_selected_text;
	}
}