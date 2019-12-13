using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : BaseDisplay
{
    public Image image;

	public void ShowImage(Sprite imageToDisplay, string caption = "")
	{
		if(image == null)
		{
        	image = gameObject.GetComponentInChildren<Image>();
        }
        image.sprite = imageToDisplay;
        image.SetNativeSize();
        
        gameObject.GetComponentInChildren<Text>().text = caption;
    }

    protected override void FeedData(object data)
    {
        PictureDatas picture = data as PictureDatas;
        ShowImage(picture.picture, picture.caption);
    }

    protected override bool IsValidData(object data)
    {
        return data is PictureDatas;
    }

    void Start()
	{
        if(image == null)
		{
        	image = GetComponentInChildren<Image>();
        }
    }
}
