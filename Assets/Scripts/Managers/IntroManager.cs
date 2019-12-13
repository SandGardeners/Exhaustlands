using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class IntroManager : MonoBehaviour, ITagsParser 
{
    [SerializeField] BaseDisplay readerPrefab;
    PlayableDirector director;
    InterfaceManager UIManager;
    TextDisplay reader;

    public void ParseTag(string tagHeader, string content)
    {
        switch(tagHeader)
		{
			case "name":
                reader.SetName(content, true);
                break;
            case "event":
                switch(content)
                {
                    case "start_snow":
                        break;
                }
                break;
			default:
                Debug.LogError("Not implemented tag");
                break;
        }
    }

    string levelName;
    AsyncOperation async;
 
    public void StartLoading() {
        StartCoroutine("load");
        async.completed += CanLoad;
    }


    void CanLoad(AsyncOperation op)
    {
        Debug.Log("canLoad");
    }
     
     IEnumerator load() {
         Debug.LogWarning("ASYNC LOAD STARTED - " +
            "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
         async = SceneManager.LoadSceneAsync("Carriage");
         async.allowSceneActivation = false; 
         yield return async;
     }
 
     public void ActivateScene() {
         async.allowSceneActivation = true;
     }

    void OnDestroy()
    {
        InkOverlord.tagsParser -= ParseTag;
    }


	// Use this for initialization
	void Start () 
    {
        InkOverlord.tagsParser += ParseTag;
        director = GetComponent<PlayableDirector>();
		UIManager = FindObjectOfType<InterfaceManager>();
        // reader = UIManager.InstantiateModule(readerPrefab).GetComponent<TextDisplay>();
        //UIManager.RequestProcessable(reader);
        // reader.ReadKnot("INTRO");
        reader.finished += StartDirector;
        StartLoading();
	}

    void StartDirector()
    {
        Invoke("PlayTimeline", 2.0f);
    }
	
    void PlayTimeline()
    {
        director.Play();
    }

    public void LoadGame()
    {
            ActivateScene();
        
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Carriage");
    }
}
