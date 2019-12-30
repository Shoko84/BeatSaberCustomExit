using System;
using System.Linq;
using CustomExit.Controllers;
using BeatSaberMarkupLanguage;
using BS_Utils.Utilities;
using UnityEngine;

namespace CustomExit
{
    internal class MainMenuUIOverload : MonoBehaviour
    {
        public enum PanelState
        {
            Unknown,
            Quit,
            Restart
        }

        #region Properties

        public bool                Initialized             { get; private set; }
        public bool                ConfirmExitDialogOpened { get; private set; }
        public ExitPanelController ExitPanelController     { get; private set; }
        public PanelState          CurrentPanelState       { get; private set; } = PanelState.Unknown;

        private static MainMenuUIOverload _instance;
        public static MainMenuUIOverload Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = new GameObject("[BeatSaverCustomExit] MainMenuUIOverload").AddComponent<MainMenuUIOverload>();
                DontDestroyOnLoad(_instance.gameObject);
                return _instance;
            }
        }

        #endregion

        #region Methods

        public void OnLoad()
        {
            Initialized = false;
            SetupUI();
        }

        private void SetupUI()
        {
            if (Initialized) return;
            SetupConfirmQuitPanel();
            Initialized = true;
        }

        private void SetupConfirmQuitPanel()
        {
            ExitPanelController = BeatSaberUI.CreateViewController<ExitPanelController>();

            if (ExitPanelController != null)
                ExitPanelController.didDeactivateEvent += deactivationType => { ConfirmExitDialogOpened = false; };
            else
                Logger.log.Error("[CustomExit.MainMenuUIOverload]: 'ExitPanelController' was null.");
        }

        private void ChangeForPanelQuitting()
        {
            if (ExitPanelController == null) return;
            if (ExitPanelController.confirmButton != null)
                ExitPanelController.confirmButton.SetButtonText("Quit");
            if (ExitPanelController.headerText != null)
                ExitPanelController.headerText.text = "Quit";
            if (ExitPanelController.topText != null)
                ExitPanelController.topText.text = Plugin.config.Value.QuitTextContent;
        }

        private void ChangeForPanelRestarting()
        {
            if (ExitPanelController == null) return;
            if (ExitPanelController.confirmButton != null)
                ExitPanelController.confirmButton.SetButtonText("Restart");
            if (ExitPanelController.headerText != null)
                ExitPanelController.headerText.text = "Restart";
            if (ExitPanelController.topText != null)
                ExitPanelController.topText.text = Plugin.config.Value.RestartTextContent;
        }

        public void ShowConfirmQuitPanel(PanelState state)
        {
            if (ConfirmExitDialogOpened) return;
            CurrentPanelState = state;
            Resources.FindObjectsOfTypeAll<MainFlowCoordinator>().First().InvokeMethod("PresentViewController", new object[] { ExitPanelController, null, false });
            switch (state)
            {
                case PanelState.Quit:
                    ChangeForPanelQuitting();
                    break;
                case PanelState.Restart:
                    ChangeForPanelRestarting();
                    break;
                case PanelState.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        #endregion
    }
}
