using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using BeatSaberMarkupLanguage;
using CustomExit.Controllers;
using CustomExit.Utilities;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.MenuButtons;
using BS_Utils.Utilities;
using Harmony;
using IPA;
using IPA.Config;
using IPA.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Config = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace CustomExit
{
    public class Plugin : IBeatSaberPlugin
    {
        #region Properties

        internal static Ref<PluginConfig> config;
        internal static IConfigProvider   configProvider;

        public string Name    => "Custom Exit";
        public string Version => "1.2.2";

        private HarmonyInstance _harmony;

        #endregion

        #region BSIPA Events

        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
            BSEvents.menuSceneLoadedFresh += MenuLoadFresh;
            configProvider = cfgProvider;

            config = cfgProvider.MakeLink<PluginConfig>((p, v) =>
            {
                if (v.Value == null || v.Value.RegenerateConfig)
                    p.Store(v.Value = new PluginConfig { RegenerateConfig = false });
                config = v;
            });
        }

        public void OnApplicationStart()
        {
            MenuButtons.instance.RegisterButton(new MenuButton("Restart Game", "Restart the game", OnRestartGameButtonClick));
            _harmony = HarmonyInstance.Create("com.Shoko84.beatsaber.BeatSaberCustomExit");
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void OnApplicationQuit()
        {
            Plugin.configProvider.Store(config.Value);
        }

        public void OnFixedUpdate()
        {

        }

        public void OnUpdate()
        {

        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {

        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {

        }

        public void OnSceneUnloaded(Scene scene)
        {

        }

        #endregion

        #region Methods

        public static void QuitGame()
        {
            Application.Quit();
        }

        public static void RestartGame()
        {
            Process.Start(Path.Combine(Environment.CurrentDirectory, Process.GetCurrentProcess().MainModule.FileName), Environment.CommandLine);
            QuitGame();
        }

        private static void OnRestartGameButtonClick()
        {
            if (config.Value.EnablePlugin && config.Value.AskWhenRestarting)
                MainMenuUIOverload.Instance.ShowConfirmQuitPanel(MainMenuUIOverload.PanelState.Restart);
            else
                RestartGame();
        }

        #endregion

        #region Methods

        public void MenuLoadFresh()
        {
            BSMLSettings.instance.AddSettingsMenu("Custom Exit", "CustomExit.Views.settings.bsml", SettingsController.instance);
            MainMenuUIOverload.Instance.OnLoad();
        }

        #endregion
    }
}
