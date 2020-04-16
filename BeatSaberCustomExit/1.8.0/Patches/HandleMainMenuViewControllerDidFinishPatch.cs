using System;
using CustomExit.Utilities;
using HarmonyLib;
using UnityEngine;

namespace CustomExit.Patches
{
    [HarmonyPatch(typeof(MainFlowCoordinator))]
    [HarmonyPatch("HandleMainMenuViewControllerDidFinish")]
    [HarmonyPatch(new Type[] { typeof(MainMenuViewController), typeof(MainMenuViewController.MenuButton) })]
    class HandleMainMenuViewControllerDidFinishPatch
    {
        static bool Prefix(MainMenuViewController viewController, MainMenuViewController.MenuButton subMenuType)
        {
            if (subMenuType == MainMenuViewController.MenuButton.Quit)
            {
                if (PluginConfig.Instance.EnablePlugin && PluginConfig.Instance.AskWhenQuitting)
                    MainMenuUIOverload.Instance.ShowConfirmQuitPanel(MainMenuUIOverload.PanelState.Quit);
                else
                    Application.Quit();
                return false;
            }
            return true;
        }
    }
}