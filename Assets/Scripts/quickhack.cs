using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class quickhack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += Input.GetAxis("Vertical") * Time.deltaTime * 0.2f;
	}
}
