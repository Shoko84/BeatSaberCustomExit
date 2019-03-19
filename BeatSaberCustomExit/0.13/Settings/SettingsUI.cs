using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CustomUI.Utilities;
using CustomUI.MenuButton;
using CustomUI.Settings;

namespace BeatSaverCustomExit.Settings
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
            var askWhenQuitting = customExitSubMenu.AddBool("Confirm on quit", "Show the dialog when quitting");
            askWhenQuitting.GetValue += delegate { return PluginConfig.AskWhenQuitting; };
            askWhenQuitting.SetValue += delegate (bool value) { PluginConfig.AskWhenQuitting = value; PluginConfig.SaveConfig(); };
            var askWhenRestarting = customExitSubMenu.AddBool("Confirm on restart", "Show the dialog when restarting");
            askWhenRestarting.GetValue += delegate { return PluginConfig.AskWhenRestarting; };
            askWhenRestarting.SetValue += delegate (bool value) { PluginConfig.AskWhenRestarting = value; PluginConfig.SaveConfig(); };
            var quitTextContent = customExitSubMenu.AddString("Quit text content", "Text written on the dialog opened when quitting (RichText compatible)");
            quitTextContent.GetValue += delegate { return PluginConfig.QuitTextContent; };
            quitTextContent.SetValue += delegate (string value) { PluginConfig.QuitTextContent = value; PluginConfig.SaveConfig(); };
            var restartTextContent = customExitSubMenu.AddString("Restart text content", "Text written on the dialog opened when restarting (RichText compatible)");
            restartTextContent.GetValue += delegate { return PluginConfig.RestartTextContent; };
            restartTextContent.SetValue += delegate (string value) { PluginConfig.RestartTextContent = value; PluginConfig.SaveConfig(); };

            Initialized = true;
        }
    }
}
