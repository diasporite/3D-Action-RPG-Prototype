using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class PointStatf : Statf
    {
        [SerializeField] float pointValue;

        public float _pointValue
        {
            get => pointValue;

            set
            {
                pointValue = value;

                if (pointValue < 0) pointValue = 0;
                if (pointValue > _currentStatValue) pointValue = _currentStatValue;
            }
        }

        public float _allStatValues
        {
            set
            {
                statValue = value;
                pointValue = value;
            }
        }

        public float _pointsFraction
        {
            get
            {
                if (currentStatValue != 0) return pointValue / currentStatValue;
                return 0;
            }
        }

        #region Constructors and Init
        public PointStatf(float statValue) : base(statValue)
        {
            valueCap = 999;

            InitPointValues(statValue);
        }

        public PointStatf(float statValue, float statCap) : base(statValue, statCap)
        {
            InitPointValues(statValue);
        }

        public PointStatf(float statValue, float pointValue, float statCap) : base(statValue, statCap)
        {
            InitPointValues(pointValue);
        }

        protected override void InitValues(float value)
        {
            //valueCap = GameManager.instance._pointsCap;

            StatValue = value;
            currentStatValue = value;
        }

        void InitPointValues(float pointValue)
        {
            this.pointValue = pointValue;

            if (pointValue < 0) this.pointValue = 0;
            if (pointValue > statValue) this.pointValue = statValue;
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

        public float GetPointFraction(float value)
        {
            return value / currentStatValue;
        }
    }
}
