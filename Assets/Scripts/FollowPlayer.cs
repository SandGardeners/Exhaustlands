using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowPlayer : MonoBehaviour 
{
	public float posY;	
	// Update is called once per frame
	void Update () 
	{
		Vector3 ok = transform.position;
		ok.y = posY;
		transform.position = ok;	
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 1f);	
	}
}
