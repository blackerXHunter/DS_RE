using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

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

    [Header("===== output signals =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dforward;

    // 1. pressing signal
    public bool run;
    // 2. trigger once signal
    public bool jump;
    private bool lastJump;

    [Header("===== others =====")]
    public bool inputEnable = true;

    private float DupTarget;
    private float DrightTarget;
    private float DupVelocity;
    private float DrightVelocity;

    // Use this for initialization
    private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
        DupTarget = Input.GetKey(keyUp)?1f:0f - (Input.GetKey(keyDown)?1f:0f);
        DrightTarget = Input.GetKey(keyRight)?1f:0f - (Input.GetKey(keyLeft)?1f:0f);

        if (!inputEnable)
        {
            DupTarget = 0;
            DrightTarget = 0;
        }


        Dup = Mathf.SmoothDamp(Dup, DupTarget, ref DupVelocity, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, DrightTarget, ref DrightVelocity, 0.1f);
        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));

        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dforward = transform.forward * Dup2 + transform.right * Dright2;

        run = Input.GetKey(keyA);

        bool newJump = Input.GetKey(keyB);
        if (newJump != lastJump && newJump == true)
        {
            jump = true;
            Debug.Log("jump!");
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - input.y * input.y/2);
        output.y = input.y * Mathf.Sqrt(1 - input.x * input.x/2);
        return output;
    }

}
