using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class ExactBackground : MonoBehaviour {

    RectTransform _mine;
    RectTransform[] _theirs;
    Vector2 startSize;
    // Use this for initialization
    void Start () 
	{
        _mine = GetComponent<RectTransform>();
        
        startSize = _mine.sizeDelta;
    }
	
	// Update is called once per frame
	void Update () 
	{
        _theirs = GetComponentsInChildren<RectTransform>();
        Vector2 sd = new Vector2(startSize.x, 0f);
        foreach(RectTransform rt in _theirs)
        {
            if(rt != _mine)
            {
                if(rt.sizeDelta.x > sd.x)
                    sd.x = rt.sizeDelta.x;
                sd.y += rt.sizeDelta.y;
            }
        }
        _mine.sizeDelta = sd;
    }
}
