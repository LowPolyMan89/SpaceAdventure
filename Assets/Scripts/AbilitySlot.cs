
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

    public class AbilitySlot: MonoBehaviour
    {
        public Image abilityImage;
        public Button abilityButton;
        public Weapon weapon;
        public BattleUI ui;

        public void Init(BattleUI _ui)
        {
            ui = _ui;
            abilityButton.onClick.RemoveAllListeners();
            abilityButton.onClick.AddListener(Click);
            
        }

        public void Click()
        {
            ui.SelectWeaponFromSlot(weapon);
        }
    }
