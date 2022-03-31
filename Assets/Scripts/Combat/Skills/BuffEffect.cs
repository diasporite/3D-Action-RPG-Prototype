using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class BuffEffect : SkillEffect
    {
        [SerializeField] int buffPercent;

        public BuffEffect() : base()
        {

        }
    }
}