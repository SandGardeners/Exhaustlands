using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using DG.Tweening;

public class RoadDisplay : MonoBehaviour 
{
    public string content;
    public string header;
	public void StartThing(System.Action _d, string _h, string _c)
	{
        done = _d;
        content = _c;
        header = _h;
        GetComponentInChildren<TMPro.TMP_Text>().text = header;
        GetComponentInChildren<DG.Tweening.DOTweenAnimation>().DOPlayById("firstTween");
    }

    public void FeedText()
    {
        GetComponentInChildren<TMPro.TMP_Text>().fontSize = 42;
        GetComponentInChildren<TMPro.TMP_Text>().text = content;
    }
System.Action done;
    public void Terminate()
    {
        done();
        Destroy(gameObject);
    }
}
