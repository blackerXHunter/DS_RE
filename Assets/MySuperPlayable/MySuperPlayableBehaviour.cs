using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour {
    public float myFloat = 5;
    public ActorManager am;
    public String command;
    public override void OnPlayableCreate (Playable playable) {

    }

    public override void OnGraphStart (Playable playable) {

    }

    public override void OnGraphStop (Playable playable) {
        am.SendCommand ("Lock", false);
    }
    public override void OnBehaviourPlay (Playable playable, FrameData info) {
        if (!String.IsNullOrEmpty (command)) {
            Debug.Log("Send Command "+command);
            am.SendCommand (command);
            return;
        }
        base.OnBehaviourPlay (playable, info);
    }

    public override void OnBehaviourPause (Playable playable, FrameData info) {
        am.SendCommand ("Lock", false);
        base.OnBehaviourPause (playable, info);
    }

    public override void PrepareFrame (Playable playable, FrameData info) {
        am.SendCommand ("Lock", true);
        base.PrepareFrame (playable, info);
    }
}