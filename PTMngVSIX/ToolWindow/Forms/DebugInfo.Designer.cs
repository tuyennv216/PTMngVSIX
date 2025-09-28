namespace PTMngVSIX.ToolWindow.Forms
{
	partial class DebugInfo
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
			this.textbox_last_send = new System.Windows.Forms.TextBox();
			this.textbox_last_receive = new System.Windows.Forms.TextBox();
			this.groupbox_last_info = new System.Windows.Forms.GroupBox();
			this.groupbox_last_receive = new System.Windows.Forms.GroupBox();
			this.label_id = new System.Windows.Forms.Label();
			this.label_model = new System.Windows.Forms.Label();
			this.textbox_last_receive_model = new System.Windows.Forms.TextBox();
			this.textbox_last_receive_provider = new System.Windows.Forms.TextBox();
			this.textbox_last_receive_id = new System.Windows.Forms.TextBox();
			this.label_provider = new System.Windows.Forms.Label();
			this.groupbox_last_send = new System.Windows.Forms.GroupBox();
			this.label_last_assistant = new System.Windows.Forms.Label();
			this.textbox_last_send_model = new System.Windows.Forms.TextBox();
			this.textbox_last_send_endpoint = new System.Windows.Forms.TextBox();
			this.label_last_endpoint = new System.Windows.Forms.Label();
			this.groupbox_session = new System.Windows.Forms.GroupBox();
			this.textbox_output_tokens_details = new System.Windows.Forms.TextBox();
			this.textbox_input_tokens_details = new System.Windows.Forms.TextBox();
			this.textbox_total_tokens = new System.Windows.Forms.TextBox();
			this.textbox_total_output_tokens = new System.Windows.Forms.TextBox();
			this.textbox_total_input_tokens = new System.Windows.Forms.TextBox();
			this.textbox_session_time = new System.Windows.Forms.TextBox();
			this.label_completion_tokens_details = new System.Windows.Forms.Label();
			this.label_prompt_tokens_details = new System.Windows.Forms.Label();
			this.label_total_tokens = new System.Windows.Forms.Label();
			this.label_session_time = new System.Windows.Forms.Label();
			this.label_total_output_tokens = new System.Windows.Forms.Label();
			this.label_total_input_tokens = new System.Windows.Forms.Label();
			this.groupbox_last_info.SuspendLayout();
			this.groupbox_last_receive.SuspendLayout();
			this.groupbox_last_send.SuspendLayout();
			this.groupbox_session.SuspendLayout();
			this.SuspendLayout();
			// 
			// textbox_last_send
			// 
			this.textbox_last_send.BackColor = System.Drawing.SystemColors.Control;
			this.textbox_last_send.Location = new System.Drawing.Point(6, 101);
			this.textbox_last_send.Multiline = true;
			this.textbox_last_send.Name = "textbox_last_send";
			this.textbox_last_send.ReadOnly = true;
			this.textbox_last_send.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textbox_last_send.Size = new System.Drawing.Size(349, 403);
			this.textbox_last_send.TabIndex = 1;
			// 
			// textbox_last_receive
			// 
			this.textbox_last_receive.Location = new System.Drawing.Point(6, 101);
			this.textbox_last_receive.Multiline = true;
			this.textbox_last_receive.Name = "textbox_last_receive";
			this.textbox_last_receive.ReadOnly = true;
			this.textbox_last_receive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textbox_last_receive.Size = new System.Drawing.Size(480, 403);
			this.textbox_last_receive.TabIndex = 3;
			// 
			// groupbox_last_info
			// 
			this.groupbox_last_info.Controls.Add(this.groupbox_last_receive);
			this.groupbox_last_info.Controls.Add(this.groupbox_last_send);
			this.groupbox_last_info.Location = new System.Drawing.Point(12, 214);
			this.groupbox_last_info.Name = "groupbox_last_info";
			this.groupbox_last_info.Size = new System.Drawing.Size(909, 544);
			this.groupbox_last_info.TabIndex = 4;
			this.groupbox_last_info.TabStop = false;
			this.groupbox_last_info.Text = "Gửi và nhận lần cuối";
			// 
			// groupbox_last_receive
			// 
			this.groupbox_last_receive.Controls.Add(this.label_id);
			this.groupbox_last_receive.Controls.Add(this.label_model);
			this.groupbox_last_receive.Controls.Add(this.textbox_last_receive_model);
			this.groupbox_last_receive.Controls.Add(this.textbox_last_receive);
			this.groupbox_last_receive.Controls.Add(this.textbox_last_receive_provider);
			this.groupbox_last_receive.Controls.Add(this.textbox_last_receive_id);
			this.groupbox_last_receive.Controls.Add(this.label_provider);
			this.groupbox_last_receive.Location = new System.Drawing.Point(395, 25);
			this.groupbox_last_receive.Name = "groupbox_last_receive";
			this.groupbox_last_receive.Size = new System.Drawing.Size(498, 510);
			this.groupbox_last_receive.TabIndex = 21;
			this.groupbox_last_receive.TabStop = false;
			this.groupbox_last_receive.Text = "Dữ liệu nhận";
			// 
			// label_id
			// 
			this.label_id.AutoSize = true;
			this.label_id.Location = new System.Drawing.Point(9, 22);
			this.label_id.Name = "label_id";
			this.label_id.Size = new System.Drawing.Size(16, 13);
			this.label_id.TabIndex = 4;
			this.label_id.Text = "Id";
			// 
			// label_model
			// 
			this.label_model.AutoSize = true;
			this.label_model.Location = new System.Drawing.Point(9, 75);
			this.label_model.Name = "label_model";
			this.label_model.Size = new System.Drawing.Size(36, 13);
			this.label_model.TabIndex = 19;
			this.label_model.Text = "Model";
			// 
			// textbox_last_receive_model
			// 
			this.textbox_last_receive_model.Location = new System.Drawing.Point(179, 72);
			this.textbox_last_receive_model.Name = "textbox_last_receive_model";
			this.textbox_last_receive_model.ReadOnly = true;
			this.textbox_last_receive_model.Size = new System.Drawing.Size(182, 20);
			this.textbox_last_receive_model.TabIndex = 18;
			// 
			// textbox_last_receive_provider
			// 
			this.textbox_last_receive_provider.Location = new System.Drawing.Point(179, 46);
			this.textbox_last_receive_provider.Name = "textbox_last_receive_provider";
			this.textbox_last_receive_provider.ReadOnly = true;
			this.textbox_last_receive_provider.Size = new System.Drawing.Size(182, 20);
			this.textbox_last_receive_provider.TabIndex = 17;
			// 
			// textbox_last_receive_id
			// 
			this.textbox_last_receive_id.Location = new System.Drawing.Point(179, 19);
			this.textbox_last_receive_id.Name = "textbox_last_receive_id";
			this.textbox_last_receive_id.ReadOnly = true;
			this.textbox_last_receive_id.Size = new System.Drawing.Size(182, 20);
			this.textbox_last_receive_id.TabIndex = 15;
			// 
			// label_provider
			// 
			this.label_provider.AutoSize = true;
			this.label_provider.Location = new System.Drawing.Point(9, 49);
			this.label_provider.Name = "label_provider";
			this.label_provider.Size = new System.Drawing.Size(46, 13);
			this.label_provider.TabIndex = 16;
			this.label_provider.Text = "Provider";
			// 
			// groupbox_last_send
			// 
			this.groupbox_last_send.Controls.Add(this.label_last_assistant);
			this.groupbox_last_send.Controls.Add(this.textbox_last_send_model);
			this.groupbox_last_send.Controls.Add(this.textbox_last_send_endpoint);
			this.groupbox_last_send.Controls.Add(this.label_last_endpoint);
			this.groupbox_last_send.Controls.Add(this.textbox_last_send);
			this.groupbox_last_send.Location = new System.Drawing.Point(9, 25);
			this.groupbox_last_send.Name = "groupbox_last_send";
			this.groupbox_last_send.Size = new System.Drawing.Size(365, 510);
			this.groupbox_last_send.TabIndex = 20;
			this.groupbox_last_send.TabStop = false;
			this.groupbox_last_send.Text = "Dữ liệu gửi";
			// 
			// label_last_assistant
			// 
			this.label_last_assistant.AutoSize = true;
			this.label_last_assistant.Location = new System.Drawing.Point(6, 49);
			this.label_last_assistant.Name = "label_last_assistant";
			this.label_last_assistant.Size = new System.Drawing.Size(49, 13);
			this.label_last_assistant.TabIndex = 18;
			this.label_last_assistant.Text = "Assistant";
			// 
			// textbox_last_send_model
			// 
			this.textbox_last_send_model.Location = new System.Drawing.Point(173, 46);
			this.textbox_last_send_model.Name = "textbox_last_send_model";
			this.textbox_last_send_model.ReadOnly = true;
			this.textbox_last_send_model.Size = new System.Drawing.Size(182, 20);
			this.textbox_last_send_model.TabIndex = 17;
			// 
			// textbox_last_send_endpoint
			// 
			this.textbox_last_send_endpoint.Location = new System.Drawing.Point(173, 19);
			this.textbox_last_send_endpoint.Name = "textbox_last_send_endpoint";
			this.textbox_last_send_endpoint.ReadOnly = true;
			this.textbox_last_send_endpoint.Size = new System.Drawing.Size(182, 20);
			this.textbox_last_send_endpoint.TabIndex = 16;
			// 
			// label_last_endpoint
			// 
			this.label_last_endpoint.AutoSize = true;
			this.label_last_endpoint.Location = new System.Drawing.Point(6, 22);
			this.label_last_endpoint.Name = "label_last_endpoint";
			this.label_last_endpoint.Size = new System.Drawing.Size(49, 13);
			this.label_last_endpoint.TabIndex = 5;
			this.label_last_endpoint.Text = "Endpoint";
			// 
			// groupbox_session
			// 
			this.groupbox_session.Controls.Add(this.textbox_output_tokens_details);
			this.groupbox_session.Controls.Add(this.textbox_input_tokens_details);
			this.groupbox_session.Controls.Add(this.textbox_total_tokens);
			this.groupbox_session.Controls.Add(this.textbox_total_output_tokens);
			this.groupbox_session.Controls.Add(this.textbox_total_input_tokens);
			this.groupbox_session.Controls.Add(this.textbox_session_time);
			this.groupbox_session.Controls.Add(this.label_completion_tokens_details);
			this.groupbox_session.Controls.Add(this.label_prompt_tokens_details);
			this.groupbox_session.Controls.Add(this.label_total_tokens);
			this.groupbox_session.Controls.Add(this.label_session_time);
			this.groupbox_session.Controls.Add(this.label_total_output_tokens);
			this.groupbox_session.Controls.Add(this.label_total_input_tokens);
			this.groupbox_session.Location = new System.Drawing.Point(12, 12);
			this.groupbox_session.Name = "groupbox_session";
			this.groupbox_session.Size = new System.Drawing.Size(909, 196);
			this.groupbox_session.TabIndex = 5;
			this.groupbox_session.TabStop = false;
			this.groupbox_session.Text = "Phiên làm việc";
			// 
			// textbox_output_tokens_details
			// 
			this.textbox_output_tokens_details.Location = new System.Drawing.Point(656, 45);
			this.textbox_output_tokens_details.Multiline = true;
			this.textbox_output_tokens_details.Name = "textbox_output_tokens_details";
			this.textbox_output_tokens_details.ReadOnly = true;
			this.textbox_output_tokens_details.Size = new System.Drawing.Size(237, 136);
			this.textbox_output_tokens_details.TabIndex = 16;
			// 
			// textbox_input_tokens_details
			// 
			this.textbox_input_tokens_details.Location = new System.Drawing.Point(395, 45);
			this.textbox_input_tokens_details.Multiline = true;
			this.textbox_input_tokens_details.Name = "textbox_input_tokens_details";
			this.textbox_input_tokens_details.ReadOnly = true;
			this.textbox_input_tokens_details.Size = new System.Drawing.Size(237, 136);
			this.textbox_input_tokens_details.TabIndex = 15;
			// 
			// textbox_total_tokens
			// 
			this.textbox_total_tokens.Location = new System.Drawing.Point(176, 97);
			this.textbox_total_tokens.Name = "textbox_total_tokens";
			this.textbox_total_tokens.ReadOnly = true;
			this.textbox_total_tokens.Size = new System.Drawing.Size(182, 20);
			this.textbox_total_tokens.TabIndex = 14;
			// 
			// textbox_total_output_tokens
			// 
			this.textbox_total_output_tokens.Location = new System.Drawing.Point(176, 71);
			this.textbox_total_output_tokens.Name = "textbox_total_output_tokens";
			this.textbox_total_output_tokens.ReadOnly = true;
			this.textbox_total_output_tokens.Size = new System.Drawing.Size(182, 20);
			this.textbox_total_output_tokens.TabIndex = 13;
			// 
			// textbox_total_input_tokens
			// 
			this.textbox_total_input_tokens.Location = new System.Drawing.Point(176, 45);
			this.textbox_total_input_tokens.Name = "textbox_total_input_tokens";
			this.textbox_total_input_tokens.ReadOnly = true;
			this.textbox_total_input_tokens.Size = new System.Drawing.Size(182, 20);
			this.textbox_total_input_tokens.TabIndex = 12;
			// 
			// textbox_session_time
			// 
			this.textbox_session_time.Location = new System.Drawing.Point(176, 19);
			this.textbox_session_time.Name = "textbox_session_time";
			this.textbox_session_time.ReadOnly = true;
			this.textbox_session_time.Size = new System.Drawing.Size(182, 20);
			this.textbox_session_time.TabIndex = 11;
			// 
			// label_completion_tokens_details
			// 
			this.label_completion_tokens_details.AutoSize = true;
			this.label_completion_tokens_details.Location = new System.Drawing.Point(653, 23);
			this.label_completion_tokens_details.Name = "label_completion_tokens_details";
			this.label_completion_tokens_details.Size = new System.Drawing.Size(113, 13);
			this.label_completion_tokens_details.TabIndex = 10;
			this.label_completion_tokens_details.Text = "Chi tiết Output Tokens";
			// 
			// label_prompt_tokens_details
			// 
			this.label_prompt_tokens_details.AutoSize = true;
			this.label_prompt_tokens_details.Location = new System.Drawing.Point(392, 23);
			this.label_prompt_tokens_details.Name = "label_prompt_tokens_details";
			this.label_prompt_tokens_details.Size = new System.Drawing.Size(105, 13);
			this.label_prompt_tokens_details.TabIndex = 9;
			this.label_prompt_tokens_details.Text = "Chi tiết Input Tokens";
			// 
			// label_total_tokens
			// 
			this.label_total_tokens.AutoSize = true;
			this.label_total_tokens.Location = new System.Drawing.Point(6, 100);
			this.label_total_tokens.Name = "label_total_tokens";
			this.label_total_tokens.Size = new System.Drawing.Size(114, 13);
			this.label_total_tokens.TabIndex = 8;
			this.label_total_tokens.Text = "Tổng số lượng Tokens";
			// 
			// label_session_time
			// 
			this.label_session_time.AutoSize = true;
			this.label_session_time.Location = new System.Drawing.Point(6, 23);
			this.label_session_time.Name = "label_session_time";
			this.label_session_time.Size = new System.Drawing.Size(91, 13);
			this.label_session_time.TabIndex = 7;
			this.label_session_time.Text = "Thời gian bắt đầu";
			// 
			// label_total_output_tokens
			// 
			this.label_total_output_tokens.AutoSize = true;
			this.label_total_output_tokens.Location = new System.Drawing.Point(6, 74);
			this.label_total_output_tokens.Name = "label_total_output_tokens";
			this.label_total_output_tokens.Size = new System.Drawing.Size(149, 13);
			this.label_total_output_tokens.TabIndex = 6;
			this.label_total_output_tokens.Text = "Tổng số lượng Output Tokens";
			// 
			// label_total_input_tokens
			// 
			this.label_total_input_tokens.AutoSize = true;
			this.label_total_input_tokens.Location = new System.Drawing.Point(6, 48);
			this.label_total_input_tokens.Name = "label_total_input_tokens";
			this.label_total_input_tokens.Size = new System.Drawing.Size(141, 13);
			this.label_total_input_tokens.TabIndex = 0;
			this.label_total_input_tokens.Text = "Tổng số lượng Input Tokens";
			// 
			// DebugInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(933, 767);
			this.Controls.Add(this.groupbox_session);
			this.Controls.Add(this.groupbox_last_info);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DebugInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Thông tin gỡ lỗi";
			this.Load += new System.EventHandler(this.DebugInfo_Load);
			this.groupbox_last_info.ResumeLayout(false);
			this.groupbox_last_receive.ResumeLayout(false);
			this.groupbox_last_receive.PerformLayout();
			this.groupbox_last_send.ResumeLayout(false);
			this.groupbox_last_send.PerformLayout();
			this.groupbox_session.ResumeLayout(false);
			this.groupbox_session.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox textbox_last_send;
		private System.Windows.Forms.TextBox textbox_last_receive;
		private System.Windows.Forms.GroupBox groupbox_last_info;
		private System.Windows.Forms.GroupBox groupbox_session;
		private System.Windows.Forms.Label label_total_input_tokens;
		private System.Windows.Forms.Label label_total_output_tokens;
		private System.Windows.Forms.Label label_session_time;
		private System.Windows.Forms.Label label_total_tokens;
		private System.Windows.Forms.Label label_completion_tokens_details;
		private System.Windows.Forms.Label label_prompt_tokens_details;
		private System.Windows.Forms.TextBox textbox_total_input_tokens;
		private System.Windows.Forms.TextBox textbox_session_time;
		private System.Windows.Forms.TextBox textbox_output_tokens_details;
		private System.Windows.Forms.TextBox textbox_input_tokens_details;
		private System.Windows.Forms.TextBox textbox_total_tokens;
		private System.Windows.Forms.TextBox textbox_total_output_tokens;
		private System.Windows.Forms.TextBox textbox_last_receive_model;
		private System.Windows.Forms.TextBox textbox_last_receive_provider;
		private System.Windows.Forms.Label label_provider;
		private System.Windows.Forms.TextBox textbox_last_receive_id;
		private System.Windows.Forms.Label label_id;
		private System.Windows.Forms.Label label_model;
		private System.Windows.Forms.GroupBox groupbox_last_receive;
		private System.Windows.Forms.GroupBox groupbox_last_send;
		private System.Windows.Forms.Label label_last_assistant;
		private System.Windows.Forms.TextBox textbox_last_send_model;
		private System.Windows.Forms.TextBox textbox_last_send_endpoint;
		private System.Windows.Forms.Label label_last_endpoint;
	}
}