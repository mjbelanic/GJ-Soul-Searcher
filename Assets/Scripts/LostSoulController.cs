using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LostSoulController : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject Player;
    public Vector3 originalPosition;
    public float EnemyDistanceRun = 20.0f;
    public float speed = 20.0f;
    public LayerMask layerMask;
    public float timeToEscape;
    public float remaingTime;

    [SerializeField]
    GameObject soul;
    [SerializeField]
    private Material safeMaterial;
    [SerializeField]
    private Material unsafeMaterial;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        remaingTime = timeToEscape;
        originalPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if (agent.isStopped)
        {
            remaingTime -= Time.deltaTime;
            if(remaingTime < 0)
            {
                agent.isStopped = false;
                soul.GetComponent<MeshRenderer>().material = safeMaterial;
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            if (distance < EnemyDistanceRun)
            {
                bool isDirSafe = false;

                //We will need to rotate the direction away from the player if straight to the opposite of the player is a wall
                float vRotation = 0;

                while (!isDirSafe)
                {
                    //Calculate the vector pointing from Player to the Enemy
                    Vector3 dirToPlayer = transform.position - Player.transform.position;

                    //Calculate the vector from the Enemy to the direction away from the Player the new point
                    Vector3 newPos = transform.position + dirToPlayer;

                    //Rotate the direction of the Enemy to move
                    newPos = Quaternion.Euler(0, vRotation, 0) * newPos;

                    //Shoot a Raycast out to the new direction with 5f length (as example raycast length) and see if it hits an obstacle
                    bool isHit = Physics.Raycast(transform.position, newPos, out RaycastHit hit, 3f, layerMask);
                    if (hit.transform == null)
                    {
                        //If the Raycast to the flee direction doesn't hit a wall then the Enemy is good to go to this direction
                        agent.SetDestination(newPos);
                        isDirSafe = true;
                    }

                    //Change the direction of fleeing is it hits a wall by 20 degrees
                    if (isHit)
                    {
                        vRotation += 20;
                        isDirSafe = false;
                    }
                    else
                    {
                        //If the Raycast to the flee direction doesn't hit a wall then the Enemy is good to go to this direction
                        agent.SetDestination(newPos);
                        isDirSafe = true;
                    }
                }
            }
        }
    }

    public void GetCaught()
    {
        remaingTime = timeToEscape;
        soul.GetComponent<MeshRenderer>().material = unsafeMaterial;
        agent.isStopped = true;
    }

    public void Captured()
    {
        Destroy(gameObject);
    }

    public void ResetPosition()
    {
        gameObject.transform.position = originalPosition;
    }
}