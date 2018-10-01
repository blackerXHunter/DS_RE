using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : UserInput
{

    [Header("===== key settings =====")]
    public string axisX = "axisX";
    public string axisY = "axisY";

    public string axisJright = "axis4";
    public string axisJup = "axis5";

    public string axisDpadX = "axis6";
    public string axisDpadY = "axis7";

    public string leftTrigger = "axis9";
    public string rightTrigger = "axis10";

    public string btn0 = "btn0";
    public string btn1 = "btn1";
    public string btn2 = "btn2";
    public string btn3 = "btn3";
    public string btn4 = "btn4";
    public string btn5 = "btn5";
    public string btn6 = "btn6";
    public string btn7 = "btn7";
    public string btn8 = "btn8";
    public string btn9 = "btn9";

    public MyButton button0 = new MyButton();
    public MyButton button1 = new MyButton();
    public MyButton button2 = new MyButton();
    public MyButton button3 = new MyButton();
    public MyButton button4 = new MyButton();
    public MyButton button5 = new MyButton();
    public MyButton button6 = new MyButton();
    public MyButton button7 = new MyButton();
    public MyButton button8 = new MyButton();
    public MyButton button9 = new MyButton();

    // Update is called once per frame
    private void Update()
    {
        button1.Tick(Input.GetButton(btn0));
        button1.Tick(Input.GetButton(btn1));
        button2.Tick(Input.GetButton(btn2));
        button3.Tick(Input.GetButton(btn3));
        button4.Tick(Input.GetButton(btn4));
        button5.Tick(Input.GetButton(btn5));
        button6.Tick(Input.GetButton(btn6));
        button7.Tick(Input.GetButton(btn7));
        button8.Tick(Input.GetButton(btn8));
        button9.Tick(Input.GetButton(btn9));

        DupTarget = Input.GetAxis(axisY);
        DrightTarget = Input.GetAxis(axisX);

        if (!inputEnable)
        {
            DupTarget = 0;
            DrightTarget = 0;
        }


        Dup = Mathf.SmoothDamp(Dup, DupTarget, ref DupVelocity, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, DrightTarget, ref DrightVelocity, 0.1f);
        //Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        Vector2 tempDAxis = new Vector2(Dright, Dup);

        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dforward = transform.forward * Dup2 + transform.right * Dright2;

        Jup = Input.GetAxis(axisJup);
        Jright = Input.GetAxis(axisJright);

        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dforward = transform.forward * Dup2 + transform.right * Dright2;

        run = button0.IsPressing;
        defense = button4.IsPressing;
        jump = button1.OnPressed;
        rb = button5.OnPressed;

    }

}
