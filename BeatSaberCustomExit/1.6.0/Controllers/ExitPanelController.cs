using System;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.ViewControllers;
using BS_Utils.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CustomExit.Controllers
{
    public class ExitPanelController : BSMLResourceViewController
    {
        public override string ResourceName => "CustomExit.Views.exit-panel.bsml";

        [UIParams]
        private BSMLParserParams parserParams;

        [UIComponent("header")] public TextMeshProUGUI headerText;
        [UIComponent("top-text")] public TextMeshProUGUI topText;
        [UIComponent("confirm-btn")] public Button confirmButton;

        [UIAction("cancel-btn-click")]
        private void CancelButtonAction()
        {
            Resources.FindObjectsOfTypeAll<MainFlowCoordinator>().First().InvokeMethod("DismissViewController", new object[] { this, null, false });
        }

        [UIAction("confirm-btn-click")]
        private void ConfirmButtonAction()
        {
            switch (MainMenuUIOverload.Instance.CurrentPanelState) {
                case MainMenuUIOverload.PanelState.Quit:
                    Plugin.QuitGame();
                    break;
                case MainMenuUIOverload.PanelState.Restart:
                    Plugin.RestartGame();
                    break;
                case MainMenuUIOverload.PanelState.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [UIAction("#apply")]
        public void OnApply()
        {

        }

        public void Update()
        {

        }
    }
}
