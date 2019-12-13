using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class Interactive : MonoBehaviour, ICommandReceiver, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    protected Command command;
    void ICommandReceiver.ExecuteCommand(Command _command, params object[] _params)
    {
        command = _command;
        OnInteract();      
    }
    protected abstract void OnInteract();
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if(canInteract)
        {
        }
        return;
    }

    public virtual List<Command> CommandsToExecute()
    {
        List<Command> executableCommands = new List<Command>();
        if(customCamera != null)
        {
            executableCommands.Add(new CameraTransitionCommand(null, customCamera.gameObject));
        }
        executableCommands.Add(new ConcreteCommand(this));
        return executableCommands;
    }
    

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (canInteract && renderer != null)
        {
            //TODO: interfaceManager.InteractiveCursor();
            Color c = startColor * 0.95f;
            c.a = 1.0f;
            renderer.color = c;
            if(outline != null)
                outline.enabled = true;
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (canInteract &&  renderer != null)
        {
            //TODO: interfaceManager.NormalCursor();
            renderer.color = startColor;
            if(outline != null)
                outline.enabled = false;
        }
    }


    Color startColor;
    new SpriteRenderer renderer;
    SpriteOutline outline;

    public Cinemachine.CinemachineVirtualCamera customCamera;
    [HideInInspector]
    bool _canInteract = true;
    public bool canInteract
    {
        get
        {
            return _canInteract;
        }
        set 
        {
            _canInteract = value;
        }
    }

    //REFACTORING WITH SCRIPTABLE OBJECTS
    protected virtual void StopInteraction()
	{
        if(!canInteract)
        {
            if(renderer != null)
            {
                Color c = startColor * 0.85f;
                c.a = 1.0f;
              renderer.color = c;
            }
        }

        if(customCamera != null)
        {
            customCamera.gameObject.SetActive(false);
        }

        command.Complete(); 
    }
    protected virtual void Start () 
	{
        renderer = GetComponent<SpriteRenderer>();
        if(renderer != null)
    		startColor = renderer.color;
        outline = GetComponent<SpriteOutline>();
        if(outline != null)
            outline.enabled = false;
    }
	
}
