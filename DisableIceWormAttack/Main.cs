using HarmonyLib;
using QModManager.API.ModLoading;
using System.Reflection;
using Logger = QModManager.Utility.Logger;

namespace DisableIceWormAttack
{
    [QModCore]
    public static class Main
    {
        [QModPatch]
        public static void Patch()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "com.disablewormattack.psmod");
            Logger.Log(Logger.Level.Info, "Disable Worm Attack Mod Patched!");
        }
    }

    [HarmonyPatch(typeof(IceWormHuntModeTrigger), nameof(IceWormHuntModeTrigger.OnPlayerEnter))]
    internal static class IceWormHuntModeTrigger_Override
    {
        [HarmonyPrefix]
        internal static bool Prefix()
        {
            return false;
        }
    }
}
