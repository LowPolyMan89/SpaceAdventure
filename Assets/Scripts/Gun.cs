using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private float _rps;
    [SerializeField] private AmmoStatsData _ammoStats;
    
    private float lastFireTime;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool isReloading;
    [SerializeField] private bool isFiring;
    [SerializeField] private int ammoCapacity;

    public bool IsReloading => isReloading;

    public AmmoStatsData AmmoStats => _ammoStats;

    public bool IsFiring => isFiring;

    private void Start()
    {
        reloadTime = _ammoStats.ReloadingTime;
        isFiring = true;
        _rps = _ammoStats.RPS;
        ammoCapacity = _ammoStats.AmmoCount;
    }

    void Update()
    {
        if(Events.isPaused) return;
        Reloading();
    }
    
    private void Reloading()
    {
        if (reloadTime <= 0f)
        {
            BigReload();
        }

        if (isReloading)
        {
            reloadTime -= Time.deltaTime;
        }
        
        if(IsFiring) return;
        
        if (Time.time > lastFireTime + 1 / _rps)
        {
            lastFireTime = Time.time;
            isFiring = true;
        }
    }

    private void BigReload()
    {
        
        isReloading = false;
        isFiring = true;
        reloadTime = _ammoStats.ReloadingTime;
        ammoCapacity = _ammoStats.AmmoCount + 1;
    }

    public void Shoot(Transform firePoint, Transform target)
    {
        isFiring = false;
        if(isReloading) return;
        
        if (ammoCapacity < 1)
        {
            isReloading = true;
        }
        else
        {
            ammoCapacity--;
            Ammo ammo = Instantiate(_ammoPrefab, firePoint.position, Quaternion.identity).GetComponent<Ammo>();
            ammo.CreateAmmo(target, _ammoStats);
        }
        
    }


}
