using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public float reach = 2f;

	protected GameObject target;
    public Vector3 offset;
    public Vector3 testOffset = new Vector3(0f, 0f, 0f);

    protected float xAngle;
    protected float yAngle;

    private GameObject LookAtTarget;

    private bool _disabled = false;

    // Start is called before the first frame update
    void Start()
    {
		target = GameObject.FindGameObjectWithTag("Player");
        yAngle = transform.eulerAngles.y;
        if (target)
        {
            SetLookAtTarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
		if (!target)
		{
			target = GameObject.FindGameObjectWithTag("Player");
            if (target)
            {
                SetLookAtTarget();
            }
		}
	}

    public void Disable(bool value)
    {
        _disabled = value;
    }

    private void SetLookAtTarget()
    {
        LookAtTarget = new GameObject("Look At Target");
        //LookAtTarget.transform.SetPositionAndRotation(new Vector3(1f, 0f, 1f), Quaternion.Euler(0f, 0f, 0f));
        LookAtTarget.transform.SetParent(target.transform);
        LookAtTarget.transform.localPosition = new Vector3(1f, 0f, 1f);
    }

    private void LateUpdate()
    {
        if (_disabled) { return; }
		float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;

        xAngle -= vertical;
        xAngle = Mathf.Clamp(xAngle, -20f, 60.0f);

        // Camera Rotation
        yAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(xAngle, yAngle, 0.0f);
        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(LookAtTarget.transform.position);

        // Target Rotation
        //target.transform.rotation = Quaternion.LookRotation(transform.forward, transform.up) * Quaternion.Euler(0f, 0f, 0f);
        if (horizontal != 0f)
        {
            Vector3 diff = LookAtTarget.transform.position - target.transform.position;
            diff.y -= horizontal;
            target.transform.rotation = Quaternion.Slerp(Quaternion.Euler(testOffset), target.transform.rotation * Quaternion.Euler(0f, horizontal, 0f), Time.time);
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect((Screen.width / 2) - 10, (Screen.height / 2), 1, 1), "");
    }
}
