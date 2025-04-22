using UnityEngine;
using Winch.Core;
using Winch.Util;

namespace NightMode.Util;

internal class SkyboxUtil
{
    public static void load()
    {
        Texture2D? skyboxTexture = TextureUtil.GetTexture("skybox_new_0");



        // ((Dictionary<string, Texture2D>)typeof(TextureUtil).GetField("TextureMap", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(null)).Values.ForEach((k, v) => { WinchCore.Log.Debug(k); WinchCore.Log.Debug(v); });

        if (skyboxTexture == null)
        {
            WinchCore.Log.Debug("Couldn't find skybox texture.");
            return;
        }

        skyboxTexture.wrapMode = TextureWrapMode.Clamp;
        skyboxTexture.filterMode = FilterMode.Bilinear;

        Shader shader = Shader.Find("Sprites/Default");

        if (shader == null) 
        {
            WinchCore.Log.Debug("Couldn't find shader.");
            return;
        }

        Material newMat = new Material(shader);
        newMat.mainTexture = skyboxTexture;

        RenderSettings.skybox = newMat;
    }
}
