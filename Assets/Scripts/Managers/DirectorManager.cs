using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManager {
    public PlayableDirector pd;
    // Use this for initialization

    [Header("=== Timeline Assets ===")]
    public TimelineAsset frontStab;


    //[Header("=== Assets Settings ===")]
    //public ActorManager attcker;
    //public ActorManager victim;

    void Start() {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;

        
    }

    public void PlayFrontStab(ActorManager attacker, ActorManager victim) {
        pd.playableAsset = Instantiate(frontStab);

        foreach (var track in pd.playableAsset.outputs) {
            if (track.streamName == "Attacker Script") {
                pd.SetGenericBinding(track.sourceObject, attacker);
            }
            else if (track.streamName == "Victim Script") {
                pd.SetGenericBinding(track.sourceObject, victim);
            }
            else if (track.streamName == "Attacker Animation") {

                pd.SetGenericBinding(track.sourceObject, attacker.ac.GetAnimator());
            }
            else if (track.streamName == "Victim Animation") {
                pd.SetGenericBinding(track.sourceObject, victim.ac.GetAnimator());
            }
        }
        pd.Play();
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetKey(KeyCode.H) && gameObject.layer == LayerMask.NameToLayer("Player")) {
        //    pd.Play();
        //}
    }
}
