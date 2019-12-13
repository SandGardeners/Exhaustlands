using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Priority_Queue;
using UnityEngine.UI;
using DG.Tweening;

public partial class InterfaceManager : MonoBehaviour 
{
    
    public GameObject goBackArrow;
    Dictionary<BaseDisplay.DISPLAY_TYPES, BaseDisplay> displayPrefabs;
	public GameObject clicker;
    public GameObject pauseMenu;
    public delegate void SimpleDelegate();
    public Texture2D normalCursor;
    public Texture2D interactiveCursor;
    public Texture2D walkableCursor;
    public DatasLibrary library;

    public Transform modulesTransform;
    Dictionary<string, CharacterDatas> characterDatas;
    public AudioClip[] defaultVoices;
    [HideInInspector]
    public bool canProcess = true;
    Vector2 cursorHotSpot = new Vector2(-0.2f, -0.2f);
    public bool inCarriage = true;
    SimplePriorityQueue<DisplayRequest> requestQueue;

    private static InterfaceManager instance;
    public static InterfaceManager Instance
    {
        get
        {
            return instance;
        }
    }

    public static bool CanCarMove()
    {
        return (instance.modulesTransform.childCount == 0 || !instance.modulesTransform.GetChild(0).gameObject.activeSelf) && TeamManager.isDisplaying() == false;
    }
    public class DisplayRequest
    {
        public BaseDisplay display;
        public object data;
        public DisplayRequest(BaseDisplay _display, object _data = null)
        {
            display = _display;
            data = _data;
        }
    }

    public Image fader;
    public void Fade(Color to, float duration, Action action)
    {
        fader.transform.SetAsLastSibling();
        fader.color = Color.clear;
        fader.gameObject.SetActive(true);
        fader.DOColor(to, duration/2.0f).OnComplete(
            ()=>{
                action();fader.DOColor(Color.clear, duration/2.0f).OnComplete(
                    ()=>{
                        fader.gameObject.SetActive(false);}
                        );
            });
    }
    
    //SHOULD MOVE
    public AudioClip GetVoiceClip(string name)
    {
        if(characterDatas != null)
        {
            if(characterDatas.ContainsKey(name))
            {
                AudioClip[] voices = characterDatas[name].characterVoice;
                return voices[UnityEngine.Random.Range(0, voices.Length)];
            }
        }
        return defaultVoices[UnityEngine.Random.Range(0, defaultVoices.Length)];
        
    }

   
    //SHOULD MOVE
    public void NormalCursor()
    {
        Cursor.SetCursor(normalCursor, cursorHotSpot, CursorMode.Auto);
    }

    //SHOULD MOVE
    public void InteractiveCursor()
    {
        Cursor.SetCursor(interactiveCursor, cursorHotSpot, CursorMode.Auto);
    }

    //SHOULD MOVE
    public void WalkableCursor()
    {
        Cursor.SetCursor(walkableCursor, cursorHotSpot, CursorMode.Auto);
    }

    //SHOULD MOVE
    public void Resume()
    {
        pauseMenu.SetActive(false);
    }
    //SHOULD MOVE
    public void Quit()
    {
        Application.Quit();
    }

    BaseDisplay currentDisplay = null;
    
    void ActivateDisplay(DisplayRequest request)
    {
        currentDisplay = request.display;
        currentDisplay.Activate(StopInteraction, request.data);
    }
    
    void StopInteraction()
    {
        if(requestQueue.Count != 0)
        {
            ActivateDisplay(requestQueue.Dequeue());
        }
        else
        {
            currentDisplay = null;
        }
    }

    public void RequestDisplay(BaseDisplay.DISPLAY_TYPES displayType, object data = null,int priority = 1, Action startInteraction = null, Action stopInteraction = null)
    {
        DisplayRequest request = new DisplayRequest(InstantiateModule(displayType), data);
        if(currentDisplay == null)
        {
            ActivateDisplay(request);
        }
        else
        {
            requestQueue.Enqueue(request, priority);
        }
    }

    public void RequestDisplayFromInk(string display, string contentID)
    {
        // Debug.Log(display);
        // Debug.Log(contentID);   

        BaseDisplay.DISPLAY_TYPES displayType = (BaseDisplay.DISPLAY_TYPES) Enum.Parse(typeof(BaseDisplay.DISPLAY_TYPES), display);
        
        switch(displayType)
        {
            case BaseDisplay.DISPLAY_TYPES.TEXT:
                RequestDisplay(displayType, contentID, 0);
                break;
            case BaseDisplay.DISPLAY_TYPES.NEWROAD:
                RequestDisplay(displayType, contentID, 0);
                break;
            case BaseDisplay.DISPLAY_TYPES.PICTURE:
                RequestDisplay(displayType, DatasManager.datasLibrary.Request<PictureDatas>(contentID), 0);
                break;
            case BaseDisplay.DISPLAY_TYPES.ALBUM:
                RequestDisplay(displayType, DatasManager.datasLibrary.Request<AlbumDatas>(contentID), 0);
                break;
            case BaseDisplay.DISPLAY_TYPES.VIDEO:
                RequestDisplay(displayType, DatasManager.datasLibrary.Request<VideoDatas>(contentID), 0);
                break;
            case BaseDisplay.DISPLAY_TYPES.AUDIO:
                RequestDisplay(displayType, DatasManager.datasLibrary.Request<AudioDatas>(contentID), 0);            
                break;
            default:
            Debug.LogError("UNKNOWN DISPLAY TYPE " + display);
                break;
        }
    }
    
    void Update()
	{
       HandleInputs();
        //ON THE FLY COMPONENT
	}
    //PART OF PROCESS() REFACTORING

    public BaseDisplay InstantiateModule(BaseDisplay.DISPLAY_TYPES displayType)
    {
		BaseDisplay module;      
		if(displayPrefabs.ContainsKey(displayType) && displayPrefabs[displayType] != null)
		{
            module = displayPrefabs[displayType];
        }
		else
		{
			module = displayPrefabs[displayType] = GameObject.Instantiate(DatasManager.prefabsLibrary[displayType], modulesTransform);
		}

        module.gameObject.SetActive(false);
        return module;
    }

	void Awake()
	{
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        displayPrefabs = new Dictionary<BaseDisplay.DISPLAY_TYPES, BaseDisplay>();
        requestQueue = new SimplePriorityQueue<DisplayRequest>();
        
        clicker.SetActive(false);
        if(goBackArrow != null)
            goBackArrow.SetActive(false);

        if(library != null)
        {
            characterDatas = new Dictionary<string, CharacterDatas>();
            foreach(CharacterDatas cd in library.charactersDatas)
            {
                characterDatas[cd.characterName] = cd;
            }
        }
    }
    
    //DEBUG COMPONENT


}
