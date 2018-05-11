using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour {

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void ResetTrigger(string triggerName)
    {
        animator.ResetTrigger(triggerName);
        Debug.Log(triggerName);
    }
}
