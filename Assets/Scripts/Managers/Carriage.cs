using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Carriage : MonoBehaviour 
{	
    public Action<int> changedCarriage;
    [SerializeField] Transform player;
    [SerializeField] Transform startingPoint;

    public void ChangeCarriage(int i)
	{
		if(changedCarriage != null)
		{
			changedCarriage(i);
		}
        player.transform.position = startingPoint.position;
    }

    private void Start()
    {
    }
}
