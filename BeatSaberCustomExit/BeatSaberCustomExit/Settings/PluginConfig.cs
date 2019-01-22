using IllusionPlugin;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BeatSaverDownloader.Misc
{
    public class PluginConfig
    {
        public static bool EnablePlugin = true;
        public static string TextContent = "Are you really sure to quit <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF> ?";

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

            //TextContent property
            if (!ModPrefs.HasKey("BeatSaverCustomExit", "TextContent"))
            {
                ModPrefs.SetString("BeatSaverCustomExit", "TextContent", "Are you really sure to quit <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF> ?");
                Console.WriteLine("Created config");
            }
            else
                TextContent = ModPrefs.GetString("BeatSaverCustomExit", "TextContent", "Are you really sure to quit <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF> ?", true);
        }

        public static void SaveConfig()
        {
            ModPrefs.SetBool("BeatSaverCustomExit", "EnablePlugin", EnablePlugin);
            ModPrefs.SetString("BeatSaverCustomExit", "TextContent", TextContent);
        }
    }
}