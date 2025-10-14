using UnityEngine;


namespace CamGlitchKit
{

    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class GlitchEffect : MonoBehaviour
    {
        public enum GlitchType
        {
            Classic, Digital, Analog, Noise, DataMosh, Electric, Pixelate, VHS, ScreenTear,
            LiquidDisplace, Thermal, Hologram,
            GlitchOverlay
        }
        public GlitchType glitchType = GlitchType.Classic;

        public Material glitchMaterial;
        [Range(0, 1)] public float intensity = 0.5f;
        public float distortion = 10f;

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (glitchMaterial != null)
            {
                glitchMaterial.SetInt("_EffectType", (int)glitchType);
                glitchMaterial.SetFloat("_Intensity", intensity);
                glitchMaterial.SetFloat("_Distortion", distortion);
                Graphics.Blit(src, dest, glitchMaterial);
            }
            else
            {
                Graphics.Blit(src, dest);
            }
        }
    }
}
