using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitvity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {   
        // Making sure my mouse doesn't leave the Game Window
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitvity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitvity * Time.deltaTime;

        xRotation -= mouseY;
        // making sure we can onle look 90?up and 90?down and don't get sick :D
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
