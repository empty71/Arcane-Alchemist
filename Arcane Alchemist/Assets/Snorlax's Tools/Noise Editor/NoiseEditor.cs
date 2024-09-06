#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Snorlax.Procedural.Noise
{
    [CustomEditor(typeof(Noise))]
    public class NoiseEditor : Editor
    {
        Texture2D PreviewMap;
        float PrevWidth;
        Vector2Int pictureSize = Vector2Int.zero;
        Noise noise;
        public override void OnInspectorGUI()
        {
            noise = (Noise)target;

            if (PreviewMap == null || PrevWidth != EditorGUIUtility.currentViewWidth)
            {
                PrevWidth = EditorGUIUtility.currentViewWidth;
                PreviewMap = noise.GenerateTexture2D((int)EditorGUIUtility.currentViewWidth, 200, noise.drawMode);
                Repaint();
            }

            EditorGUI.DrawPreviewTexture(new Rect(0, 0, EditorGUIUtility.currentViewWidth, 200), PreviewMap);

            EditorGUILayout.Space(200);

            base.OnInspectorGUI();

            if (GUI.changed)
            {
                PreviewMap = null;
            }

            EditorGUILayout.Space(20);

            pictureSize = EditorGUILayout.Vector2IntField("Texture Size", pictureSize);

            if (GUILayout.Button("Generate Texture"))
            {
                 var path = EditorUtility.SaveFilePanel("Save texture as PNG", "Assets/", "NewNoise.png", "png");

                if (path.Length != 0)
                {
                    var pngData = noise.GenerateTexture2D(pictureSize.x, pictureSize.y, noise.drawMode).EncodeToPNG();
                    if (pngData != null)
                        File.WriteAllBytes(path, pngData);
                    AssetDatabase.Refresh();
                } 
            }
        }
    }
}
#endif