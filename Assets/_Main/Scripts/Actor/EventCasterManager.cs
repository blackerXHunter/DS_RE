using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManager {
    public enum EventCasterType
    {
        item,
        box,
        lever,
        attack
    }
    public EventCasterType ect = EventCasterType.item;
    public string eventName;
    public bool active;
    public Vector3 offset = new Vector3(0, 0, 1);

    // Use this for initialization
    void Start() {
        if (am == null) {
            am = gameObject.GetComponentInParent<ActorManager>();
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
