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

    // Update is called once per frame
    private void Update()
    {
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

        run = Input.GetButton(btn0);

        bool newJump = Input.GetButton(btn1);
        if (newJump != lastJump && newJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;

        bool newAttack = Input.GetButton(btn5);
        if (newAttack != lastAttack && newAttack == true)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;
    }

}
