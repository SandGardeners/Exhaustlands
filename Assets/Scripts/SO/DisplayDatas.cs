using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(fileName = "DisplayDatas", menuName = "BCE/DisplayDatas", order = 1)]
public  class DisplayDatas : ScriptableObject
{
    public List<BaseDisplay.DISPLAY_TYPES> types;
    public List<BaseDisplay> prefabs;

}