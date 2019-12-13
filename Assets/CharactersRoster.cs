using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class CharactersRoster : MonoBehaviour {

	public TMPro.TMP_Text text;
	public TMPro.TMP_Text description;

	public Button driverButton;
	public Button passengerButton;
	public Button startButton;

	bool canRotate = true;

	private void Start()
	{
		startButton.interactable = TeamManager.HasMain();		
	}
	public void ApplyChar()
	{
		TeamManager.ApplyRoaster();
	}
	public void Rotate(bool right)
	{
		string newName;
		if(canRotate)
		{
			canRotate = false;
			startButton.interactable = driverButton.interactable = passengerButton.interactable = false;		

			RectTransform rt = transform.GetChild(0).GetComponent<RectTransform>();
			if(right)
			{
				newName = rt.GetChild(2).name;//, 2f, scrambleMode:ScrambleMode.All);		
				if(!TeamManager.IsUnlocked(newName))
					newName = "???";
				rt.DOAnchorPos(new Vector2(-704f, 0f), 2f).OnComplete(
				()=>{
						rt.transform.GetChild(0).SetSiblingIndex(rt.transform.childCount-1);
						rt.anchoredPosition = new Vector2(-352f, 0f);
				});
		}
		else
		{
			newName = rt.GetChild(0).name;
			if(!TeamManager.IsUnlocked(newName))
					newName = "???";
			rt.DOAnchorPos(new Vector2(0f, 0f), 2f).OnComplete(()=>{
			rt.transform.GetChild(rt.transform.childCount-1).SetSiblingIndex(0);
			rt.anchoredPosition = new Vector2(-352f, 0f);}
			);
		}
		text.DOText(newName, 2f, scrambleMode:ScrambleMode.All);
		text.DOColor((newName=="???"?Color.white:TeamManager.peopleDict[newName].color), 2f);
		description.DOFade(0f, 1f).OnComplete(()=>{description.text = (newName=="???"?"???":TeamManager.peopleDict[newName].description.Replace("_", "\n"));
			description.DOFade(1f, 1f).OnComplete(()=>{canRotate = true; driverButton.interactable = passengerButton.interactable = newName!="???"; startButton.interactable = TeamManager.HasMain();});});
		}
	}

	public void Select(bool main)
	{
		RectTransform rt = transform.GetChild(0).GetComponent<RectTransform>();
		string chara = rt.GetChild(1).name;
		if(!TeamManager.IsInTeam(chara))
			Rotate(true);
		
		if(main)
			TeamManager.SetMain(chara);
		else
			TeamManager.SetPassenger(chara);

		
		startButton.interactable = TeamManager.HasMain();
	}
	
}
