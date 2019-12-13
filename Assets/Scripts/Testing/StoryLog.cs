 using System.Collections.Generic;
 using System.Xml;
 using System.Xml.Serialization;
 using System.IO;


[System.Serializable]
public class InteractionLog
{
    public string interactiveName;
    public string interactiveClass;
    public InteractionLog()
    {

    }
}
 [System.Serializable]
public class StoryLog
{
    public string fullPathName;
    public bool hasBeenFastForward;
    public float totalReadingTime;

    public StoryLog()
    {
        
    }

    public StoryLog(string path)
    {
        fullPathName = path;
    }
}

[System.Serializable]
[XmlRoot("PlaythroughLog")]
public class PlaythroughHistory
{
    public PlayerToken token;

    [XmlArray("StoryLogs")]
    [XmlArrayItem("StoryLog")]
    public List<StoryLog> storyLogs;

    [XmlArray("InteractionLogs")]
    [XmlArrayItem("InteractionLog")]
    public List<InteractionLog> interactionLogs;

    public PlaythroughHistory()
    {

    }
    public byte[] Save(List<StoryLog> logsList, string path)
    {
        storyLogs = logsList;
        var serializer = new XmlSerializer(typeof(PlaythroughHistory));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
        return File.ReadAllBytes(path);
    }

    public static PlaythroughHistory Load(string path)
    {
        var serializer = new XmlSerializer(typeof(PlaythroughHistory));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as PlaythroughHistory;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static PlaythroughHistory LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(PlaythroughHistory));
        return serializer.Deserialize(new StringReader(text)) as PlaythroughHistory;
    }
}