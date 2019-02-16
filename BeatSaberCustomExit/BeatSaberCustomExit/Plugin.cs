using BeatSaverCustomExit.Settings;
using Harmony;
using IllusionPlugin;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeatSaberCustomExit
{
    public class Plugin : IPlugin
    {
        public string Name => "BeatSaberCustomExit";
        public string Version => "1.0.1";

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

        private void SceneManagerOnActiveSceneChanged(Scene from, Scene to)
        {
            Console.WriteLine($"[BeatSaberCustomExit] Active scene changed from \"{from.name}\" to \"{to.name}\"");

            if (from.name == "EmptyTransition" && to.name.Contains("Menu"))
            {
                try
                {
                    SettingsUI.Instance.OnLoad();
                    if (PluginConfig.EnablePlugin)
                        MainMenuUIOverload.Instance.OnLoad();
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
