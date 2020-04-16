using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace CustomExit.Utilities
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public bool RegenerateConfig = true;
        public bool EnablePlugin = true;
        public bool AskWhenQuitting = true;
        public bool AskWhenRestarting = true;
        public string QuitTextContent = "Are you really sure to quit <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF>?";
        public string RestartTextContent = "Are you really sure to restart <b><color=#FF6060>Beat <color=#00B0FF>Saber</b><color=#FFFFFF>?";
    }
}