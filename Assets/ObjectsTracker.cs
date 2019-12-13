using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class ObjectsTracker : MonoBehaviour {

	List<Transform> tracked;

	[SerializeField]
	Transform main;
	[SerializeField]
	float distThreshold;
	CinemachineTargetGroup group;

	private void Start()
	{
		tracked = new List<Transform>();
		group = GetComponent<CinemachineTargetGroup>();
		Debug.Assert(group != null, "Group is null");
		Debug.Assert(group.m_Targets.Length > 0 && group.m_Targets[0].target == main);
		DOTween.Init();
	}

	void SetKik(int index, float x)
	{
		// Debug.Log(index);
		group.m_Targets[index].weight = x; 
	}

	float GetKik(int index)
	{
		// Debug.Log(index);

		return group.m_Targets[index].weight;
	}

	private void Update()
	{
		for(int i = 1; i < group.m_Targets.Length; i++)
		{
			CinemachineTargetGroup.Target t = group.m_Targets[i];
			
			float dist = Mathf.Abs(Vector3.SqrMagnitude(t.target.position - main.position));
			if(!tracked.Contains(t.target) && dist <= distThreshold)
			{
				tracked.Add(t.target);
				int i2 = i;
				DOTween.To(()=>GetKik(i2), x=>SetKik(i2,x),1f, 3.5f).SetEase(Ease.InOutQuad);
			}
			else if(tracked.Contains(t.target) )
			{
				if(dist >= distThreshold)
				{
					tracked.Remove(t.target);
					int i2 = i;
					DOTween.To(()=>GetKik(i2), x=>SetKik(i2,x),0f, 3.5f).SetEase(Ease.InOutQuad);
				}
			}
		}	
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(main.position, Mathf.Sqrt(distThreshold));
	}
}
