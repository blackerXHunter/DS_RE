using UnityEngine;

public abstract class IActorController : MonoBehaviour {
    public GameObject model;
    public Animator actor;
    public abstract void Issue(string msg, object[] obj);
}