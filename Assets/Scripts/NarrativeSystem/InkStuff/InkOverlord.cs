using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public class InkOverlord : MonoBehaviour
{
    static InkOverlord _instance;

    public static InkOverlord IO
    {   
        get
        {
            return _instance;
        }
    }

    public TextAsset storyScript;
    Story inkStory;
    public string GetCurrentPath()
    {
        return lastPath;
    }

    public static Action<string, string> tagsParser;
    public void ChangeVariable(string _key, object _v)
    {
        inkStory.variablesState[_key] = _v;
    }

    public bool canContinue
    {
        get
        {
            return inkStory.canContinue;
        }
    }

    public bool hasChoices
    {
        get
        {
            return inkStory.currentChoices.Count > 0;
        }
    }

    public void Skipped()
    {

    }

    string lastPath;

    public string NextLine()
    {
        lastPath = inkStory.state.currentPathString;
        // Debug.Log(lastPath);
  
        string line = inkStory.Continue();
        if(inkStory.hasError)
        {
            foreach(string s in inkStory.currentErrors)
            {
                Debug.Log(s);
            }
        }
        Debug.Assert(!inkStory.hasError);
        
        if (tagsParser != null)
            GetTags();



       
        return line;
    }

    public List<Choice> GetChoices()
    {
   

        return inkStory.currentChoices;
    }

    public bool MakeChoice(int index)
    {
        if (index < inkStory.currentChoices.Count)
        {
            inkStory.ChooseChoiceIndex(index);
            return true;
        }
        Debug.LogError("INVALID CHOICE INDEX");
        return false;
    }


    public Action<String> eventDelegate;
    Action<string> inkEventDelegate;

    string startTime;
    // Use this for initialization
    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        startTime = System.DateTime.Now.ToString("dd-MM-yy_HH:mm:ss");
        inkEventDelegate += CatchEvent;
        inkStory = new Story(storyScript.text);
        inkStory.BindExternalFunction<String>("CustomEvent", inkEventDelegate);

    }

    void CatchEvent(string eventTag)
    {
        // Debug.Log("CATCHED EVENT " + eventTag);
        if (eventDelegate != null)
        {
            eventDelegate(eventTag);
        }
    }

    void Update()
    {

    }

    public PlaythroughHistory history;
    public Action<PlaythroughHistory> requestLogging;
    public PlayerToken token;

    void OnApplicationQuit()
    {
      
    }

    public void RequestKnot(string knotPath)
    {
        inkStory.ChoosePathString(knotPath);
        // Debug.Log(inkStory.globalTags);
        // Debug.Log(inkStory.currentTags);
    }

    public void SetCharactersInTeam(string main, string second)
    {
        var list = new InkList("CharactersInTeam", inkStory);
        list.AddItem(main);

        if(!String.IsNullOrEmpty(second))
            list.AddItem(second);

        inkStory.variablesState["CharactersInTeam"] = list;
    }

    bool CheckRequestTag(string tag)
    {
        if(!tag.Contains("<"))            
            return false;

        string[] s = tag.Split(' ');
        Debug.Assert(s.Length == 2); 
        FindObjectOfType<InterfaceManager>().RequestDisplayFromInk(s[0], s[1].Remove(s[1].Length-1,1).Remove(0,1));
        return true;
    }

    public void ShowFaith(int nb)
    {
        int faith = (int)inkStory.variablesState["faith"];
        Notifications.RequestNotification("Gained " + nb.ToString() + " faith", faith.ToString() + " total");
    }

    public void GetTags()
    {
        List<string> tags = inkStory.currentTags;
        if (tags.Count > 0)
        {
            foreach (string s in tags)
            {
                if(!CheckRequestTag(s))
                {
                    string[] tag = s.Split(':');
                    if (tag.Length != 2)
                    {
                        Debug.LogError("Invalid tag syntax");
                    }
                    tagsParser(tag[0], tag[1]);
                }
            }
        }
    }
}
