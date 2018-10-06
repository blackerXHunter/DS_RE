using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{

    public override void OnPlayableCreate (Playable playable)
    {
        
    }
    PlayableDirector pd;
    public override void OnGraphStart(Playable playable) {

        pd = (PlayableDirector) playable.GetGraph().GetResolver();
        foreach (var track in pd.playableAsset.outputs) {
            if (track.streamName == "Attacker Script" || track.streamName == "Victim Script") {
                ActorManager am =(ActorManager) pd.GetGenericBinding(track.sourceObject);
                am.Lock(true);
            }
        }
        
        base.OnGraphStart(playable);
    }

    public override void OnGraphStop(Playable playable) {
        foreach (var track in pd.playableAsset.outputs) {
            if (track.streamName == "Attacker Script" || track.streamName == "Victim Script") {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                am.Lock(false);
            }
        }
        base.OnGraphStop(playable);
    }

}
