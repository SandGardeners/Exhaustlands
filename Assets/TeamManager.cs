using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ReflexCLI.Attributes;
public class TeamManager : MonoBehaviour 
{
	[System.Serializable]
	public struct PeopleDatas
	{

		public string name;
		public string description;
		public Color color;
		public Sprite portrait;

	}

	public static bool isDisplaying()
	{
		return instance.characterSelectionScreen != null;
	}
	public List<string> unlockedCharacters;
	public static Dictionary<string, PeopleDatas> peopleDict;
	[SerializeField]
	GameObject characterSelectionPrefab;
	[SerializeField]
	List<PeopleDatas> peoples;

	[SerializeField]
	Image[] peopleLines;
	
	static TeamManager instance;
	public static void SetMain(string character)
	{
		if(instance.team[1] == character)
			instance.SelectCharacter(false,"");
		instance.SelectCharacter(true, character);
	}

	[ConsoleCommand]
	public static void ShowRoaster()
	{
		InterfaceManager.Instance.Fade(Color.black, 2f,()=>{
		instance.characterSelectionScreen = Instantiate(instance.characterSelectionPrefab,instance.transform);});
		
		// instance.characterSelectionScreen.SetActive(true);
	}

	public static bool IsInTeam(string character)
	{
		return (IsMain(character) || IsPassenger(character));
	}

	public static bool HasPassenger()
	{
		return !string.IsNullOrEmpty(instance.team[1]);
	}

	public static bool IsUnlocked(string character)
	{
		return instance.unlockedCharacters.Contains(character);
	}

	public static bool IsMain(string character)
	{
		return instance.team[0] == character;
	}

	public static string GetMain()
	{
		return instance.team[0];
	}

	public static string GetPassenger()
	{
		return instance.team[1];
	}

	public static bool IsPassenger(string character)
	{
		return instance.team[1] == character;
	}

	public static void SetPassenger(string character)
	{
		if(instance.team[0] == character)
			instance.SelectCharacter(true,"");
		instance.SelectCharacter(false, character);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			// ShowRoaster();
		}
	}
	public bool shouldIntro = true;
	public static System.Action teamChanged;
	public static void ApplyRoaster()
	{
		InkOverlord.IO.SetCharactersInTeam(instance.team[0], instance.team[1]);
		InterfaceManager.Instance.Fade(Color.black, 2f,()=>{
			if(instance.shouldIntro)
			{
				instance.shouldIntro = false;
		        InterfaceManager.Instance.RequestDisplay(BaseDisplay.DISPLAY_TYPES.TEXT, "INTRO");
			} 

		Destroy(instance.characterSelectionScreen);});
		if(teamChanged != null)
			teamChanged.Invoke();

	}

	[ConsoleCommand]
	public static void UnlockCharacter(string character)
	{
		if(!instance.unlockedCharacters.Contains(character))
		{
			instance.unlockedCharacters.Add(character);
		}
	}
	GameObject characterSelectionScreen;
	private void Start()
	{
		instance = this;
		peopleDict = new Dictionary<string, PeopleDatas>();
		foreach(PeopleDatas p in peoples)
		{
			peopleDict[p.name] = p;
		}
		unlockedCharacters = new List<string>();
		unlockedCharacters.Add("Cartographer");
		InkOverlord.IO.eventDelegate += CheckUnlock;

		ShowRoaster();
	}

	void CheckUnlock(string s)
	{
		if(s.StartsWith("UNLOCK"))
		{
			UnlockCharacter(s.Split(' ')[1]);
		}
	}

	string[] team = new string[2];
	public void SelectCharacter(bool isMain, string character)
	{
		int id = isMain?0:1;
		Color c = Color.clear;
		if(team[id] == character || string.IsNullOrEmpty(character))
			team[id] = string.Empty;

		else
		{
			team[id] = character;
			c = peopleDict[character].color;
		}
	
		peopleLines[id].DOColor(c,3f);
	}

	public static bool HasMain()
	{
		return !string.IsNullOrEmpty(instance.team[0]);
	}
}
