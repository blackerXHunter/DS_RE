using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DS_RE
{
    public class PlayerCameraController : MonoBehaviour
    {
        public PlayerController ac;

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

        public Image lockDot;

        private Vector3 originLocalEulerAngles = Vector3.zero;

        private float lockCamAdjustDistance = 5f;

        // Use this for initialization
        private void Start()
        {
            model = playerHandle.GetComponent<PlayerController>().model;
            cam = Camera.main.gameObject;
            playerInput = UserInput.GetEnabledUserInput(playerHandle);
            if (lockDot == null)
            {
                var ld = GameObject.Find("LockDot");
                if (ld != null)
                {
                    lockDot = ld.GetComponent<Image>();
                }
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {

            if (ac.lockController.lockTarget == null)
            {
                Vector3 tempModelEuler = model.transform.rotation.eulerAngles;

                if (playerHandle != null)
                {
                    playerHandle.transform.Rotate(playerHandle.transform.up, playerInput.Jright * horizontal * Time.fixedDeltaTime);
                }
                if (cameraHandle != null)
                {
                    tempEulerX -= playerInput.Jup * vertical * Time.fixedDeltaTime;
                    tempEulerX = Mathf.Clamp(tempEulerX, -15, 25);
                    cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
                }
                model.transform.rotation = Quaternion.Euler(tempModelEuler);
            }
            else
            {
                if (originLocalEulerAngles == Vector3.zero)
                {
                    originLocalEulerAngles = cameraHandle.transform.localEulerAngles;
                }
                // 根据人物和敌人的距离来调整摄像头角度
                float distance = Vector3.Distance(model.transform.position, ac.lockController.lockTarget.obj.transform.position);
                if (distance < lockCamAdjustDistance)
                {
                    float radio = (lockCamAdjustDistance - distance) / lockCamAdjustDistance;
                    float deltaEulerAngles = 40 * radio;
                    var localEulerAngles = new Vector3(originLocalEulerAngles.x + deltaEulerAngles, 0, 0);
                    cameraHandle.transform.localEulerAngles = localEulerAngles;
                }
                else
                {
                    cameraHandle.transform.localEulerAngles = originLocalEulerAngles;
                }

                //Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
                //playerHandle.transform.forward = tempForward;
                //tempForward.y = 0;
            }

            if (Vector3.Distance(cam.transform.position, cameraPos.transform.position) > 3)
            {
                cam.transform.position = cameraPos.transform.position;
            }
            else
            {
                cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cameraPos.transform.position, ref smoothDampVec, .2f);
            }

            cam.transform.LookAt(cameraHandle.transform);


        }

        private void LateUpdate()
        {
            if (playerInput.lockUnlock)
            {
                ac.lockController.LockUnLock();
            }
            if (ac.lockController.lockTarget != null)
            {
                lockDot.enabled = true;
                lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(ac.lockController.lockTarget.obj.transform.position + new Vector3(0, ac.lockController.lockTarget.halfHeight * 1.6f, 0));

                playerHandle.transform.LookAt(ac.lockController.lockTarget.obj.transform);

            }
            else
            {
                lockDot.enabled = false;
            }
        }
    }
}
