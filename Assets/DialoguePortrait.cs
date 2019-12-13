using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class DialoguePortrait : MonoBehaviour {
	Image portrait;

	private void Awake()
	{
		portrait = GetComponent<Image>();
	}
	public void SetSprite(Sprite sprite)
	{
		if(portrait.sprite != sprite)
		{

			if(portrait.sprite != null)
			{
				portrait.DOColor(new Color(0f,0f,0f,0f), 0.75f).OnComplete(()=>{portrait.sprite = sprite;ShowSprite();});
			}
			else
			{
				portrait.sprite = sprite;
				ShowSprite();
			}

		}
	}

	public void RemoveSprite()
	{
		portrait.color = new Color(0f,0f,0f,0f);
		portrait.sprite = null;
	}

	public void ShowSprite()
	{
		portrait.DOColor(new Color(0.5f,0.5f,0.5f,1.0f), 0.75f);
	}

	public void SetCurrent(bool current)
	{
		if(portrait.sprite != null)
		{
			Color c = current?new Color(1f,1f,1f,1f):new Color(0.5f,0.5f,0.5f,1.0f);
			portrait.DOColor(c, 0.75f);
		}

	}
}
