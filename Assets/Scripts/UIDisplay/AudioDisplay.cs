using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDisplay : BaseDisplay 
{
	AudioSource source;
	public override void Terminate()
	{

	}
	protected override void FeedData(object data)
	{
		AudioDatas audio = data as AudioDatas;
		if(source == null)
			source = GetComponentInChildren<AudioSource>();
		source.clip = audio.audioClip;
		source.Play();
	}

    protected override bool IsValidData(object data)
    {
        return data is AudioDatas;
    }
}
