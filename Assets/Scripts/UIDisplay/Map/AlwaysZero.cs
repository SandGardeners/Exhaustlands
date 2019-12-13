using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AlwaysZero : MonoBehaviour 
{
    public Transform model;
    
	// Update is called once per frame
    
	void LateUpdate() 
	{
		if(model != null)
		{
        	transform.position = model.position;
		}
    }

}
