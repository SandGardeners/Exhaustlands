using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour {

    public float speed = 1.0f;
    // Use this for initialization
	
	// Update is called once per frame
	void Update () 
	{
        Vector3 mouseToPerso = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        transform.position += (mouseToPerso.normalized * speed * Time.deltaTime * Mathf.Lerp(1.0f, 10.0f, mouseToPerso.magnitude/Camera.main.orthographicSize));
    }
}
