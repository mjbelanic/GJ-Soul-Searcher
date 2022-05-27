using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Transform _target;

    public float rotSpeed = 1.5f;

    private float _rotY;
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = _target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        if (horInput != 0)
        {
            _rotY += horInput * rotSpeed;
        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = _target.position - (rotation * _offset);
        transform.LookAt(_target);
    }
}
