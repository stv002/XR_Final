using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightmapSwitcher : MonoBehaviour
{

    public Texture2D[] darkLightmapDir, darkLightmapColor, darkLightmapShadow;
    public Texture2D[] brightLightmapDir, brightLightmapColor, brightLightmapShadow;

    private LightmapData[] darkLightmap, brightLightmap;

    void Start()
    {
        List<LightmapData> dlightmap = new List<LightmapData>();
        List<LightmapData> blightmap = new List<LightmapData>();    

        for (int i = 0; i < darkLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData();

            lmdata.lightmapDir = darkLightmapDir[i];
            lmdata.lightmapColor = darkLightmapColor[i];

            if (darkLightmapShadow.Length > i)
                lmdata.shadowMask = darkLightmapShadow[i];

            dlightmap.Add(lmdata);
        }

        darkLightmap = dlightmap.ToArray();

        for (int i = 0; i < brightLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData();

            lmdata.lightmapDir = brightLightmapDir[i];
            lmdata.lightmapColor = brightLightmapColor[i];
            lmdata.shadowMask = brightLightmapShadow[i];

            blightmap.Add(lmdata);
        }

        brightLightmap = dlightmap.ToArray();

    }

    public void LightsOn()
    {
        LightmapSettings.lightmaps = brightLightmap;
    }

    public void LightsOff()
    {
        LightmapSettings.lightmaps = darkLightmap;
    }
}
