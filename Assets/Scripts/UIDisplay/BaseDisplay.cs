using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseDisplay : MonoBehaviour, IProcessable {

    public static Type GetDataTypeFromDisplayType(DISPLAY_TYPES displayType)
    {
        switch(displayType)
        {
            case DISPLAY_TYPES.TEXT:
                return typeof(String);
            case DISPLAY_TYPES.NEWROAD:
                return typeof(String);
            case DISPLAY_TYPES.PICTURE:
                return typeof(PictureDatas);
            case DISPLAY_TYPES.ALBUM:
                return typeof(AlbumDatas);
            case DISPLAY_TYPES.AUDIO:
                return typeof(AudioDatas);
            case DISPLAY_TYPES.VIDEO:
                return typeof(VideoDatas);
            default:
                return null;
        }
    }

    [System.Serializable]
    public enum DISPLAY_TYPES
    {
        TEXT,
        NEWROAD,
        PICTURE,
        ALBUM,
        VIDEO,
        AUDIO
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Process();
    }

    public virtual void Process()
    {
        Terminate(); 
    }

	public Action finished;
    public virtual void Terminate()
    {
		gameObject.SetActive(false);
        if(finished != null)
			finished();
    }

    protected abstract bool IsValidData(object data);
    public void Activate(Action stopInteraction, object data=null)
    {
		gameObject.SetActive(true);
		finished = stopInteraction;
        if(data != null)
        {
            Debug.Assert(IsValidData(data));
            FeedData(data);
        }
    }

    protected virtual void FeedData(object data)
    {

    }
}
