using UnityEngine;
using UnityEngine.Events;

public abstract class IActorController : MonoBehaviour
{

    public UnityAction OnAction;
    public GameObject model;
    public Animator actor;

    public void IssueTrigger(string triggerSign)
    {
        actor.SetTrigger(triggerSign);
    }

    public void IssueBool(string boolSign, bool val)
    {
        actor.SetBool(boolSign, val);
    }

    public Animator GetAnimator()
    {
        return actor;
    }

}