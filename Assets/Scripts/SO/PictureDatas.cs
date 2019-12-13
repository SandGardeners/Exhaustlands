using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(fileName = "PictureData", menuName = "BCE/PictureData", order = 1)]
public class PictureDatas : ScriptableObject
{
    public Sprite picture;
    public string caption;

    
#if UNITY_EDITOR
    public static void CreatePictureDatas(Sprite picture)
    {
        
        PictureDatas asset = ScriptableObject.CreateInstance<PictureDatas>();
        asset.picture = picture;

        
        string uniqueAssetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/PictureDatas/"+picture.name+".asset");
        AssetDatabase.CreateAsset(asset, uniqueAssetPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif
}