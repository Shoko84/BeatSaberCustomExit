using BeatSaverCustomExit.Settings;
using CustomUI.BeatSaber;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BeatSaberCustomExit
{
    class MainMenuUIOverload : MonoBehaviour
    {
        private CustomMenu _CustomMenu;
        private CustomViewController _CustomViewController;

        private TextMeshProUGUI _TopText;
        private Button _CancelButton;
        private Button _ConfirmButton;
        private PanelState _CurrentPanelState = PanelState.QUIT;

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

        public enum PanelState : int
        {
            QUIT = 0,
            RESTART = 1
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
                        _TopText = _CustomViewController.CreateText(PluginConfig.QuitTextContent, new Vector2(0, 10f), new Vector2(105f, 10));
                        if (_TopText != null)
                        {
                            _TopText.alignment = TMPro.TextAlignmentOptions.Center;
                            _TopText.fontSize = 6;
                        }
                        else
                            Console.WriteLine("[BeatSaberCustomExit.MainMenuUIOverload]: 'topText' was null.");

                        _CancelButton = _CustomViewController.CreateUIButton("CreditsButton", new Vector2(-20f, -20f), new Vector2(30f, 10f),
                                                                                delegate () { _CustomMenu.Dismiss(); }, "Cancel");
                        _ConfirmButton  = _CustomViewController.CreateUIButton("CreditsButton", new Vector2(20, -20f), new Vector2(30f, 10f),
                                                                                    delegate () { Plugin.QuitGame(); }, "Quit");
                        if (_CancelButton != null)
                            _CancelButton.ToggleWordWrapping(false);
                        else
                            Console.WriteLine("[BeatSaberCustomExit.MainMenuUIOverload]: 'buttonCancel' was null.");

                        if (_ConfirmButton != null)
                            _ConfirmButton.ToggleWordWrapping(false);
                        else
                            Console.WriteLine("[BeatSaberCustomExit.MainMenuUIOverload]: 'buttonConfirm' was null.");
                    }
                });
            } else
                Console.WriteLine("[BeatSaberCustomExit.MainMenuUIOverload]: '_CustomMenu' or '_CustomViewController' was null.");
        }

        private void _ChangeForPanelQuitting()
        {
            _ConfirmButton.GetComponentInChildren<TMP_Text>().text = "Quit";
            _ConfirmButton.onClick.RemoveAllListeners();
            _ConfirmButton.onClick.AddListener(Plugin.QuitGame);
            _TopText.text = PluginConfig.QuitTextContent;
        }

        private void _ChangeForPanelRestarting()
        {
            _ConfirmButton.GetComponentInChildren<TMP_Text>().text = "Restart";
            _ConfirmButton.onClick.RemoveAllListeners();
            _ConfirmButton.onClick.AddListener(Plugin.RestartGame);
            _TopText.text = PluginConfig.RestartTextContent;
        }

        public void ShowConfirmQuitPanel(PanelState state)
        {
            if (!ConfirmExitDialogOpened && _CustomMenu != null)
            {
                _CustomMenu.Present(false);
                if (_CurrentPanelState != state)
                {
                    switch (state)
                    {
                        case PanelState.QUIT:
                            _ChangeForPanelQuitting();
                            break;
                        case PanelState.RESTART:
                            _ChangeForPanelRestarting();
                            break;
                    }
                    _CurrentPanelState = state;
                }
            }
        }
        
        private void SetupUI()
        {
            if (Initialized) return;
            
            _SetupConfirmQuitPanel();

            Initialized = true;
        }
    }
}
