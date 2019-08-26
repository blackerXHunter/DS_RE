using UnityEngine;
using UnityEngine.Events;

public abstract class IActorController : MonoBehaviour {

    public UnityAction OnAction;
    public GameObject model;
    public Animator actor;

    public void IssueTrigger (string triggerSign) {
        if (this == null)
        {
            return;
        }
        actor?.SetTrigger (triggerSign);
    }

    public void IssueBool (string boolSign, bool val) {
        if (this == null)
        {
            return;
        }
        actor?.SetBool (boolSign, val);
    }

    public Animator GetAnimator () {
        return actor;
    }

}