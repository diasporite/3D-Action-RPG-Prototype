using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Health : Resource
    {
        protected override void Awake()
        {
            party = GetComponent<PartyManager>();
            combatant = GetComponent<Combatant>();

            currentRegen = regenSpeed;
        }

        protected override void Start()
        {
            //base.Start();

            //party.onHealthChanged +=
        }

        protected override void OnDestroy()
        {
            //party.onHealthChanged -=
        }

        protected override void UpdateUI()
        {
            party.InvokeHealthTick();
        }

        public override void SaveToCharacter()
        {
            character.Health.PointValue = resourcePoints.PointValue;
            character.HealthResource = resource.Count;
        }

        public override void LoadFromCharacter()
        {
            //print(324);
            resourcePoints = character.Health;
            resource._cooldown = resourcePoints.CurrentStatValue;
            resource.Count = resourcePoints.PointValue;
        }

        public override void SaveToStat(PointStat stat)
        {
            base.SaveToStat(stat);
        }

        public override void LoadFromStat(PointStat stat)
        {
            base.LoadFromStat(stat);
        }

        public void InitResource()
        {
            var currentHp = resourcePoints.PointValue;
            var hp = party.TotalHealth;

            resourcePoints = new PointStat(100, 3999);

            resourcePoints.StatValue = hp;
            resourcePoints.PointValue = currentHp;

            resource._cooldown = hp;
            resource.Count = resourcePoints.PointValue;

            UpdateUI();
        }
    }
}