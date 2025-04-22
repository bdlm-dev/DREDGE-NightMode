using HarmonyLib;

namespace NightMode.Patches;

[HarmonyPatch(typeof(TimeController))]
[HarmonyPatch("RecalculateSceneLightness")]
public class TimeControllerLightnessPatcher
{
    public static bool Prefix(ref float __result)
    {
        __result = 1f;
        return true;
    }
}