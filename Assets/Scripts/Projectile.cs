using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _destroyTime;


    private GameObject _spawner;
    private Vector3 _direction;
    // Start is called before the first frame update
    void Awake()
    {
        _spawner = GameObject.Find("ProjectileSpawner");
        _direction = _spawner.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
        Destroy(gameObject, _destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _speed = 0;
        Destroy(gameObject);
    }
}
