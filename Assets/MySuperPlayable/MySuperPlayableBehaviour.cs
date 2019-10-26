using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour {
    public float myFloat = 5;
    public DS_RE.AnimatedObjectController ac;
    public String command;
    public override void OnPlayableCreate (Playable playable) {

    }

    public override void OnGraphStart (Playable playable) {

    }

    public override void OnGraphStop (Playable playable) {
        ac.animator.SetBool("Lock", false);
        //ac.SendCommand ("Lock", false);
    }
    public override void OnBehaviourPlay (Playable playable, FrameData info) {
        if (!String.IsNullOrEmpty (command)) {
            Debug.Log("Send Command "+command);
            ac.SendCommand (command);
            return;
        }
        base.OnBehaviourPlay (playable, info);
    }

    public override void OnBehaviourPause (Playable playable, FrameData info) {
        ac.animator.SetBool("Lock", false);
        base.OnBehaviourPause (playable, info);
    }

    public override void PrepareFrame (Playable playable, FrameData info) {
        ac.animator.SetBool("Lock", true);
        base.PrepareFrame (playable, info);
    }
}