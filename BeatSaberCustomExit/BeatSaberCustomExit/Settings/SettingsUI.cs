using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CustomUI.Utilities;
using CustomUI.MenuButton;
using BeatSaverDownloader.Misc;
using CustomUI.Settings;

namespace BeatSaverDownloader.UI
{
    class SettingsUI : MonoBehaviour
    {
        public bool Initialized = false;

        private static SettingsUI _instance = null;
        public static SettingsUI Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = new GameObject("[BeatSaverCustomExit] SettingsUI").AddComponent<SettingsUI>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        public void OnLoad()
        {
            Initialized = false;
            SetupUI();
        }

        private void SetupUI()
        {
            if (Initialized) return;
            
            var customExitSubMenu = CustomUI.Settings.SettingsUI.CreateSubMenu("Custom Exit");

            var enablePlugin = customExitSubMenu.AddBool("Enable", "Enable or not the plugin");
            enablePlugin.GetValue += delegate { return PluginConfig.EnablePlugin; };
            enablePlugin.SetValue += delegate (bool value) { PluginConfig.EnablePlugin = value; PluginConfig.SaveConfig(); };
            var textContent = customExitSubMenu.AddString("Text content", "Text written on the dialog opened (RichText compatible)");
            textContent.GetValue += delegate { return PluginConfig.TextContent; };
            textContent.SetValue += delegate (string value) { PluginConfig.TextContent = value; PluginConfig.SaveConfig(); };

            Initialized = true;
        }
    }
}
