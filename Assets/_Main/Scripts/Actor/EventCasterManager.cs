using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCasterManager : IActorManager
{
    public enum EventCasterType
    {
        item,
        box,
        lever,
        attack
    }
    public EventCasterType ect = EventCasterType.item;
    public string eventName;
    private bool _active;
    public bool active
    {
        get { return _active; }
        set
        {
            _active = value;
            OnActiveChanged?.Invoke(value);
        }
    }
    public UnityAction<bool> OnActiveChanged;
    public Vector3 offset = new Vector3(0, 0, 1);

    // Use this for initialization
    void Start()
    {
        if (am == null)
        {
            am = gameObject.GetComponentInParent<ActorManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
