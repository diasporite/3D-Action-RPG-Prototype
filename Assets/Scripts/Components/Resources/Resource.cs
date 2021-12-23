using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] bool regenerative = true;

        [SerializeField] int points = 100;
        [SerializeField] int initPoints = 0;

        [SerializeField] float regenSpeed = 30;
        [SerializeField] float currentRegen;

        [SerializeField] protected PointStat resourcePoints;
        [SerializeField] protected Cooldown resource = new Cooldown(100, 30);

        [SerializeField] protected Slider statBar;
        [SerializeField] protected Text statText;

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

        public Cooldown _resource => resource;

        public bool Empty => resource.Empty;
        public bool Full => resource.Full;

        private void Start()
        {
            currentRegen = regenSpeed;
            resourcePoints = new PointStat(points, initPoints, points);
            resource = new Cooldown(points, regenSpeed);

            resource.Speed = regenSpeed;
            resource.Count = initPoints;

            UpdateUI();
        }

        private void Update()
        {
            //if (regenerative) Tick(Time.deltaTime);
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

        protected virtual void UpdateUI()
        {
            if (statBar != null)
                statBar.value = resource.CooldownFraction;
        }

        public void ChangeResource(int amount)
        {
            resourcePoints.PointValue += amount;
            resource.Count += amount;
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

        public void ConvertToStat()
        {

        }
    }
}