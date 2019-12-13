using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatasManager : MonoBehaviour 
{
	public static DatasLibrary datasLibrary;
	public static Dictionary<BaseDisplay.DISPLAY_TYPES, BaseDisplay> prefabsLibrary;
	public DatasLibrary datas;
	
	[SerializeField]
	private DisplayDatas displayPrefabs;

	void Awake()
	{
		datasLibrary = datas;
		prefabsLibrary = new Dictionary<BaseDisplay.DISPLAY_TYPES, BaseDisplay>();
		for(int i = 0; i < displayPrefabs.types.Count; i++)
		{
			prefabsLibrary[displayPrefabs.types[i]] = displayPrefabs.prefabs[i];
		}
		
	}
}