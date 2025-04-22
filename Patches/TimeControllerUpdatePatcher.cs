using HarmonyLib;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace NightMode.Patches;

[HarmonyPatch(typeof(TimeController))]
[HarmonyPatch("Update")]
internal class TimeControllerUpdatePatcher
{
    public static void Postfix(TimeController __instance)
    {
        Shader.SetGlobalFloat("_TimeOfDay", 0.95f);

        __instance.directionalLight.transform.eulerAngles = new Vector3(330f, -90f, 0f);
    }
}
