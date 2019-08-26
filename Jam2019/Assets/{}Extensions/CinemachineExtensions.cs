using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineExtensions : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private float desiredFOV;

    public float ZoomSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float currentFOV = vcam.m_Lens.FieldOfView;
            float mouseAxis = Input.GetAxis("Mouse ScrollWheel");
            desiredFOV = mouseAxis * 2;

            Debug.Log("desiredFOV: " + desiredFOV);
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                vcam.m_Lens.FieldOfView = Mathf.Lerp(currentFOV, mouseAxis * 2, Time.deltaTime * ZoomSpeed);
            }
            else
            {
                vcam.m_Lens.FieldOfView = Mathf.Lerp(mouseAxis * 2, currentFOV, Time.deltaTime * ZoomSpeed);
            }
        }
    }
}
