using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer {
    public enum STATE {
        Idle,
        Run,
        Finished
    }

    public STATE state;

    public float duration = 1.0f;

    private float elapsedTime = 0;

    public void Tick(float dt) {
        switch (state) {
            case STATE.Idle:
                break;
            case STATE.Run:
                elapsedTime += dt;
                if (elapsedTime >= duration) {
                    elapsedTime = 0;
                    state = STATE.Finished;
                }
                break;
            case STATE.Finished:
                break;
            default:
                break;
        }
    }

    public void StartTimer(float duration) {
        this.duration = duration;
        state = STATE.Run;
    }

    public bool Finishied() {
        return state == STATE.Finished;
    }

    public bool Running() {
        return state == STATE.Run;
    }
}
