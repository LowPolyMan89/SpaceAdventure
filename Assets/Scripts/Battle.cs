using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Battle : MonoBehaviour, IPaused
{
   [SerializeField] private List<Ship> _playerShips = new List<Ship>();
   [SerializeField] private List<Ship> _enemyShips = new List<Ship>();
   [SerializeField] private float _distance = 100;
   [SerializeField] private Distance currentDistanceType;
   [SerializeField] private float sppedToDistanceDelimeter;
   [SerializeField] private float timeScale;
   [SerializeField] private float battleStepTime;
   public float PauseTimeDelay = 0f;
   public static Battle Instance;

   public float Distance => _distance;

   [SerializeField] private float speedSumm;

   public Distance CurrentDistanceType => currentDistanceType;

   public List<Ship> PlayerShips => _playerShips;

   public List<Ship> EnemyShips => _enemyShips;

   public float BattleStepTime => battleStepTime;

   private void Awake()
   {
      Events.OnNewShipSpawnEvent += SpawnNewShipEvent;
      Events.OnShipDestroyEvent += RemoveShipEvent;
      Events.OnPauseEvent += Pause;
      
      if(Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }

   private void Start()
   {
      speedSumm = GetSlowestShip(PlayerShips) + GetSlowestShip(EnemyShips);
      PauseTimeDelay = BattleStepTime;
      Events.OnPauseAction(true);
      AddTargetToShips();
   }

   public void NextBattleStep()
   {
      Events.OnPauseAction(false);
      StartCoroutine(BattleStep());
   }

   private IEnumerator BattleStep()
   {
      yield return new WaitForSeconds(BattleStepTime);
      PauseTimeDelay = battleStepTime;
      Events.OnPauseAction(true);
      AddTargetToShips();
   }

   private void AddTargetToShips()
   {
      foreach (var ship in _playerShips)
      {
         if (!ship.selectedTarget)
            ship.AI.SelectNewTarget(true, null);
      }
      foreach (var ship in _enemyShips)
      {
         if(!ship.selectedTarget)
            ship.AI.SelectNewTarget(true, null);
      }
   }
   
   private void Update()
   {

      Time.timeScale = timeScale;
      
      if(Events.isPaused) return;

      PauseTimeDelay -= Time.deltaTime;
      
      if (_distance <= 0)
      {
         _distance = 0;
         return;
      }
      
      _distance -= (speedSumm / sppedToDistanceDelimeter) * Time.deltaTime;

      if (_distance < 60)
      {
         currentDistanceType = global::Distance.Medium;
      }
      if (_distance < 20)
      {
         currentDistanceType = global::Distance.Close;
      }
   }

   public Ship SpawnNewShipEvent(Ship ship)
   {
      switch (ship.Side)
      {
         case ShipSide.Player:
            _playerShips.Add(ship);
            break;
         case ShipSide.Enemy:
            _enemyShips.Add(ship);
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }

      return ship;
   }
   
   public Ship RemoveShipEvent(Ship ship)
   {
      switch (ship.Side)
      {
         case ShipSide.Player:
            _playerShips.Remove(ship);
            break;
         case ShipSide.Enemy:
            _enemyShips.Remove(ship);
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }

      return ship;
   }

   public float GetSlowestShip(List<Ship> ships)
   {
      float speed = 1000f;

      foreach (var ship in ships)
      {
         speed = Mathf.Min(speed, ship.ShipStats.Speed);
      }
      
      return speed;
   }

   public Transform GetRandomEnemy(ShipSide selfSide)
   {
      Transform t = null;
      if (selfSide == ShipSide.Player && EnemyShips.Count > 0)
      {
         t = EnemyShips[Random.Range(0, EnemyShips.Count)].transform;
      }
      else if(PlayerShips.Count > 0)
      {
         t = PlayerShips[Random.Range(0, PlayerShips.Count)].transform;
      }

      return t;
   }

   private void OnDestroy()
   {
      Events.OnNewShipSpawnEvent -= SpawnNewShipEvent;
      Events.OnShipDestroyEvent -= RemoveShipEvent;
   }

   public bool Pause(bool val)
   {
      return val;
   }
}

public enum Distance
{
   Long, Medium, Close
}
