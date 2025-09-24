using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Setting;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace PTMngVSIX.Shortcut.KeyboardService
{
	public class KeyboardSequenceService : IKeyboardSequenceService, IDisposable
	{
		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x0100;
		private const int WM_KEYUP = 0x0101;

		private LowLevelKeyboardProc _proc;
		private IntPtr _hookID = IntPtr.Zero;
		private DateTime _lastKeyPress = DateTime.MinValue;
		private string _currentSequence = "";
		private bool _isExtensionHotkeyPressed = false;
		private readonly TimeSpan _sequenceTimeout = TimeSpan.FromSeconds(2);

		private readonly Dictionary<string, Action> _sequenceHandlers = new Dictionary<string, Action>();

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

		public KeyboardSequenceService()
		{
			_proc = HookCallback;
		}

		public void Initialize()
		{
			_hookID = SetHook(_proc);
		}

		public void RegisterSequence(string sequence, Action callback)
		{
			_sequenceHandlers[sequence.ToUpper()] = callback;
		}

		private IntPtr SetHook(LowLevelKeyboardProc proc)
		{
			using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
			using (var curModule = curProcess.MainModule)
			{
				return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
					GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0 && IDESetting.UseAdvancedShortcut)
			{
				if (wParam == (IntPtr)WM_KEYDOWN)
				{
					int vkCode = Marshal.ReadInt32(lParam);
					var key = KeyInterop.KeyFromVirtualKey(vkCode);

					// Check for Ctrl+Shift+M
					if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
					{
						if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
						{
							if (key == (Key)IDESetting.AdvancedHotkey)
							{
								_isExtensionHotkeyPressed = true;
								_currentSequence = "";
								_lastKeyPress = DateTime.Now;
								return (IntPtr)1; // Block the key
							}
						}
					}

					// Handle sequence if Ctrl+Shift+M was pressed
					if (_isExtensionHotkeyPressed)
					{
						// Check for timeout
						if (DateTime.Now - _lastKeyPress > _sequenceTimeout)
						{
							ResetSequence();
							return CallNextHookEx(_hookID, nCode, wParam, lParam);
						}

						// Add to sequence (only alphabetic keys)
						if ((key >= Key.A && key <= Key.Z))
						{
							_currentSequence += key.ToString().ToUpper();
							_lastKeyPress = DateTime.Now;

							// Check if sequence matches any registered handler
							if (_sequenceHandlers.ContainsKey(_currentSequence))
							{
								var handler = _sequenceHandlers[_currentSequence];
								ThreadHelper.Generic.BeginInvoke(() => handler());
								ResetSequence();
								return (IntPtr)1; // Block the key
							}

							return (IntPtr)1; // Block the key during sequence
						}

						// Escape key to cancel sequence
						if (key == Key.Escape)
						{
							ResetSequence();
						}
					}
				}
			}

			return CallNextHookEx(_hookID, nCode, wParam, lParam);
		}

		private void ResetSequence()
		{
			_isExtensionHotkeyPressed = false;
			_currentSequence = "";
			_lastKeyPress = DateTime.MinValue;
		}

		public void Dispose()
		{
			if (_hookID != IntPtr.Zero)
			{
				UnhookWindowsHookEx(_hookID);
				_hookID = IntPtr.Zero;
			}
		}
	}
}
