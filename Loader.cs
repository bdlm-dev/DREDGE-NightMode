using Winch.Core.API;
using UnityEngine;
using Winch.Core;
using HarmonyLib;
using NightMode.Util;

namespace NightMode;

public class Loader
{
    public static void Initialize()
    {
        new Harmony("mmbluey.nightmode").PatchAll();

        DredgeEvent.OnGameLoading += UpdateConfigData;
    }

    private static void UpdateConfigData(GameSceneInitializer init)
    {
        GameConfigData data = GameManager.Instance.GameConfigData;

        // daySanityModifier -> nightSanityModifier
        data.GetType().GetField("daySanityModifier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GameManager.Instance.GameConfigData, data.NightSanityModifier);
        // specialSpotChanceDay -> specialSpotChanceNight
        data.GetType().GetField("specialSpotChanceDay", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GameManager.Instance.GameConfigData, data.SpecialSpotChanceNight);
        // baseAberrationSpawnChance -> nightAberrationSpawnChance
        data.GetType().GetField("baseAberrationSpawnChance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GameManager.Instance.GameConfigData, data.NightAberrationSpawnChance);

        Gradient darkGradient = new Gradient();
        darkGradient.alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(0, 0), new GradientAlphaKey(0, 1) };
        darkGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(new Color(0, 0, 0), 0), new GradientColorKey(new Color(0, 0, 0), 1) };
        GameManager.Instance.Time.GetType().GetField("sunColour", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GameManager.Instance.Time, darkGradient);
        GameManager.Instance.Time.GetType().GetField("ambientLightColor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(GameManager.Instance.Time, darkGradient);

        GameObject lightParent = new GameObject("DirectionalLightParent");
        lightParent.transform.localEulerAngles = new Vector3(0, 90, 0);
        GameObject.Find("Directional Light").transform.parent = lightParent.transform;

        SkyboxUtil.load();
        
        WinchCore.Log.Debug("Successfully updated fields.");
    }
}