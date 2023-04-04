using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SServices : MonoBehaviour
{
   [SerializeField] private SBuffDatabase _database;
   public static SServices Instance;
   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(this);
      }
      
      _database.Init();
      
      DontDestroyOnLoad(this.gameObject);
   }
}
