using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class TextBoxScalerSimple : MonoBehaviour {

    private TMP_Text textReader;
    private RectTransform tr;
    float curHeight;
    float startHeight;
    
    public bool scaleX;
    public bool scaleY;
	void Start () 
    {
        textReader = GetComponent<TMP_Text>();
        tr = GetComponent<RectTransform>();
        
        
        //GetComponent<TextMeshBox>().newLineCallback += ResetHeight;
    }

    void ResetHeight()
    {
        curHeight = startHeight;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(textReader.text.Length > 3)
        {
            if((scaleY && tr.sizeDelta.y != textReader.bounds.size.y) || (scaleX && tr.sizeDelta.x != textReader.bounds.size.x))
            {
                Vector2 v = tr.sizeDelta;
                if(scaleY)
                    v.y = textReader.bounds.size.y;
                if(scaleX)
                    v.x = textReader.bounds.size.x;
                    
                tr.DOSizeDelta(v, 1f).SetEase(Ease.OutSine);
            }
        }
        // tr.sizeDelta = new Vector2(textReader.bounds.size.x, );
    }
}
