using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(fileName = "DatasLibrary", menuName = "BCE/DatasLibrary", order = 2)]
public class DatasLibrary : ScriptableObject 
{
    public CharacterDatas[] charactersDatas;
    public AlbumDatas[] albumDatas;
    public PictureDatas[] pictureDatas;
    public AudioDatas[] audioDatas;
    public VideoDatas[] videoDatas;
    Dictionary<string, ScriptableObject> dictionary;

    void InitializeDictionary()
    {
        dictionary = new Dictionary<string, ScriptableObject>();
        ScriptableObject[][] allDatas = {charactersDatas, albumDatas, pictureDatas, audioDatas, videoDatas};
        foreach(ScriptableObject[] soArray in allDatas)
        {
            foreach(ScriptableObject so in soArray)
            {
                Debug.Assert(!dictionary.ContainsKey(so.name));
                dictionary[so.name] = so;
            }
        }
    }
    public T Request<T>(string soID) where T: ScriptableObject
    {
        if(dictionary == null)
            InitializeDictionary();
        Debug.Assert(dictionary.ContainsKey(soID) && dictionary[soID] is T);
        return dictionary[soID] as T;
    }



#if UNITY_EDITOR
    [ContextMenu("Auto-Feed")]
    public void AutoFeed()
    {
        charactersDatas = GetDatasArray<CharacterDatas>();
        albumDatas = GetDatasArray<AlbumDatas>();
        pictureDatas = GetDatasArray<PictureDatas>();
        audioDatas = GetDatasArray<AudioDatas>();
        videoDatas = GetDatasArray<VideoDatas>();

        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(this);
    }

    public T[] GetDatasArray<T>() where T : ScriptableObject
    {
        string[] folders = { "Assets/ScriptableObjects"};
        string[] guids = AssetDatabase.FindAssets("t:"+typeof(T).ToString(), folders);
        T[] array = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            array[i] = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[i]));
        }
        return array;
    }

#endif
}
