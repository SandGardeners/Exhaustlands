using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "BCE/CharacterData", order = 1)]
public class CharacterDatas : ScriptableObject 
{
    public string characterName;
    public AudioClip[] characterVoice;
}
