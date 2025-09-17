using PTMngVSIX.Setting;
using PTMngVSIX.Utils.Setting;
using System.Windows.Forms;

namespace PTMngVSIX.ToolWindow.Forms
{
	public partial class OptionForm : Form
	{
		public Utils.Chat.Option Option { get; private set; } = new Utils.Chat.Option();

		public OptionForm()
		{
			this.InitializeComponent();
			this.ApplyLanguage();
		}

		private void OptionForm_Load(object sender, System.EventArgs e)
		{
			this.LoadCombobox();
			this.ApplyOptionPage();
		}

		private void ApplyLanguage()
		{
			this.groupbox_model_setting.Text = Resource.Lang.FormOption.Setting_GroupTitle;
			this.label_role.Text = Resource.Lang.FormOption.Setting_RoleLabel;
			this.label_output_format.Text = Resource.Lang.FormOption.Setting_OutputFormat;
			this.checkbox_translate_input.Text = Resource.Lang.FormOption.Setting_TranslateInput;
			this.checkbox_translate_output.Text = Resource.Lang.FormOption.Setting_TranslateOutput;

			this.groupbox_additional_infor.Text = Resource.Lang.FormOption.Option_LabelTitle;
			this.checkbox_solution_structure.Text = Resource.Lang.FormOption.Option_SolutionStructure;
			this.checkbox_project_structure.Text = Resource.Lang.FormOption.Option_ProjectStructure;
			this.checkbox_active_document.Text = Resource.Lang.FormOption.Option_ActiveDocument;
			this.checkbox_class.Text = Resource.Lang.FormOption.Option_ParentClass;
			this.checkbox_function.Text = Resource.Lang.FormOption.Option_ParentFunction;
			this.checkbox_selected_text.Text = Resource.Lang.FormOption.Option_SelectedText;
			this.checkbox_fill_in_middle.Text = Resource.Lang.FormOption.Option_FillInMiddle;
			this.checkbox_error.Text = Resource.Lang.FormOption.Option_Error;

			this.btnOK.Text = Resource.Lang.Dialog.Dialog_Ok;
			this.btnCancel.Text = Resource.Lang.Dialog.Dialog_Cancel;
		}

		private void LoadCombobox()
		{
			this.combobox_role.Items.Clear();
			this.combobox_role.Items.AddRange(DefaultSetting.OptionRoleList);

			this.combobox_output_language.Items.Clear();
			this.combobox_output_language.Items.AddRange(DefaultSetting.OptionOutputLanguageList);
		}

		private void ApplyOptionPage()
		{
			var pageName = Utils.Setting.Constant.OptionPageGeneral;
			this.combobox_role.SelectedItem = RegStorage.GetValue(pageName, nameof(PTMngOptionPage.RoleName));
			this.textbox_output_format.Text = RegStorage.GetValue(pageName, nameof(PTMngOptionPage.OutputFormat));
			this.checkbox_translate_input.Checked = RegStorage.GetBool(pageName, nameof(PTMngOptionPage.TranslateInput));
			this.checkbox_translate_output.Checked = RegStorage.GetBool(pageName, nameof(PTMngOptionPage.TranslateOutput));
			this.combobox_output_language.SelectedItem = RegStorage.GetValue(pageName, nameof(PTMngOptionPage.OutputLanguage));
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			SaveModelSetting();

			Option.IncludeSolutionStructure = checkbox_solution_structure.Checked;
			Option.IncludeProjectStructure = checkbox_project_structure.Checked;
			Option.IncludeActiveDocument = checkbox_active_document.Checked;
			Option.IncludeParentClass = checkbox_class.Checked;
			Option.IncludeParentFunction = checkbox_function.Checked;
			Option.IncludeSelection = checkbox_selected_text.Checked;
			Option.IncludeFillInMiddle = checkbox_fill_in_middle.Checked;
			Option.IncludeError = checkbox_error.Checked;

			checkbox_solution_structure.Checked = false;
			checkbox_project_structure.Checked = false;
			checkbox_active_document.Checked = false;
			checkbox_class.Checked = false;
			checkbox_function.Checked = false;
			checkbox_selected_text.Checked = false;
			checkbox_fill_in_middle.Checked = false;
			checkbox_error.Checked = false;

			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void SaveModelSetting()
		{
			var RoleName = combobox_role.SelectedItem?.ToString() ?? string.Empty;
			var OutputFormat = textbox_output_format.Text;
			var TranslateInput = checkbox_translate_input.Checked;
			var TranslateOutput = checkbox_translate_output.Checked;
			var OutputLanguage = combobox_output_language.SelectedItem?.ToString() ?? string.Empty;

			var pageName = Utils.Setting.Constant.OptionPageGeneral;
			RegStorage.SetValue(pageName, nameof(PTMngOptionPage.RoleName), RoleName);
			RegStorage.SetValue(pageName, nameof(PTMngOptionPage.OutputFormat), OutputFormat);
			RegStorage.SetBool(pageName, nameof(PTMngOptionPage.TranslateInput), TranslateInput);
			RegStorage.SetBool(pageName, nameof(PTMngOptionPage.TranslateOutput), TranslateOutput);
			RegStorage.SetValue(pageName, nameof(PTMngOptionPage.OutputLanguage), OutputLanguage);

			ModelSetting.RoleName = RoleName;
			ModelSetting.OutputFormat = OutputFormat;
			ModelSetting.TranslateInput = TranslateInput;
			ModelSetting.TranslateOutput = TranslateOutput;
			ModelSetting.OutputLanguage = OutputLanguage;
		}
	}
}
