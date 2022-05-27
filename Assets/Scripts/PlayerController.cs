using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float jumpSpeed = 15.0f;
    public float rotationTime = .1f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public float pushForce = 3.0f;
    public CanvasGroup pausePanel;
    public CanvasGroup gameOverPanel;
    public bool pauseUp = false;
    public bool gameOverUp = false;
    public HUD display;
    
    private Vector3 moveDirection;
    private CharacterController _characterController;
    private Vector3 velocity;
    private float _vertspeed;
    private ControllerColliderHit _contact;

    [SerializeField]
    private Transform _cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Vector3.zero;
        float deltaX = Input.GetAxisRaw("Horizontal");
        float deltaZ = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(deltaX, 0f, deltaZ).normalized;
        if (movement.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        bool hitGround = false;
        RaycastHit hit;
        if (_vertspeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_characterController.height + _characterController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertspeed = jumpSpeed;
            }
            else
            {
                _vertspeed = minFall;
            }
        }
        else
        {
            _vertspeed += gravity * 5 * Time.deltaTime;
            if (_vertspeed < terminalVelocity)
            {
                _vertspeed = terminalVelocity;
            }
            if (_characterController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * speed;
                }
                else
                {
                    movement += _contact.normal * speed;
                }
            }

        }

        moveDirection.y = _vertspeed;
        _characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
    }
}
