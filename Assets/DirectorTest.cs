using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorTest : MonoBehaviour {

    public PlayableDirector pd;

    public Animator attackerAnim;
    public Animator victimerAnim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey( KeyCode.H)) {
            foreach (var track in pd.playableAsset.outputs) {
                if (track.streamName == "Attacker Animation") {
                    pd.SetGenericBinding(track.sourceObject, attackerAnim);
                }
                else if (track.streamName == "Victim Animation") {
                    pd.SetGenericBinding(track.sourceObject, victimerAnim);
                }
            }
            pd.Play();
        }
	}
}
