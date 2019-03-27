using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour {
    public float myFloat = 5;
    public ActorManager am;
    public override void OnPlayableCreate(Playable playable) {

    }

    public override void OnGraphStart(Playable playable) {

    }

    public override void OnGraphStop(Playable playable) {

    }

    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        base.OnBehaviourPlay(playable, info);
    }
    
    public override void OnBehaviourPause(Playable playable, FrameData info) {
        am.SendCommand("Lock",false);
        base.OnBehaviourPause(playable, info);
    }

    public override void PrepareFrame(Playable playable, FrameData info) {
        am.SendCommand("Lock",true);
        base.PrepareFrame(playable, info);
    }
}
