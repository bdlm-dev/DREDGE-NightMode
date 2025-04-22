using HarmonyLib;

using UnityEngine;

namespace NightMode.Patches;

[HarmonyPatch(typeof(FogController), "Start")]
public class FogControllerPatcher
{
    public static void Postfix(FogController __instance)
    {
        AnimationCurve newAnimCurve = new AnimationCurve();
        newAnimCurve.AddKey(0, 0.9f);
        newAnimCurve.AddKey(1, 0.9f);
        __instance.defaultFogDensityOverDay = newAnimCurve;

        Gradient newGradient = new Gradient();
        newGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(new Color(0, 0, 0), 0), new GradientColorKey(new Color(0, 0, 0), 1) };
        newGradient.alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1) };
        __instance.defaultFogColorOverDay = newGradient;

        UnityEngine.Object.FindObjectsOfType<FogPropertyModifier>().ToList().ForEach(modifier =>
        {
            modifier.fogProperty.fogDensityOverDay = newAnimCurve;
            modifier.fogProperty.fogColorOverDay.colorKeys.ForEach(colorKey =>
            {
                colorKey.color = new Color(colorKey.color.r / 3, colorKey.color.g / 3, colorKey.color.b / 3);
            });
        });
    }
}
