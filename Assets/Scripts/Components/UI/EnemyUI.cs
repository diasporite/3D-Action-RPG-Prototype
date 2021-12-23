using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class EnemyUI : CombatantUI, ICombatantUI
    {
        //EnemyController enemy;

        //PlayerController player;

        //[SerializeField] Slider staggerBar;

        [SerializeField] Text spdText;

        public Text _spdText => spdText;

        protected override void Awake()
        {
            base.Awake();

            //enemy = GetComponent<EnemyController>();
        }

        public override void UpdateUI()
        {
            base.UpdateUI();
        }
    }
}