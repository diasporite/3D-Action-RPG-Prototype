using System;
using UnityEngine;

namespace RPG_Project
{
    [Serializable]
    public class PointStat : Stat
    {
        [SerializeField] int pointValue;

        public int PointValue
        {
            get => pointValue;

            set
            {
                pointValue = value;

                if (pointValue < 0) pointValue = 0;
                if (pointValue > CurrentStatValue) pointValue = CurrentStatValue;
            }
        }

        public int _allStatValues
        {
            set
            {
                statValue = value;
                pointValue = value;
            }
        }

        public float PointFraction
        {
            get
            {
                if (currentStatValue != 0) return (float)pointValue / currentStatValue;
                return 0;
            }
        }
        #region Constructors and Init
        public PointStat(int statValue) : base(statValue)
        {
            valueCap = 999;

            InitPointValues(statValue);
        }

        public PointStat(int statValue, int statCap) : base(statValue, statCap)
        {
            InitPointValues(statValue);
        }

        public PointStat(int statValue, int pointValue, int statCap) : base(statValue, statCap)
        {
            InitPointValues(pointValue);
        }

        protected override void InitValues(int value)
        {
            //valueCap = GameManager.instance._pointsCap;

            StatValue = value;
            currentStatValue = value;
        }

        void InitPointValues(int pointValue)
        {
            this.pointValue = pointValue;

            if (pointValue < 0) pointValue = 0;
            if (pointValue > statValue) pointValue = statValue;
        }
        #endregion

        public void ChangeCurrentPoints(int points)
        {
            pointValue += points;
            if (pointValue < 0) pointValue = 0;
            if (pointValue > statValue) pointValue = statValue;
        }

        public void ChangeCurrentPointsFraction(int fraction)
        {
            int change = Mathf.RoundToInt(fraction * statValue);

            ChangeCurrentPoints(change);
        }

        public float GetPointFraction(int value)
        {
            return (float)value / currentStatValue;
        }
    }
}