using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterInfo : MonoBehaviour {

	public bool isUnlocked;
	// Use this for initialization
	void OnEnable()
	{
		isUnlocked = TeamManager.IsUnlocked(gameObject.name);
		GetComponent<Image>().color = isUnlocked?Color.white:Color.black;
	}
}
