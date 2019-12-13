using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public class ChoiceManager : MonoBehaviour 
{
    public enum CHOICES_DISPLAY_MODE
	{
		IMMEDIATE,
		DELAYED
	}
	
    public CHOICES_DISPLAY_MODE _choicesDisplayMode;
	
	[SerializeField]
    public GameObject _textBoxModel;
    // Use this for initialization

    public delegate void InputDelegate(int input);
    public delegate void AddDelegate(Transform t);
    public InputDelegate Input;
    public AddDelegate AddBox;

    public bool IsBusy {
        get
        {
            return (transform.childCount != 0);
        }
    }
    public virtual void DisplayChoices()
	{
        TextBox[] textBoxes = GetComponentsInChildren<TextBox>();
        for (int i = 0; i < textBoxes.Length; i++)
        {
            TextBox tb = textBoxes[i];
            if (_choicesDisplayMode == CHOICES_DISPLAY_MODE.DELAYED)
            {
                tb.ReadLine(i * 0.5f + 0.25f, 1f);
            }
            else
            {
                tb.ReadLine();
            }
        }
    }

    private TextBox InstantiateTextBox(string s)
    {
        TextBox box;
        if (_textBoxModel == null)
        {
            GameObject go = new GameObject();
            go.transform.SetParent(transform);
            box = go.AddComponent<TextBox>();
        }
        else
        {
            GameObject go = Instantiate(_textBoxModel, transform.position+(Vector3.down*0.35f*(transform.childCount+1)), transform.rotation, transform);
            box = go.transform.GetComponent<TextBox>();
        }
        box.FeedLine(s);
        if(box is ClickableTextBox)
        {
            (box as ClickableTextBox).Input += Input;
        }

        return box;
    }

    public void DisplayImmediate()
    {
        TextBox[] textBoxes = GetComponentsInChildren<TextBox>();
        for (int i = 0; i < textBoxes.Length; i++)
        {
            TextBox tb = textBoxes[i];
            tb.DisplayImmediate();
        }
    }

    public virtual void FeedChoices(List<Choice> choices)
	{
        foreach (Choice c in choices)
        {
            // Debug.Log(c.pathStringOnChoice );
            InstantiateTextBox(c.text);
        }

        DisplayChoices();
    }

	public virtual void ClearChoices()
	{
		foreach(Transform t in transform)
		{
            Destroy(t.gameObject);
        }
	}
}
