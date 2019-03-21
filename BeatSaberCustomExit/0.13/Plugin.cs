using BeatSaverCustomExit.Settings;
using CustomUI.MenuButton;
using Harmony;
using IllusionPlugin;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeatSaberCustomExit
{
    public class Plugin : IPlugin
    {
        public string Name => "BeatSaberCustomExit";
        public string Version => "1.1.1";

        public static void QuitGame()
        {
            Application.Quit();
        }

        public static void RestartGame()
        {
            Process.Start(Path.Combine(Environment.CurrentDirectory, Process.GetCurrentProcess().MainModule.FileName), Environment.CommandLine);
            QuitGame();
        }

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            PluginConfig.LoadOrCreateConfig();
            HarmonyInstance harmony = HarmonyInstance.Create("com.Shoko84.beatsaber.BeatSaberCustomExit");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
            PluginConfig.SaveConfig();
        }

        private void _OnRestartGameButtonClick()
        {
            if (PluginConfig.AskWhenRestarting)
                MainMenuUIOverload.Instance.ShowConfirmQuitPanel(MainMenuUIOverload.PanelState.RESTART);
            else
                RestartGame();
        }

        private void SceneManagerOnActiveSceneChanged(Scene from, Scene to)
        {
            Console.WriteLine($"[BeatSaberCustomExit] Active scene changed from \"{from.name}\" to \"{to.name}\"");

            if (from.name == "EmptyTransition" && to.name.Contains("Menu"))
            {
                try
                {
                    SettingsUI.Instance.OnLoad();
                    MainMenuUIOverload.Instance.OnLoad();
                    if (PluginConfig.EnablePlugin)
                        MenuButtonUI.AddButton("Restart Game", () => _OnRestartGameButtonClick());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception on scene change: " + e);
                }
            }
        }

        private void SceneManager_sceneLoaded(Scene from, LoadSceneMode to)
        {

        }

        public void OnLevelWasLoaded(int level)
        {

        }

        public void OnLevelWasInitialized(int level)
        {
        }

        public void OnUpdate()
        {

        }

        public void OnFixedUpdate()
        {

        }
    }
}
