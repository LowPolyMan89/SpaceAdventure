using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _turretBase;
    [SerializeField] private Transform _fireTurretHead;

    [SerializeField] private Gun _gun;
    
    [SerializeField] private Transform _targetTransform;

    public Gun Gun => _gun;

    private void Update()
    {
        if(!_targetTransform) return;
        
        RotateToTarget();

        if (Gun.IsFiring)
        {
            Shoot();
        }
    }

    public void AddTarget(Transform target)
    {
        _targetTransform = target;
    }

    public void Shoot()
    {
        Gun.Shoot(_firePoint, _targetTransform);
    }

    private void RotateToTarget()
    {
        Vector3 direction = _targetTransform.position - _turretBase.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _turretBase.rotation = Quaternion.Slerp(_turretBase.rotation, targetRotation, 25 * Time.deltaTime);
    }
    
}
