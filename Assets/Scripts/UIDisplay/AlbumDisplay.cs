using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlbumDisplay : ImageDisplay, IProcessable 
{

	int currentIndex;
	AlbumDatas album;

	public Image leftArrow;
	public Image rightArrow;

    public override void Terminate()
    {
		base.Terminate();
		currentIndex = 0;
    }

	public void LeftImage()
	{
		if(currentIndex > 0)
			currentIndex--;
		else
			leftArrow.enabled = false;

		rightArrow.enabled = true;
        ShowCurrentImage();
    }
	public void RightImage()
	{
		if(currentIndex < album.pictures.Count-1)
			currentIndex++;
		if(currentIndex >= album.pictures.Count-1)
			rightArrow.enabled = (false);
		leftArrow.enabled = (true);
        ShowCurrentImage();
    }

	public void ShowCurrentImage()
	{
        string caption = string.Empty;
        Debug.Assert(currentIndex < album.pictures.Count);
        
		if(currentIndex < album.captions.Count)
			caption = album.captions[currentIndex];

        ShowImage(album.pictures[currentIndex], caption);
	}

    protected override bool IsValidData(object data)
    {
        return data is AlbumDatas;
    }
    protected override void FeedData(object _album)
    {
		album = _album as AlbumDatas;
        transform.parent.gameObject.SetActive(true);
		currentIndex = 0;
		
		rightArrow.enabled = (true);
		leftArrow.enabled = false;
        ShowCurrentImage();
    }

}
