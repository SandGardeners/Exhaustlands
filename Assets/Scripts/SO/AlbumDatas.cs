using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "AlbumData", menuName = "BCE/AlbumData", order = 1)]
public class AlbumDatas : ScriptableObject
{
    public List<Sprite> pictures;
    public List<string> captions;

#if UNITY_EDITOR
    public static void CreateAlbumDatas(List<Sprite> pictures)
    {
        AlbumDatas asset = ScriptableObject.CreateInstance<AlbumDatas>();
        asset.pictures = pictures;
        
        string uniqueAssetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/AlbumDatas/album.asset");
        AssetDatabase.CreateAsset(asset, uniqueAssetPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif

}