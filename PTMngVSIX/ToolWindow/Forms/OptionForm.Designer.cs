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
			this.label_role = new System.Windows.Forms.Label();
			this.groupbox_model_setting = new System.Windows.Forms.GroupBox();
			this.textbox_env = new System.Windows.Forms.TextBox();
			this.label_env = new System.Windows.Forms.Label();
			this.combobox_output_language = new System.Windows.Forms.ComboBox();
			this.checkbox_translate_output = new System.Windows.Forms.CheckBox();
			this.checkbox_translate_input = new System.Windows.Forms.CheckBox();
			this.textbox_output_format = new System.Windows.Forms.TextBox();
			this.label_output_format = new System.Windows.Forms.Label();
			this.combobox_role = new System.Windows.Forms.ComboBox();
			this.groupbox_additional_infor.SuspendLayout();
			this.groupbox_model_setting.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(391, 424);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 30);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(472, 424);
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
			this.groupbox_additional_infor.Location = new System.Drawing.Point(12, 213);
			this.groupbox_additional_infor.Name = "groupbox_additional_infor";
			this.groupbox_additional_infor.Size = new System.Drawing.Size(535, 198);
			this.groupbox_additional_infor.TabIndex = 6;
			this.groupbox_additional_infor.TabStop = false;
			this.groupbox_additional_infor.Text = "Additional title";
			// 
			// checkbox_selected_text
			// 
			this.checkbox_selected_text.AutoSize = true;
			this.checkbox_selected_text.Location = new System.Drawing.Point(10, 130);
			this.checkbox_selected_text.Name = "checkbox_selected_text";
			this.checkbox_selected_text.Size = new System.Drawing.Size(143, 17);
			this.checkbox_selected_text.TabIndex = 7;
			this.checkbox_selected_text.Text = "Selected text đang chọn";
			this.checkbox_selected_text.UseVisualStyleBackColor = true;
			// 
			// checkbox_class
			// 
			this.checkbox_class.AutoSize = true;
			this.checkbox_class.Location = new System.Drawing.Point(10, 86);
			this.checkbox_class.Name = "checkbox_class";
			this.checkbox_class.Size = new System.Drawing.Size(106, 17);
			this.checkbox_class.TabIndex = 6;
			this.checkbox_class.Text = "Class đang chọn";
			this.checkbox_class.UseVisualStyleBackColor = true;
			// 
			// checkbox_error
			// 
			this.checkbox_error.AutoSize = true;
			this.checkbox_error.Location = new System.Drawing.Point(10, 172);
			this.checkbox_error.Name = "checkbox_error";
			this.checkbox_error.Size = new System.Drawing.Size(110, 17);
			this.checkbox_error.TabIndex = 5;
			this.checkbox_error.Text = "Thêm thông tin lỗi";
			this.checkbox_error.UseVisualStyleBackColor = true;
			// 
			// checkbox_fill_in_middle
			// 
			this.checkbox_fill_in_middle.AutoSize = true;
			this.checkbox_fill_in_middle.Location = new System.Drawing.Point(10, 152);
			this.checkbox_fill_in_middle.Name = "checkbox_fill_in_middle";
			this.checkbox_fill_in_middle.Size = new System.Drawing.Size(215, 17);
			this.checkbox_fill_in_middle.TabIndex = 4;
			this.checkbox_fill_in_middle.Text = "Hàm đang chọn định dạng Fill In Middle";
			this.checkbox_fill_in_middle.UseVisualStyleBackColor = true;
			// 
			// checkbox_function
			// 
			this.checkbox_function.AutoSize = true;
			this.checkbox_function.Location = new System.Drawing.Point(10, 108);
			this.checkbox_function.Name = "checkbox_function";
			this.checkbox_function.Size = new System.Drawing.Size(103, 17);
			this.checkbox_function.TabIndex = 3;
			this.checkbox_function.Text = "Hàm đang chọn";
			this.checkbox_function.UseVisualStyleBackColor = true;
			// 
			// checkbox_active_document
			// 
			this.checkbox_active_document.AutoSize = true;
			this.checkbox_active_document.Location = new System.Drawing.Point(10, 64);
			this.checkbox_active_document.Name = "checkbox_active_document";
			this.checkbox_active_document.Size = new System.Drawing.Size(139, 17);
			this.checkbox_active_document.TabIndex = 2;
			this.checkbox_active_document.Text = "File Document đang mở";
			this.checkbox_active_document.UseVisualStyleBackColor = true;
			// 
			// checkbox_project_structure
			// 
			this.checkbox_project_structure.AutoSize = true;
			this.checkbox_project_structure.Location = new System.Drawing.Point(10, 42);
			this.checkbox_project_structure.Name = "checkbox_project_structure";
			this.checkbox_project_structure.Size = new System.Drawing.Size(152, 17);
			this.checkbox_project_structure.TabIndex = 1;
			this.checkbox_project_structure.Text = "Cây thư mục dự án hiện tại";
			this.checkbox_project_structure.UseVisualStyleBackColor = true;
			// 
			// checkbox_solution_structure
			// 
			this.checkbox_solution_structure.AutoSize = true;
			this.checkbox_solution_structure.Location = new System.Drawing.Point(10, 20);
			this.checkbox_solution_structure.Name = "checkbox_solution_structure";
			this.checkbox_solution_structure.Size = new System.Drawing.Size(124, 17);
			this.checkbox_solution_structure.TabIndex = 0;
			this.checkbox_solution_structure.Text = "Cây thư mục solution";
			this.checkbox_solution_structure.UseVisualStyleBackColor = true;
			// 
			// label_role
			// 
			this.label_role.AutoSize = true;
			this.label_role.Location = new System.Drawing.Point(10, 20);
			this.label_role.Name = "label_role";
			this.label_role.Size = new System.Drawing.Size(52, 13);
			this.label_role.TabIndex = 7;
			this.label_role.Text = "Chọn role";
			// 
			// groupbox_model_setting
			// 
			this.groupbox_model_setting.Controls.Add(this.textbox_env);
			this.groupbox_model_setting.Controls.Add(this.label_env);
			this.groupbox_model_setting.Controls.Add(this.combobox_output_language);
			this.groupbox_model_setting.Controls.Add(this.checkbox_translate_output);
			this.groupbox_model_setting.Controls.Add(this.checkbox_translate_input);
			this.groupbox_model_setting.Controls.Add(this.textbox_output_format);
			this.groupbox_model_setting.Controls.Add(this.label_output_format);
			this.groupbox_model_setting.Controls.Add(this.combobox_role);
			this.groupbox_model_setting.Controls.Add(this.label_role);
			this.groupbox_model_setting.Location = new System.Drawing.Point(12, 12);
			this.groupbox_model_setting.Name = "groupbox_model_setting";
			this.groupbox_model_setting.Size = new System.Drawing.Size(535, 195);
			this.groupbox_model_setting.TabIndex = 8;
			this.groupbox_model_setting.TabStop = false;
			this.groupbox_model_setting.Text = "Model setting";
			// 
			// textbox_env
			// 
			this.textbox_env.Location = new System.Drawing.Point(294, 104);
			this.textbox_env.Multiline = true;
			this.textbox_env.Name = "textbox_env";
			this.textbox_env.Size = new System.Drawing.Size(230, 85);
			this.textbox_env.TabIndex = 15;
			// 
			// label_env
			// 
			this.label_env.AutoSize = true;
			this.label_env.Location = new System.Drawing.Point(291, 88);
			this.label_env.Name = "label_env";
			this.label_env.Size = new System.Drawing.Size(104, 13);
			this.label_env.TabIndex = 14;
			this.label_env.Text = "Môi trường phát triển";
			// 
			// combobox_output_language
			// 
			this.combobox_output_language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combobox_output_language.FormattingEnabled = true;
			this.combobox_output_language.Location = new System.Drawing.Point(294, 64);
			this.combobox_output_language.Name = "combobox_output_language";
			this.combobox_output_language.Size = new System.Drawing.Size(170, 21);
			this.combobox_output_language.TabIndex = 13;
			// 
			// checkbox_translate_output
			// 
			this.checkbox_translate_output.AutoSize = true;
			this.checkbox_translate_output.Location = new System.Drawing.Point(294, 41);
			this.checkbox_translate_output.Name = "checkbox_translate_output";
			this.checkbox_translate_output.Size = new System.Drawing.Size(170, 17);
			this.checkbox_translate_output.TabIndex = 12;
			this.checkbox_translate_output.Text = "Hiển thị output bằng ngôn ngữ";
			this.checkbox_translate_output.UseVisualStyleBackColor = true;
			// 
			// checkbox_translate_input
			// 
			this.checkbox_translate_input.AutoSize = true;
			this.checkbox_translate_input.Location = new System.Drawing.Point(294, 18);
			this.checkbox_translate_input.Name = "checkbox_translate_input";
			this.checkbox_translate_input.Size = new System.Drawing.Size(148, 17);
			this.checkbox_translate_input.TabIndex = 11;
			this.checkbox_translate_input.Text = "Dịch input sang tiếng Anh";
			this.checkbox_translate_input.UseVisualStyleBackColor = true;
			// 
			// textbox_output_format
			// 
			this.textbox_output_format.Location = new System.Drawing.Point(10, 58);
			this.textbox_output_format.Multiline = true;
			this.textbox_output_format.Name = "textbox_output_format";
			this.textbox_output_format.Size = new System.Drawing.Size(250, 131);
			this.textbox_output_format.TabIndex = 9;
			// 
			// label_output_format
			// 
			this.label_output_format.AutoSize = true;
			this.label_output_format.Location = new System.Drawing.Point(10, 42);
			this.label_output_format.Name = "label_output_format";
			this.label_output_format.Size = new System.Drawing.Size(89, 13);
			this.label_output_format.TabIndex = 8;
			this.label_output_format.Text = "Định dạng output";
			// 
			// combobox_role
			// 
			this.combobox_role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combobox_role.FormattingEnabled = true;
			this.combobox_role.Location = new System.Drawing.Point(68, 16);
			this.combobox_role.Name = "combobox_role";
			this.combobox_role.Size = new System.Drawing.Size(192, 21);
			this.combobox_role.TabIndex = 8;
			// 
			// OptionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 466);
			this.Controls.Add(this.groupbox_model_setting);
			this.Controls.Add(this.groupbox_additional_infor);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Option";
			this.Load += new System.EventHandler(this.OptionForm_Load);
			this.groupbox_additional_infor.ResumeLayout(false);
			this.groupbox_additional_infor.PerformLayout();
			this.groupbox_model_setting.ResumeLayout(false);
			this.groupbox_model_setting.PerformLayout();
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
		private System.Windows.Forms.Label label_role;
		private System.Windows.Forms.GroupBox groupbox_model_setting;
		private System.Windows.Forms.ComboBox combobox_role;
		private System.Windows.Forms.TextBox textbox_output_format;
		private System.Windows.Forms.Label label_output_format;
		private System.Windows.Forms.CheckBox checkbox_translate_input;
		private System.Windows.Forms.ComboBox combobox_output_language;
		private System.Windows.Forms.CheckBox checkbox_translate_output;
		private System.Windows.Forms.Label label_env;
		private System.Windows.Forms.TextBox textbox_env;
	}
}