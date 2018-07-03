using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float horizontal = 100f;
    public float vertical = 50f;

    [SerializeField]
    private GameObject playerHandle;
    [SerializeField]
    private GameObject cameraHandle;
    [SerializeField]
    private GameObject cameraPos;

    private GameObject model;

    private GameObject cam;

    private UserInput playerInput;

    private float tempEulerX;

    private Vector3 smoothDampVec;

    public float smoothSpeed = .1f;
    [SerializeField]
    public GameObject lockTarget;

    // Use this for initialization
    private void Start() {
        model = playerHandle.GetComponent<ActorController>().model;
        cam = Camera.main.gameObject;
        playerInput = UserInput.GetEnabledUserInput(playerHandle);
    }

    // Update is called once per frame
    private void FixedUpdate() {

        Vector3 tempModelEuler = model.transform.rotation.eulerAngles;

        if (playerHandle != null) {
            playerHandle.transform.Rotate(playerHandle.transform.up, playerInput.Jright * horizontal * Time.fixedDeltaTime);
        }
        if (cameraHandle != null) {
            tempEulerX -= playerInput.Jup * vertical * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -15, 25);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
        }

        model.transform.rotation = Quaternion.Euler(tempModelEuler);
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cameraPos.transform.position, ref smoothDampVec, .2f);
        //cam.transform.eulerAngles = cameraPos.transform.eulerAngles;
        cam.transform.LookAt(cameraHandle.transform);
    }

    private void LateUpdate() {
        if (playerInput.lockUnlock) {
            Debug.Log("LockUnLock");
            LockUnLock();
        }
    }

    public void LockUnLock() {
        if (lockTarget == null) {
            var originPos1 = model.transform.position;
            var originPos2 = originPos1 + new Vector3(0, 1, 0);
            var center = originPos2 + model.transform.forward * 5;
            var cols = Physics.OverlapBox(center, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask("Enemy"));
            foreach (var col in cols) {
                lockTarget = col.gameObject;
            }
        }
        else {

        }
    }
}
