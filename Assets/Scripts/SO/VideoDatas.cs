using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(fileName = "VideoData", menuName = "BCE/VideoData", order = 1)]
public  class VideoDatas : ScriptableObject
{
    public UnityEngine.Video.VideoClip clip;
    public string caption;

    
#if UNITY_EDITOR
    public static void CreateVideoDatas(VideoClip clip)
    {
        VideoDatas asset = ScriptableObject.CreateInstance<VideoDatas>();
        asset.clip = clip;
        
        string uniqueAssetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/VideoDatas/"+clip.name+".asset");
        AssetDatabase.CreateAsset(asset, uniqueAssetPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif

}