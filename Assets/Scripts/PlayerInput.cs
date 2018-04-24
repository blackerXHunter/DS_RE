using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    //variable
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dforward;

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
        Dmag = Mathf.Sqrt(Dup * Dup + Dright * Dright);
        Dforward = transform.forward * Dup + transform.right * Dright;
    }

}
