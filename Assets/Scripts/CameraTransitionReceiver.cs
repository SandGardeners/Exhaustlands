using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionReceiver : MonoBehaviour, ICommandReceiver
 {
	Command command;
	void ICommandReceiver.ExecuteCommand(Command _command, params object[] _params)
	{
		command = _command;
		brain = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
		((GameObject)(_params[0])).SetActive(true);
		StartCoroutine(CheckForEndTransition());
	}

    Cinemachine.CinemachineBrain brain;
	IEnumerator CheckForEndTransition()
	{
		while(cameraTransitionPercentage() != 1f)
		{
			yield return null;
		}
		command.Complete();
		Destroy(gameObject);
	}

	bool transitionStarted;
    public float cameraTransitionPercentage()
    {
        if (brain.ActiveBlend != null)
        {
            transitionStarted = true;
            float percentage = brain.ActiveBlend.TimeInBlend / brain.ActiveBlend.Duration;
            return percentage;
        }
		else
		{
			if(transitionStarted)
			{
                transitionStarted = false;
                return 1.0f;
            }
			else
			{
            	return -1.0f;
            }
		}
    }
}
