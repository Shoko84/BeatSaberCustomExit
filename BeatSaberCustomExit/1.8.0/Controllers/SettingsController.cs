using BeatSaberMarkupLanguage.Attributes;
using CustomExit.Utilities;

namespace CustomExit.Controllers
{
    public class SettingsController : PersistentSingleton<SettingsController>
    {
        [UIValue("enabled-bool")]
        public bool enabledValue
        {
            get => PluginConfig.Instance.EnablePlugin;
            set => PluginConfig.Instance.EnablePlugin = value;
        }

        [UIValue("confirm-quit-bool")]
        public bool confirmQuit
        {
            get => PluginConfig.Instance.AskWhenQuitting;
            set => PluginConfig.Instance.AskWhenQuitting = value;
        }

        [UIValue("confirm-restart-bool")]
        public bool confirmRestart
        {
            get => PluginConfig.Instance.AskWhenRestarting;
            set => PluginConfig.Instance.AskWhenRestarting = value;
        }

        [UIValue("quit-text-content-string")]
        public string quitTextContent
        {
            get => PluginConfig.Instance.QuitTextContent;
            set => PluginConfig.Instance.QuitTextContent = value;
        }

        [UIValue("restart-text-content-string")]
        public string restartTextContent
        {
            get => PluginConfig.Instance.RestartTextContent;
            set => PluginConfig.Instance.RestartTextContent = value;
        }

        [UIAction("#apply")]
        public void OnApply()
        {
            PluginConfig.Instance.EnablePlugin = enabledValue;
            PluginConfig.Instance.AskWhenQuitting = confirmQuit;
            PluginConfig.Instance.AskWhenRestarting = confirmRestart;
            PluginConfig.Instance.QuitTextContent = quitTextContent;
            PluginConfig.Instance.RestartTextContent = restartTextContent;
        }
    }
}
