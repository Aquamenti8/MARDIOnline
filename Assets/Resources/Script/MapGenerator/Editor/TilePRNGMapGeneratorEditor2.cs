using UnityEngine;
using System.Collections;
using UnityEditor;
[System.Serializable]

[CustomEditor(typeof(TilePRNGMapGenerator2))]
public class TilePRNGMapGeneratorEditor2 : Editor
{

    public override void OnInspectorGUI()
    {



        TilePRNGMapGenerator2 map = target as TilePRNGMapGenerator2;
        if (DrawDefaultInspector())
        {
            if (map.autoUpdate)
            {
                map.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
        }
    }
}
