using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInteractive : Interactive {

    private InterfaceManager interfaceManager;

    public BaseDisplay.DISPLAY_TYPES displayType;
    [SerializeField]
    public Object data;

    [SerializeField]
    public string knotData;
	// Use this for initialization
    public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if(canInteract)
        {
            InterfaceManager.Instance.RequestDisplay(displayType, (displayType==BaseDisplay.DISPLAY_TYPES.TEXT)?(knotData):(data as object));
        }
    }
	protected override void Start () 
	{
		base.Start();
        interfaceManager = FindObjectOfType<InterfaceManager>();
        // processable = interfaceManager.InstantiateModule(DatasManager.prefabsLibrary[PrefabName]).GetComponent<BaseDisplay>();
	}

    protected override void OnInteract()
    {
        interfaceManager.RequestDisplay(displayType,data);
        // processable.Activate(StopInteraction);
    }
}       
