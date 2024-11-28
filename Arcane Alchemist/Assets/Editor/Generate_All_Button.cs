using UnityEngine;
using System.Collections;
using UnityEditor;


public class Generate_All_Button : Editor
{


		public override void OnInspectorGUI()
		{
			MapDisplay mapPreview = (MapDisplay)target;
			TerrainGenerator terrainGenerator = (TerrainGenerator)target;
			

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
