using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineExtensions : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    public float ZoomSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            vcam.m_Lens.FieldOfView -= ZoomSpeed;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            vcam.m_Lens.FieldOfView += ZoomSpeed;
        }
    }
}
