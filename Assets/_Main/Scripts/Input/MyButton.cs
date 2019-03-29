using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton {

    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExtending = false;
    public bool IsDelaying = false;

    private bool curState = false;
    private bool lastState = false;
    private float delayingDuraton = .3f;
    private MyTimer extendingTimer = new MyTimer();
    private MyTimer delayingTimer = new MyTimer();

    public void Tick(bool input) {
        extendingTimer.Tick(Time.deltaTime);
        delayingTimer.Tick(Time.deltaTime);
        curState = input;

        IsPressing = input;
        OnReleased = false;
        OnPressed = false;
        IsExtending = false;
        IsDelaying = false;

        if (curState != lastState) {
            if (curState == true) {
                OnPressed = true;
                delayingTimer.StartTimer(delayingDuraton);
            }
            else {
                OnReleased = true;
                extendingTimer.StartTimer(.3f);
            }
        }

        if (extendingTimer.Running()) {
            IsExtending = true;
        }
        if (delayingTimer.Running()) {
            IsDelaying = true;
        }
        lastState = curState;
    }

}
