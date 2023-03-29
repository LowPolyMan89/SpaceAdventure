using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDShipStatus : MonoBehaviour
{
   public Ship ship;
   public Image shield;
   public Image armor;
   public Image hull;
   public TMP_Text numericText;

   public bool isDamageTextShow;
   public float accumulateDamage;
   public float damageShowTime;

   private float _showingDamageTime;
   
   private void Start()
   {
      Events.OnTakeDamageEvent += TakeDamage;
      _showingDamageTime = damageShowTime;
      numericText.text = "";
   }

   public CalculatedDamage TakeDamage(CalculatedDamage calculatedDamage)
   {
      if (ship != calculatedDamage.DamagingShip) return calculatedDamage;

      if (calculatedDamage.isMissing) return calculatedDamage;

      if (calculatedDamage.ArmorDamage > 0)
      {
         numericText.color = Color.yellow;
      }
      else if(calculatedDamage.ShieldDamage > 0)
      {
         numericText.color = Color.blue;
      }
      else if(calculatedDamage.HitPointDamage > 0)
      {
         numericText.color = Color.red;
      }
      
      if (isDamageTextShow)
      {
         _showingDamageTime += 0.5f;
         accumulateDamage += calculatedDamage.ArmorDamage + calculatedDamage.HitPointDamage +
                             calculatedDamage.ShieldDamage;
         numericText.text = "- " +accumulateDamage.ToString("F0");
      }
      else
      {
         isDamageTextShow = true;
         _showingDamageTime = damageShowTime;
         accumulateDamage = calculatedDamage.ArmorDamage + calculatedDamage.HitPointDamage +
                             calculatedDamage.ShieldDamage;
         numericText.text = "- " +accumulateDamage.ToString("F0");
      }
      return calculatedDamage;
   }

   private void Update()
   {
      if (ship)
      {
         transform.position = Camera.main.WorldToScreenPoint(ship.transform.position);

         shield.fillAmount = ship.ShipStats.Shield / ship.ShipStats.StatsData.Shield;
         armor.fillAmount = ship.ShipStats.Armor / ship.ShipStats.StatsData.Armor;
         hull.fillAmount = ship.ShipStats.HitPoint / ship.ShipStats.StatsData.HitPoint;
         
         if (_showingDamageTime < 0)
         {
            _showingDamageTime = 0;
            isDamageTextShow = false;
            numericText.text = "";
         }
         else
         {
            
            _showingDamageTime -= Time.deltaTime;
         }

      }
      else
      {
         Destroy(gameObject);
      }
   }

   private void OnDestroy()
   {
      Events.OnTakeDamageEvent -= TakeDamage;
   }
}
