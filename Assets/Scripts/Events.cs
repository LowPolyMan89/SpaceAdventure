using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
   public static Func<Ship, Ship> OnNewShipSpawnEvent;
   public static Func<Ship, Ship> OnShipDestroyEvent;
   public static Func<bool, bool> OnPauseEvent;
   public static bool isPaused;
   
   public static Ship OnNewShipSpawnAction(Ship ship)
   {
      if (OnNewShipSpawnEvent != null)
      {
         OnNewShipSpawnEvent(ship);
      }
      return ship;
   }

   public static Ship OnNewShipDestroyAction(Ship ship)
   {
      if (OnShipDestroyEvent != null)
      {
         OnShipDestroyEvent(ship);
      }
      return ship;
   }
   
   public static bool OnPauseAction(bool val)
   {
      if (OnPauseEvent != null)
      {
         isPaused = val;
         OnPauseEvent(val);
      }
      return val;
   }
   
}
