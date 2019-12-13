using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoDisplay : BaseDisplay 
{
    VideoPlayer video;
	protected override void FeedData(object data)
	{
        VideoDatas _data = data as VideoDatas;
        gameObject.SetActive(true);
		
        if(video == null)
		{
        	video = GetComponent<VideoPlayer>();
        }
        if(video != null)
        {
            video.clip = _data.clip;
        }
        if(_data.caption != string.Empty)
        {
            gameObject.GetComponentInChildren<Text>().text = _data.caption;
        }
    }

    protected override bool IsValidData(object data)
    {
        return data is VideoDatas;
    }
}
