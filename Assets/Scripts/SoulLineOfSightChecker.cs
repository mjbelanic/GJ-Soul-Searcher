using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SoulLineOfSightChecker : MonoBehaviour
{
    public SphereCollider Collider;
    public float fieldOfView = 360f;
    public LayerMask lineOfSightLayer;

    public delegate void GainSightEvent(Transform target);
    public GainSightEvent OnGainSight;
    public delegate void LoseSightEvent(Transform target);
    public GainSightEvent OnLoseSight;

    private Coroutine CheckForLineOfSightCoroutine;


    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckLineOfSight(other.transform))
        {
            CheckForLineOfSightCoroutine = StartCoroutine(CheckForLineOfSight(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnLoseSight?.Invoke(other.transform);
        if(CheckForLineOfSightCoroutine != null)
        {
            StopCoroutine(CheckForLineOfSightCoroutine);
        }
    }

    private bool CheckLineOfSight(Transform target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, direction);
        if(dotProduct >= Mathf.Cos(fieldOfView))
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, Collider.radius, lineOfSightLayer))
            {
                OnGainSight?.Invoke(target);
                return true;
            }
        }
        return false;
    }

    private IEnumerator CheckForLineOfSight(Transform target)
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        while (!CheckLineOfSight(target))
        {
            yield return wait;
        }
    }
}
