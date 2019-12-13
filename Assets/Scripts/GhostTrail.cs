using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour 
{
    List<GameObject> trailParts;
    void Start()
    {
        trailParts = new List<GameObject>();
    }

    Vector3 previousPosition;
    void Update()
	{
		if(transform.position != previousPosition)
		{
            SpawnTrailPart();
        }

        previousPosition = transform.position;
    }
     
    void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;

        trailPart.transform.localScale = transform.localScale; // We forgot about this line!!!
        trailPartRenderer.sortingOrder = (trailParts.Count);
        trailParts.Add(trailPart);
        StartCoroutine(FadeTrailPart(trailPartRenderer));
        // Destroy(trailPart, 1.5f); // replace 0.5f with needed lifeTime
    }
     
    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
		Color color = trailPartRenderer.color;
        color.a = 0.75f;
        while(color.a > 0f)
		{
			color.a -= 0.020f; // replace 0.5f with needed alpha decrement
			trailPartRenderer.color = color;
		
			yield return new WaitForEndOfFrame();
		}
        Destroy(trailPartRenderer.gameObject);
		yield return new WaitForEndOfFrame();
    }

}
