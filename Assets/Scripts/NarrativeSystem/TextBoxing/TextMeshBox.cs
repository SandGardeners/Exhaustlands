﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMeshBox : MonoBehaviour 
{
    protected TMP_Text _textComponent = null;
    [SerializeField]
    protected string _currentString = string.Empty;

    public string CurrentString
    {
        get
        {
            return _currentString;
        }
        set
        {
            FeedLine(value);
        }
    }
    public float _readingSpeedReference = 0f;
    public Color _colorDefault;
    float _currentReadingSpeed = 0f;
    float _characterTimer = 0f;
    protected int _currentIndex = 0;

    public bool _isReading = false;
    public delegate void SimpleCallback();
    public SimpleCallback finishedCallback;
    public SimpleCallback newLineCallback;

    public SimpleCallback characterCallback;

    [HideInInspector]
    public float temporaryRatio = -1f;
    float commaWaitingTime = 0.1f;
    float fullpointWaitingTime = 0.25f;

    void Awake()
    {
        RealStart();
    }
    public void RealStart() 
	{
        _textComponent = GetComponent<TMP_Text>();
        _textComponent.text = "";
        _textComponent.color = _colorDefault;
        _currentReadingSpeed = _readingSpeedReference;
        // finishedCallback += ReadLine;
    }

	public virtual void FeedLine(string s)
	{
        _currentString = s;
    }

    public void ReadLine(float delayTimer = 0f, float readingSpeedRatio = 1f, string newString = "")
    {
        if(newLineCallback != null)
            newLineCallback.Invoke();
        if(newString != string.Empty)
        {
            FeedLine(newString);
        }

        if (_currentString == string.Empty)
        {
            DisplayImmediate();
            return;
        }
        else
        {
           
            _currentReadingSpeed = _readingSpeedReference * readingSpeedRatio;
            // Debug.Log("readingSpeedRatio: " + readingSpeedRatio);
            _characterTimer = _currentReadingSpeed + delayTimer;
         
            _isReading = true;
            _currentIndex = 0;
            if(_textComponent == null)
            {
                _textComponent = GetComponent<TMP_Text>();
            }
            _textComponent.text = string.Empty;
            if(readingSpeedRatio == 0f)
            {
                _currentIndex = newString.Length;   
                if(characterCallback != null)
                {
                    characterCallback();
                }
            }
        }
    }

    internal void DisplayImmediate()
    {
        if (_textComponent != null)
        {
            _textComponent.text = _currentString;
            _textComponent.ForceMeshUpdate();
            _isReading = false;
            if (finishedCallback != null)
                finishedCallback.Invoke();
        }
    }

    public void ReadLine(string newString)
    {
        ReadLine(0f, 1f, newString);
    }

    public void ReadLine()
    {
        ReadLine(0f, 1f, "");
    }

    protected virtual void Progress(int index)
    {
        _textComponent.text += _currentString[index];
    }

	// Update is called once per frame
	protected virtual void Update () 
	{
        if (_isReading)
        {
            _characterTimer -= Time.deltaTime;
            if (_characterTimer <= 0f)
            {
                if (_currentIndex >= _currentString.Length)
                {
                    DisplayImmediate();
                }
                else
                {
                    Progress(_currentIndex++);
                    _characterTimer = _currentReadingSpeed;
                    if(_textComponent.text.EndsWith(","))
                        _characterTimer += commaWaitingTime * (_currentReadingSpeed/_readingSpeedReference);
                    else if(_textComponent.text.EndsWith("."))
                        _characterTimer += fullpointWaitingTime * (_currentReadingSpeed/_readingSpeedReference);
                }
            }
        }
    }
}