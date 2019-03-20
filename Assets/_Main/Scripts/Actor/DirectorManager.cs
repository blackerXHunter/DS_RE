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
    public TimelineAsset openBox;
    public TimelineAsset leverUp;


    //[Header("=== Assets Settings ===")]
    //public ActorManager attcker;
    //public ActorManager victim;

    void Start() {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
    }

    private void PlayFrontStab(ActorManager attacker, ActorManager victim) {

        if (pd.state == PlayState.Playing) {
            return;
        }
        pd.playableAsset = Instantiate(frontStab);
        TimelineAsset timeline = (TimelineAsset) pd.playableAsset;

        foreach (var track in timeline.GetOutputTracks()) {
            if (track.name == "Attacker Script") {
                pd.SetGenericBinding(track, attacker);
                foreach (var clip in track.GetClips()) {
                    MySuperPlayableClip mySuperPlayableClip = (MySuperPlayableClip) clip.asset;
                    //MySuperPlayableBehaviour mySuperPlayableBehaviour = mySuperPlayableClip.template;
                    mySuperPlayableClip.am.exposedName = Guid.NewGuid().ToString();
                    pd.SetReferenceValue(mySuperPlayableClip.am.exposedName, attacker);
                }
            }
            else if (track.name == "Victim Script") {
                pd.SetGenericBinding(track, victim);
                pd.SetGenericBinding(track, attacker);
                foreach (var clip in track.GetClips()) {
                    MySuperPlayableClip mySuperPlayableClip = (MySuperPlayableClip)clip.asset;
                    //MySuperPlayableBehaviour mySuperPlayableBehaviour = mySuperPlayableClip.template;
                    mySuperPlayableClip.am.exposedName = Guid.NewGuid().ToString();
                    pd.SetReferenceValue(mySuperPlayableClip.am.exposedName, victim);
                }
            }
            else if (track.name == "Attacker Animation") {
                pd.SetGenericBinding(track, attacker.ac.GetAnimator());
            }
            else if (track.name == "Victim Animation") {
                pd.SetGenericBinding(track, victim.ac.GetAnimator());
            }
        }
        pd.Evaluate();
        pd.Play();
    }

    public bool IsPlaying() {
        if (pd.state == PlayState.Playing) {
            return true;
        }
        else {
            return false;
        }
    }

    public void Play(string eventName, ActorManager attacker, ActorManager victim) {

        if (eventName == "frontStab") {
            PlayFrontStab(attacker, victim);
        }

        else if (eventName == "treasureBox") {

            pd.playableAsset = Instantiate(openBox);
            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;
            foreach (var track in timeline.GetOutputTracks()) {
                if (track.name == "Player Script") {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips()) {
                        MySuperPlayableClip mySuperPlayableClip = (MySuperPlayableClip)clip.asset;
                        //MySuperPlayableBehaviour mySuperPlayableBehaviour = mySuperPlayableClip.template;
                        mySuperPlayableClip.am.exposedName = Guid.NewGuid().ToString();
                        pd.SetReferenceValue(mySuperPlayableClip.am.exposedName, attacker);
                    }
                }
                else if (track.name == "Box Script") {
                    pd.SetGenericBinding(track, victim);
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips()) {
                        MySuperPlayableClip mySuperPlayableClip = (MySuperPlayableClip)clip.asset;
                        //MySuperPlayableBehaviour mySuperPlayableBehaviour = mySuperPlayableClip.template;
                        mySuperPlayableClip.am.exposedName = Guid.NewGuid().ToString();
                        pd.SetReferenceValue(mySuperPlayableClip.am.exposedName, victim);
                    }
                }
                else if (track.name == "Player Animation") {
                    pd.SetGenericBinding(track, attacker.ac.GetAnimator());
                }
                else if (track.name == "Box Animation") {
                    pd.SetGenericBinding(track, victim.ac.GetAnimator());
                    victim.ac.GetAnimator().SetTrigger("open");
                }
            }
            pd.Evaluate();
            pd.Play();
        }

        else if (eventName == "leverUp") {
            pd.playableAsset = Instantiate(leverUp);
            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;
            foreach (var track in timeline.GetOutputTracks()) {
                if (track.name == "Player Script") {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips()) {
                        MySuperPlayableClip mySuperPlayableClip = (MySuperPlayableClip)clip.asset;
                        //MySuperPlayableBehaviour mySuperPlayableBehaviour = mySuperPlayableClip.template;
                        mySuperPlayableClip.am.exposedName = Guid.NewGuid().ToString();
                        pd.SetReferenceValue(mySuperPlayableClip.am.exposedName, attacker);
                    }
                }
                else if (track.name == "Lever Script") {
                    pd.SetGenericBinding(track, victim);
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips()) {
                        MySuperPlayableClip mySuperPlayableClip = (MySuperPlayableClip)clip.asset;
                        //MySuperPlayableBehaviour mySuperPlayableBehaviour = mySuperPlayableClip.template;
                        mySuperPlayableClip.am.exposedName = Guid.NewGuid().ToString();
                        pd.SetReferenceValue(mySuperPlayableClip.am.exposedName, victim);
                    }
                }
                else if (track.name == "Player Animation") {
                    pd.SetGenericBinding(track, attacker.ac.GetAnimator());
                }
                else if (track.name == "Lever Animation") {
                    pd.SetGenericBinding(track, victim.ac.GetAnimator());
                    victim.ac.GetAnimator().SetTrigger("open");
                }
            }

            pd.Evaluate();
            pd.Play();
        }
        else if (eventName == "item"){
            return;
        }
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetKey(KeyCode.H) && gameObject.layer == LayerMask.NameToLayer("Player")) {
        //    pd.Play();
        //}
    }
}
