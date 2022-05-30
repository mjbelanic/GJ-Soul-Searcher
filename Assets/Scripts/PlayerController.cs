using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3000.0f;
    public float rotationSpeed = 3000.0f;
    private CharacterController characterController;
    public HUD hud;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var horInput = Input.GetAxis("Horizontal");
        var verInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horInput, 0, verInput);

        transform.Rotate(Vector3.up, horInput * rotationSpeed * Time.deltaTime);

        if(verInput != 0)
        {
            characterController.SimpleMove(transform.forward * speed * verInput);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Soul")
        {
            LostSoulController soul = hit.gameObject.GetComponent<LostSoulController>();
            if(soul.agent.isStopped){
                hud.UpdateScore(1);
                soul.Captured();
            }
        }
    }
}
