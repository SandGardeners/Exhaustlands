using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlphaHistory : ScriptableObject 
{
	public int nbTesters;
	public string[] storyIndexes;
	public StoryStat[] storyStats;

	public string[] interactionIndexes;
	public int[] interactionCount;
	public static AlphaHistory CreateInstance(List<string> _storyIndexes, List<StoryStat> _storyStats, List<string> _interactionIndexes, List<int> _interactionCount)
	{
		AlphaHistory so = CreateInstance<AlphaHistory>();
		so.storyIndexes = _storyIndexes.ToArray();
		so.storyStats = _storyStats.ToArray();
		so.interactionIndexes = _interactionIndexes.ToArray();
		so.interactionCount = _interactionCount.ToArray();
		return so;
	}

	Dictionary<string, StoryStat> storiesDico;
	Dictionary<string, int> interactionDico;
	public StoryStat getStatByString(string index)
	{
		if(storiesDico == null)
		{
			storiesDico = new Dictionary<string, StoryStat>();
			for(int i = 0; i < storyIndexes.Length; i++)
			{
				storiesDico[storyIndexes[i]] = storyStats[i];
			}
		}

		if(storiesDico.ContainsKey(index))
		{
			return storiesDico[index];
		}
		else
		{
			return null;
		}
	}

	public int getInteractiveCountByName(string name)
	{
		if(interactionDico == null)
		{
			interactionDico = new Dictionary<string, int>();
			for(int i = 0; i < interactionIndexes.Length; i++)
			{
				interactionDico[interactionIndexes[i]] = interactionCount[i];
			}
		}

		if(interactionDico.ContainsKey(name))
		{
			return interactionDico[name];
		}
		else
		{
			return 0;
		}
	}
}


[System.Serializable]
public class StoryStat
{
	public int count;
	public float meanTime;
	public float medianeTime;
	public float meanFastForward;

    internal string DebugString()
    {
        return "meanTime: " + meanTime + " / medianeTime: " + medianeTime + " / meanFastForward: " + meanFastForward;
    }
}
