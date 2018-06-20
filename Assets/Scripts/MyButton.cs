using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton {

    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool isExtending = false;

    private bool curState = false;
    private bool lastState = false;
    private MyTimer timer = new MyTimer();

    public void Tick(bool input) {
        timer.Tick(Time.deltaTime);
        curState = input;

        IsPressing = input;
        OnReleased = false;
        OnPressed = false;
        if (curState != lastState) {
            if (curState == true) {
                OnPressed = true;
            }
            else {
                OnReleased = true;

                timer.StartTimer(.15f);
            }
        }
        if (timer.Running()) {
            isExtending = true;
        }
        else {
            isExtending = false;
        }
        lastState = curState;
    }

}
