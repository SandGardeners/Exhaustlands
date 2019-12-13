using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class AutoRadio : MonoBehaviour {

	[System.Serializable]
	public struct Station
	{
		public string name;
		public AudioClip track;
	}

	[SerializeField]
	Station[] stations;
	
	int currentStation = 0;

	AudioSource source;

	[SerializeField]
	TMP_Text radioDisplay;
	public void NextStation()
	{
		if(on)
		{
			currentStation++;
			if(currentStation == stations.Length)
				currentStation = 0;
			
			float t = source.time;
			source.clip = stations[currentStation].track;
			source.Play();
			source.time = t%source.clip.length;
			RollingText rt = radioDisplay.GetComponent<RollingText>(); 
			rt.enabled = false;
			radioDisplay.DOText(stations[currentStation].name, 1f, scrambleMode:ScrambleMode.All).OnComplete(()=>{rt.enabled = true;});
		}
	}

	bool on = true;
	public void ToggleRadio()
	{
		on = !on;
		if(on)
		{
			radioDisplay.enabled = on;
			radioDisplay.DOFade(1f, 0.25f);
		}
		else
		{
			radioDisplay.DOFade(0f, 0.25f).OnComplete(()=>{radioDisplay.enabled = false;});
		}
		source.DOFade(on?startVolume:0f, 0.25f);
	}

	float startVolume;
	void Start () 
	{
		source = GetComponent<AudioSource>();
		startVolume = source.volume;
		NextStation();
	}
	
}
