using System;

namespace PTMngVSIX.Shortcut.KeyboardService
{
	public interface IKeyboardSequenceService
	{
		void Initialize();
		void RegisterSequence(string sequence, Action callback);
		void Dispose();
	}
}
