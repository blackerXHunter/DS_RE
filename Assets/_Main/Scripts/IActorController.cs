using UnityEngine;
using UnityEngine.Events;

public abstract class IActorController : MonoBehaviour {
    
    public UnityAction OnAction;
    public GameObject model;
    public Animator actor;
    public abstract void Issue(string msg, object[] obj);
}