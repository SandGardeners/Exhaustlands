using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(fileName = "AudioData", menuName = "BCE/AudioData", order = 1)]
public class AudioDatas : ScriptableObject
{
    public AudioClip audioClip;

#if UNITY_EDITOR
    public static void CreateAudioDatas(AudioClip clip)
    {
        
        AudioDatas asset = ScriptableObject.CreateInstance<AudioDatas>();
        asset.audioClip = clip;

        
        string uniqueAssetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/AudioDatas/"+clip.name+".asset");
        AssetDatabase.CreateAsset(asset, uniqueAssetPath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
#endif
}