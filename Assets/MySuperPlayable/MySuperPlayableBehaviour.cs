using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour {
    public float myFloat = 5;
    public ActorManager am;
    public String command;
    public override void OnPlayableCreate(Playable playable) {

    }

    public override void OnGraphStart(Playable playable) {
        if (String.IsNullOrEmpty(command))
        {
            return;
        }
        Debug.Log("Do Graph Start!!!");
        am.SendCommand(command);
    }

    public override void OnGraphStop(Playable playable) {

    }
    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        //am.SendCommand(command);
        //Debug.Log("Behaviour Play!!");
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
