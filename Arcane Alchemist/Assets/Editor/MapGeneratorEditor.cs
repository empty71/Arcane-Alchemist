 using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapDisplay))]
public class MapPreviewEditor : Editor
{

	public override void OnInspectorGUI()
	{
		MapDisplay mapPreview = (MapDisplay)target;

		if (DrawDefaultInspector())
		{
			if (mapPreview.autoUpdate)
			{
				mapPreview.DrawMapInEditor();
			}
		}

		if (GUILayout.Button("Generate"))
		{
			mapPreview.DrawMapInEditor();
		}
	}
}