using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZFrame.UGUI
{
    /// <summary>
    /// 截屏脚本
    /// </summary>
    public class UIScreenshot : RawImage
    {
        private void UIPostRender(Camera cam)
        {
            if (cam.CompareTag(TAGS.MainCamera)) {
                var tex2d = new Texture2D(cam.pixelWidth, cam.pixelHeight, TextureFormat.RGB24, false);
                tex2d.ReadPixels(new Rect(0, 0, cam.pixelWidth, cam.pixelHeight), 0, 0);
                tex2d.Apply();
                texture = tex2d;
                color = Color.white;
                Camera.onPostRender -= UIPostRender;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif
            color = Color.clear;
            Camera.onPostRender += UIPostRender;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            //Destroy(texture);
            //Destroy may not be called from edit mode! Use DestroyImmediate instead
            DestroyImmediate(texture);
        }
    }
}
