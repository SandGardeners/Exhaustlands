using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CopyCamera : MonoBehaviour {

    Camera parentCam;
    Camera copyCam;
	void OnEnable()
	{
        copyCam = GetComponent<Camera>();
        parentCam = transform.parent.GetComponent<Camera>();
    }
	void LateUpdate () 
	{
        if(parentCam.cullingMask != -1)
		{
            copyCam.cullingMask = 512;
            copyCam.orthographicSize = parentCam.orthographicSize;
		}
		else
            copyCam.cullingMask = 0;
    }
}
