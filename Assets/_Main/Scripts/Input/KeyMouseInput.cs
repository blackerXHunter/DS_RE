using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMouseInput : UserInput {

    //variable
    [Header("===== key settings =====")]
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;
    public string keyM2;

    public string keyQ;
    public string keyE;
    public string keyI;

    public string keyJUp;
    public string keyJRight;
    public string keyJLeft;
    public string keyJDown;

    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonC = new MyButton();
    public MyButton buttonD = new MyButton();
    public MyButton buttonM2 = new MyButton();
    public MyButton buttonQ = new MyButton();
    public MyButton buttonE = new MyButton();
    public MyButton buttonI = new MyButton();
    // Update is called once per frame
    private void Update () {

        buttonA.Tick(Input.GetKey(keyA));
        buttonB.Tick(Input.GetKey(keyB));
        buttonC.Tick(Input.GetKey(keyC));
        buttonD.Tick(Input.GetKey(keyD));
        buttonM2.Tick(Input.GetKey(keyM2));
        buttonQ.Tick(Input.GetKey(keyQ));
        buttonE.Tick(Input.GetKey(keyE));
        buttonI.Tick(Input.GetKey(keyI));

        Jup = Input.GetKey(keyJUp)?1.0f:0 - (Input.GetKey(keyJDown)?1.0f:0);
        Jright = Input.GetKey(keyJRight) ? 1.0f : 0 - (Input.GetKey(keyJLeft) ? 1.0f : 0);

        DupTarget = Input.GetKey(keyUp)?1f:0f - (Input.GetKey(keyDown)?1f:0f);
        DrightTarget = Input.GetKey(keyRight)?1f:0f - (Input.GetKey(keyLeft)?1f:0f);

        if (!inputEnable) {
            DupTarget = 0;
            DrightTarget = 0;
        }


        Dup = Mathf.SmoothDamp(Dup, DupTarget, ref DupVelocity, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, DrightTarget, ref DrightVelocity, 0.1f);

        if (!inputEnable) {
            Dup = 0;
            Dright = 0;
        }
        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));

        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;
        if (!inputEnable) {
            Dup2 = 0;
            Dright2 = 0;
        }
        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dforward = transform.forward * Dup2 + transform.right * Dright2;

        run = (buttonA.IsPressing && !buttonA.IsDelaying) || buttonA.IsExtending;
        roll = buttonB.OnReleased && buttonB.IsDelaying;
        jump = buttonB.OnPressed && buttonA.IsPressing;

        lt = buttonQ.OnPressed;
        rb = buttonC.OnPressed;
        action = buttonE.OnPressed;
        defense = buttonD.IsPressing;
        lockUnlock = buttonM2.OnPressed;
        menu = buttonI.OnPressed;
        //Debug.Log(buttonD.isExtending && buttonD.OnPressed);
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - input.y * input.y/2);
        output.y = input.y * Mathf.Sqrt(1 - input.x * input.x/2);
        return output;
    }

}
