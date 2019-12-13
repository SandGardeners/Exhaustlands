using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using TMPro;
using DG.Tweening.Plugins;
using DG.Tweening.Core;
using System;
public class TriggerAnim : MonoBehaviour {

	TMP_Text anim;
	public string fullName;

	public Action<bool> activated;
	private void Start()
	{
		anim = GetComponent<TMP_Text>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			anim.DOText(fullName, 1f,scrambleMode:ScrambleMode.All);
			anim.DOFade(1f, 0.25f);
			activated(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			anim.DOFade(0f, 1f).onComplete += ()=>{anim.text = "";};
			activated(false);
		}
	}
}
