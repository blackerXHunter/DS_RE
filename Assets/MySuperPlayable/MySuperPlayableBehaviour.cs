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
    PlayableDirector pd;
    public override void OnGraphStart(Playable playable) {
        pd = (PlayableDirector)playable.GetGraph().GetResolver();
    }

    public override void OnGraphStop(Playable playable) {
        if (pd != null) {
            pd.playableAsset = null;
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        base.OnBehaviourPlay(playable, info);
    }
    
    public override void OnBehaviourPause(Playable playable, FrameData info) {
        am.Lock(false);
        base.OnBehaviourPause(playable, info);
    }

    public override void PrepareFrame(Playable playable, FrameData info) {
        am.Lock(true);
        base.PrepareFrame(playable, info);
    }
}
