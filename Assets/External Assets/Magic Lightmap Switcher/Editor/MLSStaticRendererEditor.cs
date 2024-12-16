using UnityEditor;
using UnityEngine;

namespace MagicLightmapSwitcher
{
    [CustomEditor(typeof(MLSStaticRenderer))]
    public class MLSStaticRendererEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MLSStaticRenderer staticRenderer = (MLSStaticRenderer)target;

            MeshRenderer meshRenderer = staticRenderer.GetComponent<MeshRenderer>();
            Terrain terrain = staticRenderer.GetComponent<Terrain>();

            if (meshRenderer != null)
            {
                if (meshRenderer.scaleInLightmap == 0)
                {
                    DestroyImmediate(staticRenderer.GetComponent<MLSStaticRenderer>());
                }
            }

            if (terrain != null)
            {
                if (terrain.lightmapScaleOffset.x == 0 || terrain.lightmapScaleOffset.y == 0)
                {
                    DestroyImmediate(staticRenderer.GetComponent<MLSStaticRenderer>());
                }
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("Script GUID");
                GUILayout.Label(staticRenderer.scriptId);
            }
            
#if BAKERY_INCLUDED
            if (staticRenderer.switcherInstance.currentLightmapScenario != null)
            {
                if ((staticRenderer.switcherInstance.currentLightmapScenario.blendingModules & (1 << 4)) > 0)
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label(
                            MLSTooltipManager.MainComponent.GetParameter("Bakery Volumes Synch.",
                                MLSTooltipManager.MainComponent.Tabs.LightmapScenario),
                            GUILayout.MinWidth(200));
                        staticRenderer.bakeryVolumesBlendingSynch =
                            (MLSObject.BakeryVolumesBlendingSynch) EditorGUILayout.EnumPopup(staticRenderer
                                .bakeryVolumesBlendingSynch);

                        switch (staticRenderer.bakeryVolumesBlendingSynch)
                        {
                            case MLSObject.BakeryVolumesBlendingSynch.Reflections:
                                staticRenderer.propertyBlock.SetInt("_MLS_BAKERY_VOLUMES_SYNCH", 0);
                                break;

                            case MLSObject.BakeryVolumesBlendingSynch.Lightmaps:
                                staticRenderer.propertyBlock.SetInt("_MLS_BAKERY_VOLUMES_SYNCH", 1);
                                break;
                        }

                        staticRenderer.meshRenderer.SetPropertyBlock(staticRenderer.propertyBlock);
                    }
                }
            }
#endif

            if (GUILayout.Button("Update GUID"))
            {
                staticRenderer.UpdateGUID();
            }
        }
    }
}
