using BeatSaverCustomExit.Settings;
using CustomUI.BeatSaber;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BeatSaberCustomExit
{
    class MainMenuUIOverload : MonoBehaviour
    {
        private CustomMenu _CustomMenu;
        private CustomViewController _CustomViewController;

        public bool Initialized = false;
        public bool ConfirmExitDialogOpened = false;

        private static MainMenuUIOverload _instance = null;
        public static MainMenuUIOverload Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = new GameObject("[BeatSaverCustomExit] MainMenuUIOverload").AddComponent<MainMenuUIOverload>();
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

        private void _SetupConfirmQuitPanel()
        {
            _CustomMenu = BeatSaberUI.CreateCustomMenu<CustomMenu>("Quit");
            _CustomViewController = BeatSaberUI.CreateViewController<CustomViewController>();

            if (_CustomMenu != null && _CustomViewController != null)
            {
                _CustomViewController.didDeactivateEvent += (deactivationType) => { ConfirmExitDialogOpened = false; };
                _CustomMenu.SetMainViewController(_CustomViewController, false, (firstActivation, type) =>
                {
                    if (firstActivation && type == VRUI.VRUIViewController.ActivationType.AddedToHierarchy)
                    {
                        var topText = _CustomViewController.CreateText(PluginConfig.TextContent, new Vector2(0, 10f), new Vector2(105f, 10));
                        if (topText != null)
                        {
                            topText.alignment = TMPro.TextAlignmentOptions.Center;
                            topText.fontSize = 6;
                        }
                        else
                            Console.WriteLine("[BeatSaberCustomExit.MainMenuUIOverload]: 'topText' was null.");

                        var buttonCancel = _CustomViewController.CreateUIButton("CreditsButton", new Vector2(-20f, -20f), new Vector2(30f, 10f),
                                                                                delegate () { _CustomMenu.Dismiss(); }, "Cancel");
                        var buttonConfirm = _CustomViewController.CreateUIButton("CreditsButton", new Vector2(20, -20f), new Vector2(30f, 10f),
                                                                                    delegate () { Application.Quit(); }, "Confirm");
                        if (buttonCancel != null)
                            buttonCancel.ToggleWordWrapping(false);
                        else
                            Console.WriteLine("[BeatSaberCustomExit.MainMenuUIOverload]: 'buttonCancel' was null.");

                        if (buttonConfirm != null)
                            buttonConfirm.ToggleWordWrapping(false);
                        else
                            Console.WriteLine("[BeatSaberCustomExit.MainMenuUIOverload]: 'buttonConfirm' was null.");
                    }
                });
            } else
                Console.WriteLine("[BeatSaberCustomExit.MainMenuUIOverload]: '_CustomMenu' or '_CustomViewController' was null.");
        }

        public void ShowConfirmQuitPanel()
        {
            if (!ConfirmExitDialogOpened && _CustomMenu != null)
                _CustomMenu.Present(false);
        }
        
        private void SetupUI()
        {
            if (Initialized) return;
            
            _SetupConfirmQuitPanel();

            Initialized = true;
        }
    }
}
