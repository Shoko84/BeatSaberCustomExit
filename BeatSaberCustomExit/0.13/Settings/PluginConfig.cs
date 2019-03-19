using IllusionPlugin;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BeatSaverCustomExit.Settings
{
    public class PluginConfig
    {
        public static bool EnablePlugin = true;
        public static bool AskWhenQuitting = true;
        public static bool AskWhenRestarting = true;
        public static string QuitTextContent = "Are you really sure to quit <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF> ?";
        public static string RestartTextContent = "Are you really sure to restart <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF> ?";

        public static void LoadOrCreateConfig()
        {
            if (!Directory.Exists("UserData"))
                Directory.CreateDirectory("UserData");

            //EnablePlugin property
            if (!ModPrefs.HasKey("BeatSaverCustomExit", "EnablePlugin"))
            {
                ModPrefs.SetBool("BeatSaverCustomExit", "EnablePlugin", true);
                Console.WriteLine("Created config");
            }
            else
                EnablePlugin = ModPrefs.GetBool("BeatSaverCustomExit", "EnablePlugin", true, true);

            //AskWhenQuitting property
            if (!ModPrefs.HasKey("BeatSaverCustomExit", "AskWhenQuitting"))
            {
                ModPrefs.SetBool("BeatSaverCustomExit", "AskWhenQuitting", true);
                Console.WriteLine("Created config");
            }
            else
                AskWhenQuitting = ModPrefs.GetBool("BeatSaverCustomExit", "AskWhenQuitting", true, true);

            //AskWhenRestarting property
            if (!ModPrefs.HasKey("BeatSaverCustomExit", "AskWhenRestarting"))
            {
                ModPrefs.SetBool("BeatSaverCustomExit", "AskWhenRestarting", true);
                Console.WriteLine("Created config");
            }
            else
                AskWhenRestarting = ModPrefs.GetBool("BeatSaverCustomExit", "AskWhenRestarting", true, true);

            //QuitTextContent property
            if (!ModPrefs.HasKey("BeatSaverCustomExit", "QuitTextContent"))
            {
                ModPrefs.SetString("BeatSaverCustomExit", "QuitTextContent", "Are you really sure to quit <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF> ?");
                Console.WriteLine("Created config");
            }
            else
                QuitTextContent = ModPrefs.GetString("BeatSaverCustomExit", "QuitTextContent", "Are you really sure to quit <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF> ?", true);

            //RestartTextContent property
            if (!ModPrefs.HasKey("BeatSaverCustomExit", "RestartTextContent"))
            {
                ModPrefs.SetString("BeatSaverCustomExit", "RestartTextContent", "Are you really sure to restart <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF> ?");
                Console.WriteLine("Created config");
            }
            else
                RestartTextContent = ModPrefs.GetString("BeatSaverCustomExit", "RestartTextContent", "Are you really sure to restart <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF> ?", true);
        }

        public static void SaveConfig()
        {
            ModPrefs.SetBool("BeatSaverCustomExit", "EnablePlugin", EnablePlugin);
            ModPrefs.SetBool("BeatSaverCustomExit", "AskWhenQuitting", AskWhenQuitting);
            ModPrefs.SetBool("BeatSaverCustomExit", "AskWhenRestarting", AskWhenRestarting);
            ModPrefs.SetString("BeatSaverCustomExit", "QuitTextContent", QuitTextContent);
            ModPrefs.SetString("BeatSaverCustomExit", "RestartTextContent", RestartTextContent);
        }
    }
}