using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.2193396f, 0.5483491f, 0.8773585f)]
[TrackClipType(typeof(MySuperPlayableClip))]
[TrackBindingType(typeof(DS_RE.AnimatedObjectController))]
public class MySuperPlayableTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MySuperPlayableMixerBehaviour>.Create (graph, inputCount);
    }
}
