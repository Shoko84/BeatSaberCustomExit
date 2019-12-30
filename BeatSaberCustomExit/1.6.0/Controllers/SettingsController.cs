using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Parser;

namespace CustomExit.Controllers
{
    public class SettingsController : PersistentSingleton<SettingsController>
    {
        [UIParams]
        private BSMLParserParams parserParams;

        [UIValue("enabled-bool")]
        public bool enabledValue
        {
            get => Plugin.config.Value.EnablePlugin;
            set => Plugin.config.Value.EnablePlugin = value;
        }

        [UIValue("confirm-quit-bool")]
        public bool confirmQuit
        {
            get => Plugin.config.Value.AskWhenQuitting;
            set => Plugin.config.Value.AskWhenQuitting = value;
        }

        [UIValue("confirm-restart-bool")]
        public bool confirmRestart
        {
            get => Plugin.config.Value.AskWhenRestarting;
            set => Plugin.config.Value.AskWhenRestarting = value;
        }

        [UIValue("quit-text-content-string")]
        public string quitTextContent
        {
            get => Plugin.config.Value.QuitTextContent;
            set => Plugin.config.Value.QuitTextContent = value;
        }

        [UIValue("restart-text-content-string")]
        public string restartTextContent
        {
            get => Plugin.config.Value.RestartTextContent;
            set => Plugin.config.Value.RestartTextContent = value;
        }

        [UIAction("#apply")]
        public void OnApply()
        {
            Logger.log.Info($"enabled: {enabledValue}");
            Logger.log.Info($"confirmQuit: {confirmQuit}");
            Logger.log.Info($"confirmRestart: {confirmRestart}");
            Logger.log.Info($"quitTextContent: {quitTextContent}");
            Logger.log.Info($"restartTextContent: {restartTextContent}");
            Plugin.config.Value.EnablePlugin = enabledValue;
            Plugin.config.Value.AskWhenQuitting = confirmQuit;
            Plugin.config.Value.AskWhenRestarting = confirmRestart;
            Plugin.config.Value.QuitTextContent = quitTextContent;
            Plugin.config.Value.RestartTextContent = restartTextContent;
            Plugin.configProvider.Store(Plugin.config.Value);
        }

        public void Update()
        {

        }
    }
}
