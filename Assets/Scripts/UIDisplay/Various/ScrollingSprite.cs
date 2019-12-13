using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingSprite : MonoBehaviour {

    float spriteWidth;
    public float scrollingSpeed = 1.0f;
    float cameraHalfWidth;
    void Start () 
	{	
		 Camera cam = Camera.main;
	     float halfHeight = cam.orthographicSize;
    	 cameraHalfWidth = halfHeight * cam.aspect;
        spriteWidth = GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
    }
	
	// Update is called once per frame
	void Update () 
	{
		transform.localPosition += (Vector3)(Vector2.left * Time.deltaTime * scrollingSpeed);	
		if(transform.localPosition.x <= -(cameraHalfWidth + spriteWidth))
		{
            Vector3 newPos = transform.localPosition;
            newPos.x *= -1f;
            transform.localPosition = newPos;
        }
	}
}
