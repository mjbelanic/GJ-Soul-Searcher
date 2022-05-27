using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureDevice : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private float _projectileFiringPeriod = 1f;
    [SerializeField]
    private Transform _projectileSpawn;

    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _projectileFiringPeriod)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _timer = 0f;
                FireProjectile();
            }
        }
    }

    private void FireProjectile()
    {
        GameObject projectile;
        projectile = Instantiate(_projectilePrefab, _projectileSpawn.transform.position, Quaternion.identity);
    }
}
