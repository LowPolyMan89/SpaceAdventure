using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ammo
{
    private Vector3 startPos;           
    private Vector3 targetPos;          
    private float distance;             
    private float startTime;            
    private bool isCreate;
    private Transform _target;
    private AmmoStatsData _ammoStatsData;

    private bool isMiss;
    private bool isCheck;
    private Ship atackerShip;

    public Ship AtackerShip => atackerShip;

    public override void CreateAmmo(Transform target, AmmoStatsData ammoStatsData, Ship attacker)
    {
        atackerShip = attacker;
        _ammoStatsData = ammoStatsData;
        _target = target;
        startTime = Time.time;
        startPos = transform.position;
        targetPos = target.position;
        distance = Vector3.Distance(startPos, targetPos);
        isCreate = true;
        transform.LookAt(_target);
    }

    void Update()
    {
        if(!isCreate) return;
        //if(Events.isPaused) return;
        
        float timeSinceStart = Time.time - startTime;
        float fractionOfJourney = timeSinceStart * _ammoStatsData.AmmoSpeed / distance;
       // transform.position = Vector3.Lerp(startPos, targetPos, fractionOfJourney);
       
       transform.position += transform.forward * Time.deltaTime * _ammoStatsData.AmmoSpeed;
       
       if (_target && !isCheck)
        {
            if (fractionOfJourney >= 1 ||Vector3.Distance(transform.position, startPos) > maxDistance)
            {
                IDamagable damagable = null;

                damagable = _target.gameObject.GetComponent<Ship>().DamageDetector;

                if (_target != damagable.GetSelfTransform()) return;

                isMiss = damagable.TakeDamage(_ammoStatsData, atackerShip).isMissing;
                
                isCheck = true;
                
                if(!isMiss)
                    Destroy(gameObject);
            }
        }
       
        if(Vector3.Distance(transform.position, startPos) > maxDistance)
        {
            Destroy(gameObject);
        }

    }
    
}

internal interface  IDamagable
{
    CalculatedDamage TakeDamage(AmmoStatsData ammoStatsData, Ship attackerShip);
    Transform GetSelfTransform();
}
