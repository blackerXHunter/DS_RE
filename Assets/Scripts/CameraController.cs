using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float horizontal = 100f;
    public float vertical = 50f;

    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    private GameObject playerHandle;
    [SerializeField]
    private GameObject cameraHandle;

	// Use this for initialization
	private void Awake () {

	}
	
	// Update is called once per frame
	private void Update () {
        if (playerHandle != null)
        {
            playerHandle.transform.Rotate(Vector3.up, input.Jright * horizontal * Time.deltaTime);
        }
        if (cameraHandle != null)
        {
            cameraHandle.transform.Rotate(Vector3.right, input.Jup * vertical * Time.deltaTime);
        }
    }
}
