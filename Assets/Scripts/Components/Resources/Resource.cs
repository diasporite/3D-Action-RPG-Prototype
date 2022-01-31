using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] protected bool regenerative = true;

        [SerializeField] protected float regenSpeed = 30;
        [SerializeField] protected float currentRegen;

        [SerializeField] protected PointStat resourcePoints;
        [SerializeField] protected Cooldown resource = new Cooldown(100, 30);

        protected PartyManager party;
        protected Combatant combatant;
        protected BattleChar character;

        public bool Regenerative
        {
            get => regenerative;
            set => regenerative = value;
        }

        public float CurrentRegen
        {
            get => currentRegen;
            set
            {
                currentRegen = value;
                resource.Speed = currentRegen;
            }
        }

        public PointStat ResourcePoints => resourcePoints;
        public Cooldown _resource => resource;

        public int ResourcePointValue => resourcePoints.PointValue;
        public int ResourceStatValue => resourcePoints.CurrentStatValue;
        public float ResourceFraction => resource.CooldownFraction;

        public bool Empty => resource.Empty;
        public bool Full => resource.Full;

        protected virtual void Awake()
        {
            //resourcePoints = new PointStat(points, initPoints, points);
            //resource = new Cooldown(points, regenSpeed);

            //resource.Speed = regenSpeed;
            //resource.Count = initPoints;
        }

        protected virtual void Start()
        {
        //    if (combatant != null) character = combatant.Character;

        //    LoadFromCharacter();

        //    UpdateUI();
        }

        private void Update()
        {
            //if (regenerative) Tick(Time.deltaTime);
        }

        protected virtual void OnDestroy()
        {

        }

        public virtual void InitResource()
        {
            party = GetComponentInParent<PartyManager>();
            combatant = GetComponent<Combatant>();

            currentRegen = regenSpeed;

            character = combatant.Character;

            LoadFromCharacter();

            UpdateUI();
        }

        protected virtual void UpdateUI()
        {

        }

        public void Tick(float dt)
        {
            if (regenerative)
            {
                resource.Tick(dt);

                var r = Mathf.CeilToInt(resource.Count);
                if (resourcePoints.PointValue != r)
                    resourcePoints.PointValue = r;

                UpdateUI();
            }
        }

        public int GetResourcePercent(float percent)
        {
            return Mathf.RoundToInt(percent * (float)resourcePoints.CurrentStatValue);
        }

        public void ChangeResource(int amount)
        {
            resourcePoints.PointValue += amount;
            resource.Count = resourcePoints.PointValue;

            UpdateUI();
        }

        public void ChangeResourcePercent(float percent)
        {
            var p = 0.01f * percent;

            resourcePoints.PointFraction += p;
            resource.Count = resourcePoints.PointValue;

            UpdateUI();
        }

        public void SetResource(int amount)
        {
            resourcePoints.PointValue = amount;
            resource.Count = amount;
            UpdateUI();
        }

        public void ResetSpeed()
        {
            currentRegen = regenSpeed;
            resource.Speed = currentRegen;
        }

        public virtual void SaveToCharacter()
        {
            
        }

        public virtual void LoadFromCharacter()
        {

        }

        public virtual void SaveToStat(PointStat stat)
        {

        }
        
        public virtual void LoadFromStat(PointStat stat)
        {

        }
    }
}