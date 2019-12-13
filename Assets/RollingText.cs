using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RollingText : MonoBehaviour 
{
	TMP_Text text;
	private void Start()
	{
		text = GetComponent<TMP_Text>();
		InvokeRepeating("Roll", 0.5f, 0.5f);
	}

	void Roll()
	{
		string t = text.text;
		text.text = string.Format("{0}{1}", t.Substring(1, t.Length - 1), t[0]);
	}
}
