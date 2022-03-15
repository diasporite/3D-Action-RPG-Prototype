using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Stamina : Resource
    {
        [SerializeField] bool inRecovery = false;

        public bool InRecovery
        {
            get => inRecovery;
            set => inRecovery = value;
        }

        protected override void Awake()
        {
            party = GetComponent<PartyManager>();
            combatant = GetComponent<Combatant>();

            currentRegen = regenSpeed;
        }

        public override void InitResource()
        {
            var sp = party.PartySp;

            resourcePoints = new PointStat(sp, sp, 999);

            resource._cooldown = sp;
            resource.Count = resourcePoints.PointValue;

            UpdateUI();
        }

        protected override void UpdateUI()
        {
            party.InvokeStaminaTick();
        }

        public void Run(bool value)
        {
            if (value) CurrentRegen = GameManager.instance.Combat.staminaRunRegen;
            else CurrentRegen = GameManager.instance.Combat.staminaRegen;
        }
    }
}