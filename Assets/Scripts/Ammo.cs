using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{ 
    public float maxDistance = 1000f;    
    public Vector3 target; 

    public virtual void CreateAmmo(Transform target, AmmoStatsData _ammoStatsData)
    {
        
    }
}
