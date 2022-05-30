using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1f;

    private CinemachineComposer composer;
    // Start is called before the first frame update
    void Start()
    {
        composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        //composer.m_TrackedObjectOffset.y += vertical;
        //composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -10, 10);

    }
}
