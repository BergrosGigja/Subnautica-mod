using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options;
using SMLHelper.V2.Options.Attributes;
using System.Reflection;
using Logger = QModManager.Utility.Logger;

namespace DisableIceWormAttack
{
    [QModCore]
    public static class Main
    {
        internal static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();

        [QModPatch]
        public static void Patch()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "com.disablewormattack.psmod");
            Logger.Log(Logger.Level.Info, "Disable Worm Attack Mod Patched!");
        }
    }

    [Menu("Options Menu")]
    public class Config : ConfigFile
    {
        [Toggle("Ice Worm Attack Disabled"), OnChange(nameof(MyCheckboxToggleEvent))]
        public bool ToggleValue = false;

        private void MyCheckboxToggleEvent(ToggleChangedEventArgs e)
        {
            Logger.Log(Logger.Level.Info, $"Ice Worm Attack Disabled: {e.Value}");
        }
    }

    [HarmonyPatch(typeof(IceWormHuntModeTrigger), nameof(IceWormHuntModeTrigger.OnPlayerEnter))]
    internal static class IceWormHuntModeTrigger_Override
    {
        [HarmonyPrefix]
        internal static bool Prefix()
        {
            return !(Main.config.ToggleValue);
        }
    }
}
