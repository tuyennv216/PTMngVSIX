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

		private void ApplyLanguage()
		{
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

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;

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

			Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
