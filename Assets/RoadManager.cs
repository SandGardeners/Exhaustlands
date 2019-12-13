using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class RoadManager : MonoBehaviour 
{
	static RoadManager _instance;
	public static RoadManager Instance
	{
		get
		{
			return _instance;
		}
	}
	[SerializeField] 
	TMP_Text roadText;

	// Use this for initialization
	void Start () 
	{
		if(_instance != null)
		{
			Destroy(gameObject);
			return;
		}	
		_instance = this;
		knownRoads = new List<string>();
	}
	string curRoad;
	List<string> knownRoads;
	public void NewRoad(string roadName)
	{
		if(roadName != curRoad)
		{
			curRoad = roadName;
			if(roadName != "???")
			{
				if(!knownRoads.Contains(roadName))
				{
					knownRoads.Add(roadName);
					Notifications.RequestNotification("New road discovered", roadName);
				}
			}
			roadText.DOText(roadName, 0.25f, scrambleMode:ScrambleMode.All);
		}
	}
}
