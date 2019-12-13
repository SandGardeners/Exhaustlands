using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInteractive : Interactive {

	public UnityEvent m_event;

    protected override void OnInteract()
    {
        m_event.Invoke();
    }
}
