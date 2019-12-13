using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PlaceInteractive))]
public class CustomPlaceEditor : Editor 
{
	
	TriggerAnim tg = null;
	TMPro.TMP_Text pn = null;
    public override void OnInspectorGUI()
    {
		
        PlaceInteractive myTarget = (PlaceInteractive)target;

		myTarget.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

		if(tg == null)
		{
			tg = myTarget.transform.parent.GetComponentInChildren<TriggerAnim>();  
		}
		if(tg != null)
		{
			tg.fullName = EditorGUILayout.TextField("Full display name", tg.fullName);
			myTarget.transform.parent.name = tg.fullName;
		}

		myTarget.knotData = EditorGUILayout.TextField("Ink knot", myTarget.knotData);
		
		if (GUI.changed)
        {
			EditorUtility.SetDirty(target);
			EditorUtility.SetDirty(myTarget.transform.parent.gameObject);
			EditorUtility.SetDirty(tg);
        }

    }
}

