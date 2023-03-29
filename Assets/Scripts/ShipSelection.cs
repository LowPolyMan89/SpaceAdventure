using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelection : MonoBehaviour
{
   [SerializeField] private Ship ship;

   public Ship SelectShip => ship;
}
