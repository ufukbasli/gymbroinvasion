using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CubeInterface))]
public class ECubeInterface : Editor
{


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var go = (CubeInterface)target;
        if(GUILayout.Button("place points"))
        {
            //go.DistributeCubes();
            var tr = go.transform;
            if (tr.childCount > 0)
            {
                tr.GetChild(0).localPosition = new Vector3(go.edgeOffset, 0, 0);
            }
            if (tr.childCount > 1)
            {
                tr.GetChild(1).localPosition = new Vector3(-go.edgeOffset, 0, 0);
            }
            if (tr.childCount > 2)
            {
                tr.GetChild(2).localPosition = new Vector3(0, 0, go.edgeOffset);
            }
            if (tr.childCount > 3)
            {
                tr.GetChild(3).localPosition = new Vector3(0, 0, -go.edgeOffset);
            }
        }
    }
}
