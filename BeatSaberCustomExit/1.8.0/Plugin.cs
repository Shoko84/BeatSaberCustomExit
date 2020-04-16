using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using CustomExit.Controllers;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.MenuButtons;
using BS_Utils.Utilities;
using CustomExit.Utilities;
using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using UnityEngine;
using Config = IPA.Config.Config;
using Logger = IPA.Logging.Logger;

namespace CustomExit
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        #region Properties

        public static Logger Log { get; private set; }

        #endregion

        #region BSIPA Events

        [Init]
        public Plugin(Logger logger, Config conf)
        {
            Log = logger;
            PluginConfig.Instance = conf.Generated<PluginConfig>();
        }

        [OnStart]
        public void OnApplicationStart()
        {
            BSEvents.menuSceneLoadedFresh += MenuLoadFresh;
            MenuButtons.instance.RegisterButton(new MenuButton("Restart Game", "Restart the game", OnRestartGameButtonClick));
            var harmony = new Harmony("com.Shoko84.beatsaber.CustomExit");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        #endregion

        #region Events

        public void MenuLoadFresh()
        {
            BSMLSettings.instance.AddSettingsMenu("Custom Exit", "CustomExit.Views.settings.bsml", SettingsController.instance);
            MainMenuUIOverload.Instance.OnLoad();
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
            if (PluginConfig.Instance.EnablePlugin && PluginConfig.Instance.AskWhenRestarting)
                MainMenuUIOverload.Instance.ShowConfirmQuitPanel(MainMenuUIOverload.PanelState.Restart);
            else
                RestartGame();
        }

        #endregion
    }
}
