using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float horizontal = 100f;
    public float vertical = 50f;

    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    private GameObject playerHandle;
    [SerializeField]
    private GameObject cameraHandle;

    private GameObject model;

    private GameObject cam;

    private PlayerInput playerInput;

    private float tempEulerX;

    private Vector3 smoothDampVec;

    public float smoothSpeed = .1f;

    // Use this for initialization
    private void Start()
    {
        model = playerHandle.GetComponent<ActorController>().model;
        cam = Camera.main.gameObject;
        playerInput = playerHandle.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        Vector3 tempModelEuler = model.transform.rotation.eulerAngles;

        if (playerHandle != null)
        {
            playerHandle.transform.Rotate(playerHandle.transform.up, input.Jright * horizontal * Time.fixedDeltaTime);
        }
        if (cameraHandle != null)
        {
            tempEulerX -= playerInput.Jup * vertical * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -15, 25);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
        }

        model.transform.rotation = Quaternion.Euler(tempModelEuler);
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cameraHandle.transform.position, ref smoothDampVec, .2f);
        cam.transform.eulerAngles = cameraHandle.transform.eulerAngles;
    }
}
