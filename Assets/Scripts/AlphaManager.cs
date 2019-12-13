using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AlphaManager : MonoBehaviour {

    public TMPro.TMP_Text text;
    string randoms;
    public void Start()
	{
        string s = PlayerPrefs.GetString("id");
        if(s != string.Empty)
		{
            randoms = s;
        } 
		else
		{
			randoms = Guid.NewGuid().ToString().Replace("-", string.Empty).Replace("+", string.Empty).Substring(0, 4);
            PlayerPrefs.SetString("id", randoms);
        }
        text.text += randoms;
    }

	public void OnButtonPressed()
	{
        PlayerToken token = new PlayerToken();
        token.playerName = randoms;
        InkOverlord.IO.token = token;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
    }

	
	
}
