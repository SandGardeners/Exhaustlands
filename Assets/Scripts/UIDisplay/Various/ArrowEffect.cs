using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = startScale * 1.25f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = startScale; 

    }

	Vector3 startScale;
    // Use this for initialization
    void Start () 
	{
		startScale = transform.localScale;	
	}
	
}
