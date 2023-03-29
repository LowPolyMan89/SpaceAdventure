using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour, IPaused
{
    [SerializeField] private DoubleSideBar distanceBar;
    [SerializeField] private TMP_Text allDistanceTMP;
    [SerializeField] private TMP_Text playerSpeedTMP;
    [SerializeField] private TMP_Text enemySpeedTMP;
    [SerializeField] private TMP_Text distanceTypeTMP;

    [SerializeField] private AbilityPanel abilityPanel;
    [SerializeField] private Transform shipSelectTransform;
    [SerializeField] private Button SelectNextShipButton;
    [SerializeField] private TMP_Text selectedShipNameTMP;

    private Battle _battle;
    [SerializeField] private int currentShipIndx = 0;
    [SerializeField] private Weapon selectedWeapon;
    [SerializeField] private Transform tmpSelectTarget;
    [SerializeField] private List<GameObject> DisabledUIfromPause = new List<GameObject>();
    

    private void Awake()
    {
        Events.OnPauseEvent += Pause;
    }

    private void Start()
    {
        SelectNextShipButton.onClick.AddListener(SelectNextShipButtonClick);
        _battle = Battle.Instance;
    }

    public bool Pause(bool val)
    {
        SelectShip(_battle.PlayerShips[currentShipIndx]);
        DisabledUIfromPause.ForEach(d => d.SetActive(val));
        return val;
    }
    
    public void SelectShip(Ship ship)
    {
        StartCoroutine(SelectShipRoutine(ship));
    }

    private IEnumerator SelectShipRoutine(Ship ship)
    {
        yield return new WaitForSeconds(0.1f);
        selectedShipNameTMP.text = ship.gameObject.name;
        shipSelectTransform.position = ship.transform.position;
        abilityPanel.ShipImage.sprite = ship.ShipStats.StatsData.ShipSprite;
        
        abilityPanel.Slots.ForEach(F => F.gameObject.SetActive(false));

        for (var index = 0; index < ship.WeaponController.WeaponSlots.Count; index++)
        {
            var weaponSlot = ship.WeaponController.WeaponSlots[index];
            abilityPanel.Slots[index].gameObject.SetActive(true);
            abilityPanel.Slots[index].abilityImage.sprite = weaponSlot.MountedWeapon.Gun.AmmoStats.WeaponSprite;
            abilityPanel.Slots[index].weapon = weaponSlot.MountedWeapon;
            abilityPanel.Slots[index].Init(this);
        }
    }

    public void SelectWeaponFromSlot(Weapon select)
    {
        selectedWeapon = select;
    }

    public void SelectNextShipButtonClick()
    {
        int allshipcount = _battle.PlayerShips.Count;
        
        if (allshipcount - 1 >= currentShipIndx + 1)
        {
            currentShipIndx++;
        }
        else
        {
            currentShipIndx = 0;
        }

        SelectShip(_battle.PlayerShips[currentShipIndx]);
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Selection"))
                {
                    if (hit.transform.gameObject.GetComponent<ShipSelection>().SelectShip.Side == ShipSide.Player)
                    {
                        selectedWeapon = null;
                        currentShipIndx =
                            _battle.PlayerShips.IndexOf(hit.transform.gameObject.GetComponent<ShipSelection>().SelectShip);
                        SelectShip(_battle.PlayerShips[currentShipIndx]);
                    }
                    else if(selectedWeapon)
                    {
                        selectedWeapon.AddTarget(hit.transform.gameObject.GetComponent<ShipSelection>().SelectShip.transform);
                        selectedWeapon = null;
                    }
                    else
                    {
                        _battle.PlayerShips[currentShipIndx].AI.SelectNewTarget(false, hit.transform.gameObject
                            .GetComponent<ShipSelection>().SelectShip.transform);
                    }
                }
            }
            
            selectedWeapon = null;
        }
        
        tmpSelectTarget.gameObject.SetActive(selectedWeapon);
        
        distanceBar.Amount(_battle.Distance / 100);

        allDistanceTMP.text = _battle.Distance.ToString("F0") + " km";
        playerSpeedTMP.text = (_battle.GetSlowestShip(_battle.PlayerShips) * 10f).ToString("F0") + " m/s";
        enemySpeedTMP.text = (_battle.GetSlowestShip(_battle.EnemyShips) * 10f).ToString("F0") + " m/s";

        switch (_battle.CurrentDistanceType)
        {
            case Distance.Long:
                distanceTypeTMP.text = "Long Range";
                distanceTypeTMP.color = Color.blue;
                distanceBar.Color(Color.blue);
                break;
            case Distance.Medium:
                distanceTypeTMP.text = "Medium Range";
                distanceTypeTMP.color = Color.yellow;
                distanceBar.Color(Color.yellow);
                break;
            case Distance.Close:
                distanceTypeTMP.text = "Close Range";
                distanceTypeTMP.color = Color.red;
                distanceBar.Color(Color.red);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDestroy()
    {
        Events.OnPauseEvent -= Pause;
    }
}



[System.Serializable]
public class DoubleSideBar
{
    public UnityEngine.UI.Image barLSide;
    public UnityEngine.UI.Image barRSide;

    public void Amount(float val)
    {
        barLSide.fillAmount = val;
        barRSide.fillAmount = val;
    }

    public void Color(Color color)
    {
        barLSide.color = color;
        barRSide.color = color;
    }
}
