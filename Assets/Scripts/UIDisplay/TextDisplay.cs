using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    public class TextDisplay : BaseDisplay {
        

        [SerializeField]
        public TextBox mainTextBox;

        [SerializeField]
        ChoiceManager choiceManager;

        [SerializeField]
        TextBox nameTextBox;

        public Image lineFinishedFeedback;

    // [SerializeField]
    // GameObject clickCatcher;

        // [SerializeField]
        // Fader fader;

        // [SerializeField]
        // bool introDone = false;

        public bool canSkip = true;
        public bool mute = false;
        AudioSource audioSource;
        void Start () 
        {
            choiceManager.Input += MadeChoice;
            audioSource = GetComponent<AudioSource>();
            if(!mute && audioSource != null)
                mainTextBox.characterCallback += TryPlaySound;
            if(lineFinishedFeedback != null)
            {
                mainTextBox.finishedCallback += ShowLineFinished;
                mainTextBox.newLineCallback += HideLineFinished;
            }
        }
        void ShowLineFinished()
        {
            // Debug.Log(InkOverlord.IO.hasChoices);
            lineFinishedFeedback.gameObject.SetActive(true);
            StartCoroutine("FadeLineFinished");
        }
        Color transparent = new Color(0,0,0,0);
        IEnumerator FadeLineFinished()
        {
            float timer = 0;
            int sens = 1;
            while(true)
            {
                timer += Time.deltaTime*sens;
                float ratio = timer/0.5f;
                if(ratio > 1 || ratio < 0)
                {
                    sens *= -1;
                }

                lineFinishedFeedback.color = Color.Lerp(transparent, Color.white, Utilities.Ease.sineInOut(ratio));
                yield return new WaitForEndOfFrame();
            }
        }


        void HideLineFinished()
        {
            lineFinishedFeedback.color = transparent;
            lineFinishedFeedback.gameObject.SetActive(false);
            StopCoroutine("FadeLineFinished");
        }

        public void Mute()
        {
            mute = true;
        }
        public void PlayVoiceSound()
        {
            if(previousName != string.Empty)
            {
                //TODO:audioSource.PlayOneShot(interfaceManager.GetVoiceClip(previousName),0.15f);
            }
        }

        string knot;

        protected override void FeedData(object data)
        {
            knot = data as string;
            InkOverlord.IO.RequestKnot(knot);
            InkOverlord.tagsParser += ParseTag;
        //    clickCatcher.SetActive(true);
            mainTextBox.transform.parent.gameObject.SetActive(true);
            Process();
        }

        bool ReadNextLine()
        {
            SetName("", true);
            string line = InkOverlord.IO.NextLine();
            if(line != string.Empty)
            {
                mainTextBox.ReadLine(tempDelay,tempRatio, line);
            }
            tempDelay = 0f;
            tempRatio = 1f;
            if(line != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool madeChoice = false;
        public void MadeChoice(int i)
        {
            choiceManager.ClearChoices();
            InkOverlord.IO.MakeChoice(i);
            madeChoice = true;
            Process();   
        }
        public DialoguePortrait leftPortrait;
        public DialoguePortrait rightPortrait;
        void ParseTag(string tagHeader, string content)
        {
            switch(tagHeader)
            {
                case "leftPortrait":
                    if(content == "Passenger" && TeamManager.HasPassenger())
                        content = TeamManager.GetPassenger();
                    else if(content == "Driver" && TeamManager.HasMain())
                        content = TeamManager.GetMain();
                    leftPortrait.SetSprite(TeamManager.peopleDict[content].portrait);
                    break;
                case "rightPortrait":
                                        if(content == "Passenger" && TeamManager.HasPassenger())
                        content = TeamManager.GetPassenger();
                    else if(content == "Driver" && TeamManager.HasMain())
                        content = TeamManager.GetMain();
                    rightPortrait.SetSprite(TeamManager.peopleDict[content].portrait);
                    break;   
                case "currentPortrait":
                    leftPortrait.SetCurrent(content=="left");
                    rightPortrait.SetCurrent(content=="right");
                    break;
            case "name":
                    if(content == "Passenger" && TeamManager.HasPassenger())
                        content = TeamManager.GetPassenger();
                    else if(content == "Driver" && TeamManager.HasMain())
                        content = TeamManager.GetMain();
                    SetName(content, true);
                    break;
                case "speed":
                    float textSpeed;
                    if(float.TryParse(content, out textSpeed))
                    {
                        // Debug.Log(textSpeed);
                        SetSpeed(textSpeed);
                    }
                    else
                    {
                        Debug.Log("Can't parse textSpeed");
                    }
                    break;
                case "delay":
                    float textDelay;
                    if(float.TryParse(content, out textDelay))
                    {
                        // Debug.Log(textDelay);
                        SetDelay(textDelay);
                    }
                    else
                    {
                        Debug.Log("Can't parse textDelay");
                    }
                    break;
                case "position":
                    Vector2 position;
                    float posX, posY;
                    string[] posString = content.Split('|');
                    if(float.TryParse(posString[0], out posX) && float.TryParse(posString[1], out posY))
                    {
                        RectTransform rt = transform.GetChild(0).GetComponent<RectTransform>();
                        rt.anchoredPosition = new Vector2(posX,posY);
                        rt.ForceUpdateRectTransforms();
                    }
                    else
                    {
                        Debug.Log("Can't parse position");
                    }
                    break;
                case "canSkip":
                    if(content == "false")
                        canSkip = false;
                    else
                        canSkip = true;
                        break;
                case "faith":
                    int c;
                    if(int.TryParse(content, out c))
                    {
                        InkOverlord.IO.ShowFaith(c);
                    }
                    else
                    {
                        Debug.LogError("Can't parse faith");
                    }
                    break;

                case "hack":
                    if(content == "fuel")
                        TimeManager.Refill();
                    else if(content == "motel")
                        shouldMotel = true;
                break;
            }
        }

        bool shouldMotel = false;
        
        public override void Process()
        {
            if (!choiceManager.IsBusy || madeChoice)
            {
                madeChoice = false;
                if(mainTextBox._isReading)
                {
                    if(canSkip)
                    {
                        InkOverlord.IO.Skipped();
                        mainTextBox.DisplayImmediate();
                    }

                }
                else if (InkOverlord.IO.canContinue)
                {
                    if(!ReadNextLine())
                    {
                        Terminate();
                    }
                }
                else if (InkOverlord.IO.hasChoices)
                {
                    HideLineFinished();
                    choiceManager.FeedChoices(InkOverlord.IO.GetChoices());
                    choiceManager.DisplayChoices();
                }
                else
                {
                    Terminate();
                }
            }
            else if(canSkip)
            {
                choiceManager.DisplayImmediate();
            }  
        }
        float tempDelay = 0f;
        float tempRatio = 1f;
        public void SetDelay(float textDelay)
        {
            tempDelay = textDelay;
        }

        public void SetSpeed(float textSpeed)
        {
            tempRatio = textSpeed;
        }

        public override void Terminate()
        {
            CloseTextBox();
            base.Terminate();
        }

        string previousName = "";
        public void SetName(string name, bool immediate)
        {
            nameTextBox.transform.gameObject.SetActive(name != string.Empty);
            if(name != string.Empty && name != previousName)
            {
                nameTextBox.ReadLine(0f, 0f, name);
            // nameTextBox.DisplayImmediate();
            }
            previousName = name;
        }

        void CloseTextBox()
        {
            InkOverlord.tagsParser -= ParseTag;
            nameTextBox.ReadLine("");
            mainTextBox.ReadLine("");
            mainTextBox._isReading = false;
            choiceManager.ClearChoices();
            nameTextBox.transform.parent.gameObject.SetActive(false);
            mainTextBox.transform.parent.gameObject.SetActive(false);
            RectTransform rt = transform.GetChild(0).GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f,0f);
            leftPortrait.RemoveSprite();
            rightPortrait.RemoveSprite();
            if(shouldMotel)
            {
                TeamManager.ShowRoaster();
                shouldMotel = false;
            }
        }

        float timer = 0f;
        public float sfxDelay = 0.01f;

        void TryPlaySound()
        {
            if (audioSource != null)
            {
                if (timer > sfxDelay)
                {
                        timer = 0f;
                        PlayVoiceSound();
                }
            }
        }

        void Update()
        {
            timer += Time.deltaTime;
        }

        void RemoveTrigger()
        {
        //   clickCatcher.SetActive(false);
        }

        void RemoveBackground()
        {
            // introDone = true;
            // fader.allBlackDelegate -= RemoveBackground;
    //     clickCatcher.SetActive(false);
        }

        protected override bool IsValidData(object data)
        {
            return data is string;
        }

        /*
    public InputField inputField;

    public void OpenInputField()
    {
    inputField.gameObject.SetActive(true);
    inputField.Select();
    inputField.ActivateInputField();
    }
    */
    }

