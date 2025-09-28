using PTMngVSIX.Setting;
using System.Windows.Forms;

namespace PTMngVSIX.ToolWindow.Forms
{
	public partial class DebugInfo : Form
	{
		public DebugInfo()
		{
			this.InitializeComponent();
			this.ApplyLanguage();
		}

		private void DebugInfo_Load(object sender, System.EventArgs e)
		{
			this.textbox_session_time.Text = AIServiceUsage.Instance.SessionTime.ToString("HH:mm:ss");
			this.textbox_total_input_tokens.Text = AIServiceUsage.Instance.TotalUsage.PromptTokens.ToString();
			this.textbox_total_output_tokens.Text = AIServiceUsage.Instance.TotalUsage.CompletionTokens.ToString();

			this.textbox_total_tokens.Text = AIServiceUsage.Instance.TotalUsage.TotalTokens.ToString();

			this.textbox_input_tokens_details.Text = AIServiceUsage.Instance.TotalUsage.PromptTokensDetails?.ToString() ?? string.Empty;
			this.textbox_output_tokens_details.Text = AIServiceUsage.Instance.TotalUsage.CompletionTokensDetails?.ToString() ?? string.Empty;

			this.textbox_last_send_endpoint.Text = ModelSetting.Endpoint;
			this.textbox_last_send_model.Text = ModelSetting.AssistantModelName;
			this.textbox_last_receive_id.Text = AIServiceUsage.Instance.LastResponse.Id;
			this.textbox_last_receive_provider.Text = AIServiceUsage.Instance.LastResponse.Provider.ToString();
			this.textbox_last_receive_model.Text = AIServiceUsage.Instance.LastResponse.Model;

			this.textbox_last_send.Text = AIServiceUsage.Instance.LastSendText;
			this.textbox_last_receive.Text = AIServiceUsage.Instance.LastReceiveText;
		}

		private void ApplyLanguage()
		{
			this.Text = Resource.Lang.DebugInfo.Dialog_Title;
			this.groupbox_session.Text = Resource.Lang.DebugInfo.Session_Title;
			this.label_session_time.Text = Resource.Lang.DebugInfo.Session_Time;
			this.label_total_input_tokens.Text = Resource.Lang.DebugInfo.Session_Total_Number_Of_Input_Tokens;
			this.label_total_output_tokens.Text = Resource.Lang.DebugInfo.Session_Total_Number_Of_Output_Token;
			this.label_total_tokens.Text = Resource.Lang.DebugInfo.Session_Total_Number_Of_Tokens;
			this.label_prompt_tokens_details.Text = Resource.Lang.DebugInfo.Session_Input_Tokens_Details;
			this.label_completion_tokens_details.Text = Resource.Lang.DebugInfo.Session_Output_Tokens_Details;

			this.groupbox_last_info.Text = Resource.Lang.DebugInfo.Last_Title;
			this.groupbox_last_send.Text = Resource.Lang.DebugInfo.Last_Send_Title;
			this.label_last_endpoint.Text = Resource.Lang.DebugInfo.Last_Send_Endpoint;
			this.label_last_assistant.Text = Resource.Lang.DebugInfo.Last_Send_Model;

			this.groupbox_last_receive.Text = Resource.Lang.DebugInfo.Last_Receive_Title;
			this.label_id.Text = Resource.Lang.DebugInfo.Last_Receive_Id;
			this.label_provider.Text = Resource.Lang.DebugInfo.Last_Receive_Provider;
			this.label_model.Text = Resource.Lang.DebugInfo.Last_Receive_Model;
		}
	}
}
