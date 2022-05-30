using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOBController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Soul")
        {
            LostSoulController soul = other.gameObject.GetComponent<LostSoulController>();
            soul.ResetPosition();
        }
    }
}
