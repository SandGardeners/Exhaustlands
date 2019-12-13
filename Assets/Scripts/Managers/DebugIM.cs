using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InterfaceManager : MonoBehaviour 
{
    public GameObject BugReporter;
	
    GameObject inReportMode;
	
    private List<InteractionLog> interactionLogs;

    //DEBUG COMPONENT
    public void LogInteractions(PlaythroughHistory history)
    {
        history.interactionLogs = interactionLogs;
    }

    void LogInteraction(string _name, string _class)
    {
        InteractionLog log = null;
        if(interactionLogs != null)
        {
            log = new InteractionLog();
            log.interactiveName = _name;
            log.interactiveClass = _class;
        }
        if (!inCarriage)
        {
            if(interactionLogs != null)
                interactionLogs.Add(log);
        }
        else
        {
            if(log != null)
            {
                //pointController._steering.OnArrival += (_) => { interactionLogs.Add(log); };
            }
            else
            {

            }
        }
    }
    
}
