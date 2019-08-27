using UnityEngine;
using UnityEditor;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraZoomController : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    public float minFov = 15f;
    public float maxFov = 90f;
    public float sensitivity = 10f;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        float fov = vcam.m_Lens.FieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        vcam.m_Lens.FieldOfView = fov;
    }
}