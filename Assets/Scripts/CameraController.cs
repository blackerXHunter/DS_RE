using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public class LockTarget {
        public float halfHeight
        {
            get
            {
                return obj.GetComponent<Collider>().bounds.size.y / 2;
            }
        }
        public GameObject obj;
        public ActorManager am;

        public LockTarget(GameObject obj) {
            this.obj = obj;
            am = obj.GetComponent<ActorManager>();
        }
    }

    public float horizontal = 100f;
    public float vertical = 50f;

    [SerializeField]
    private GameObject playerHandle;
    [SerializeField]
    private GameObject cameraHandle;
    [SerializeField]
    private GameObject cameraPos;

    [SerializeField]
    private bool isAI;

    private GameObject model;

    private GameObject cam;

    private UserInput playerInput;

    private float tempEulerX;

    private Vector3 smoothDampVec;

    public float smoothSpeed = .1f;
    [SerializeField]
    public LockTarget lockTarget = null;

    public bool lockState = false;

    public Image lockDot;

    // Use this for initialization
    private void Start() {
        model = playerHandle.GetComponent<ActorController>().model;
        cam = Camera.main.gameObject;
        playerInput = UserInput.GetEnabledUserInput(playerHandle);
    }

    // Update is called once per frame
    private void FixedUpdate() {

        if (lockTarget == null) {


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
        }
        //else {
        //Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
        //playerHandle.transform.forward = tempForward;
        //tempForward.y = 0;
        //}
        if (!isAI) {
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cameraPos.transform.position, ref smoothDampVec, .2f);
            //cam.transform.eulerAngles = cameraPos.transform.eulerAngles;
            cam.transform.LookAt(cameraHandle.transform);
        }
    }

    private void LateUpdate() {
        if (playerInput.lockUnlock) {
            Debug.Log("LockUnLock");
            LockUnLock();
        }
        if (lockTarget != null) {
            if (!isAI) {
                lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
            }
            playerHandle.transform.LookAt(lockTarget.obj.transform);
            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f) {
                UnLock();
            }
            if (lockTarget.am != null && lockTarget.am.sm.HPisZero) {
                UnLock();
            }
        }
    }

    public void LockUnLock() {

        var originPos1 = model.transform.position;
        var originPos2 = originPos1 + new Vector3(0, 1, 0);
        var center = originPos2 + model.transform.forward * 5;
        var cols = Physics.OverlapBox(center, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask(isAI ? "Player" : "Enemy"));


        if (lockTarget != null) {
            UnLock();
        }
        else {
            foreach (var col in cols) {
                Lock(col);
                break;
            }
        }
    }

    private void UnLock() {
        lockTarget = null;
        lockState = false;
        if (!isAI) {
            lockDot.enabled = false;
        }
    }
    private void Lock(Collider target) {
        lockTarget = new LockTarget(target.gameObject);
        lockState = true;
        if (!isAI) {
            lockDot.enabled = true;
        }
    }
}
