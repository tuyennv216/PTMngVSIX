using PTMngVSIX.Abstraction.AI;
using PTMngVSIX.Abstraction.AIServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PTMngVSIX.Setting
{
	public class AppState : INotifyPropertyChanged
	{
		public static readonly AppState Instance = new();
		private AppState() { }

		private bool _isModelAvailable = false;
		public bool IsModelAvailable
		{
			get => _isModelAvailable;
			set
			{
				_isModelAvailable = value;
				OnPropertyChanged(nameof(IsModelAvailable));
			}
		}

		public IChatClient ApiClient { get; set; }
		public IModelService Assistant { get; set; }
		public IModelService Translator { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
