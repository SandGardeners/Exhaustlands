using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FindObjectOfType<Carriage>().changedCarriage += SwitchCarriage;
		currentCarriage = transform.GetChild(0).gameObject;
	}
	GameObject currentCarriage;
	public void SwitchCarriage(int carriageNumber)
	{
		currentCarriage.SetActive(false);
		currentCarriage = transform.GetChild(carriageNumber-1).gameObject;
		currentCarriage.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
