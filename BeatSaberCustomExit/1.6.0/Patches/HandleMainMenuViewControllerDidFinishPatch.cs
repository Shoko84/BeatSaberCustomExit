using Harmony;
using System;
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
                if (Plugin.config.Value.EnablePlugin && Plugin.config.Value.AskWhenQuitting)
                    MainMenuUIOverload.Instance.ShowConfirmQuitPanel(MainMenuUIOverload.PanelState.Quit);
                else
                    Application.Quit();
                return false;
            }
            return true;
        }
    }
}