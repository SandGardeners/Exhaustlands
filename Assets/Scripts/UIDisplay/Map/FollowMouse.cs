using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour 
{
    public RectTransform boundaries;
    public RectTransform imageToAdjust;

    Vector2 positionPercentage;
	Vector2 localPoint;
    void Update () 
	{
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint))
		{
			if(RectTransformUtility.RectangleContainsScreenPoint(boundaries, Input.mousePosition))
			{
            	GetComponent<RectTransform>().anchoredPosition = (localPoint);
                imageToAdjust.anchoredPosition = -localPoint + (Vector2.zero - localPoint/ 2.0f);
            }
        }	
	}

	void LateUpdate()
	{
    }
}
