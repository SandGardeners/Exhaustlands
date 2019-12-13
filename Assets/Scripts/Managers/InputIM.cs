using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InterfaceManager : MonoBehaviour 
{
       void HandleInputs()
    {
        if (canProcess)
        {
            if (inReportMode == null)
            {  
                //INPUT MANAGER
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    //TODO:Process();
                }
               /* if (Input.GetKeyDown(KeyCode.S))
                {
                    IProcessable curProc = processable;
                    processable.Terminate();
                    
                    StopProcess(curProc);
                }*/
                // if(Input.GetKeyDown(KeyCode.Escape))
                // {
                //     if(pauseMenu.activeSelf)
                //     {
                //         Resume();
                //     }
                //     else
                //     {
                //         pauseMenu.SetActive(true);
                //         pauseMenu.transform.SetAsLastSibling();
                //     }
                // }

                if (Input.GetKeyDown(KeyCode.B))
                {
                    InkOverlord.IO.GetCurrentPath();
                    inReportMode = Instantiate(BugReporter, Vector3.zero, Quaternion.identity, transform);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(inReportMode);
                inReportMode = null;
            }
        }
    }
}