using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderCam : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed = 5;
    public float clampAngle = 60f;
    public bool active = true;

    Vector3 offset;

    private float rotX;

    void LateUpdate()
    {
        if (!active) { return; }

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");

            transform.position = target.transform.position - new Vector3(0f, 0f, 5f);
            offset = target.transform.position - transform.position;

            rotX = 0f;
        }

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        rotX -= vertical;
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(rotX, desiredAngle, 0f);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}
