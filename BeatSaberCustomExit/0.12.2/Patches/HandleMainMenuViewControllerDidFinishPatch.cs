﻿using BeatSaberCustomExit;
using BeatSaverCustomExit.Settings;
using Harmony;
using System;
using UnityEngine;

namespace BeatSaberCustomExit.Patches
{
    [HarmonyPatch(typeof(MainFlowCoordinator), "HandleMainMenuViewControllerDidFinish",
    new Type[] { typeof(MainMenuViewController), typeof(MainMenuViewController.MenuButton) })]
    class HandleMainMenuViewControllerDidFinishPatch
    {
        public static bool Prefix(MainMenuViewController viewController, MainMenuViewController.MenuButton subMenuType)
        {
            if (subMenuType == MainMenuViewController.MenuButton.Quit)
            {
                if (PluginConfig.EnablePlugin)
                    MainMenuUIOverload.Instance.ShowConfirmQuitPanel();
                else
                    Application.Quit();
                return false;
            }
            return true;
        }
    }
}