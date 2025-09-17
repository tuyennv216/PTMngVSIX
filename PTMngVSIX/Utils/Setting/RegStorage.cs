using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
using System;

namespace PTMngVSIX.Utils.Setting
{
	internal class RegStorage
	{
		private static readonly Guid PackageGuid = typeof(PTMngVSIXPackage).GUID;
		private static readonly string PackageGuidStr = typeof(PTMngVSIXPackage).GUID.ToString();

		public static string GetValue(string pageName, string propertyName, string defaultValue = null)
		{
			var mgr = new ShellSettingsManager(ServiceProvider.GlobalProvider);
			var store = mgr.GetReadOnlySettingsStore(SettingsScope.UserSettings);
			var key = $@"{pageName}\{propertyName}";
			return store.GetString(PackageGuid.ToString(), key, defaultValue);
		}

		public static void SetValue(string pageName, string propertyName, string value)
		{
			if (string.IsNullOrEmpty(propertyName)) return;
			if (string.IsNullOrEmpty(value)) return;

			var mgr = new ShellSettingsManager(ServiceProvider.GlobalProvider);
			var store = mgr.GetWritableSettingsStore(SettingsScope.UserSettings);

			if (!store.CollectionExists(PackageGuidStr))
				store.CreateCollection(PackageGuidStr);

			var key = $@"{pageName}\{propertyName}";
			store.SetString(PackageGuid.ToString(), key, value);
		}

		public static bool GetBool(string pageName, string propertyName, bool defaultValue = false)
		{
			var mgr = new ShellSettingsManager(ServiceProvider.GlobalProvider);
			var store = mgr.GetReadOnlySettingsStore(SettingsScope.UserSettings);
			var key = $@"{pageName}\{propertyName}";
			return store.GetBoolean(PackageGuid.ToString(), key, defaultValue);
		}

		public static void SetBool(string pageName, string propertyName, bool value)
		{
			if (string.IsNullOrEmpty(propertyName)) return;

			var mgr = new ShellSettingsManager(ServiceProvider.GlobalProvider);
			var store = mgr.GetWritableSettingsStore(SettingsScope.UserSettings);

			if (!store.CollectionExists(PackageGuidStr))
				store.CreateCollection(PackageGuidStr);

			var key = $@"{pageName}\{propertyName}";
			store.SetBoolean(PackageGuid.ToString(), key, value);
		}
	}
}
