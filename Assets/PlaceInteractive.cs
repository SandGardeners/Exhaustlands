using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
public class PlaceInteractive : DisplayInteractive 
{
	public TMP_Text placeName;
	TriggerAnim tg;

	bool canDo;
	
	void CanDo(bool b)
	{
		canDo = b;
	}
	
	void Highlight(bool b)
	{
		placeName.DOColor(b?Color.grey:Color.white, 0.15f);
		
	} 
	protected override void Start()
	{
		base.Start();
		tg = GetComponentInChildren<TriggerAnim>();
		tg.activated = CanDo;
		placeName = GetComponent<TMP_Text>();
	}

	 public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if(canDo)
        {
            base.OnPointerClick(eventData);//InterfaceManager.Instance.RequestDisplay(displayType, (displayType==BaseDisplay.DISPLAY_TYPES.TEXT)?(knotData):(data as object));
        }
    }
	public override void OnPointerEnter(PointerEventData e)
	{
		if(canDo)
		{
			placeName.DOColor(Color.grey, 0.15f);
		}
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		if(canDo)
		{
			placeName.DOColor(Color.white, 0.15f);
		}
	}
}
