using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Skillset
    {
        [SerializeField] Skill tlSkill;
        [SerializeField] Skill trSkill;
        [SerializeField] Skill blSkill;
        [SerializeField] Skill brSkill;

        [SerializeField] List<Skill> skillset = new List<Skill>();

        public Skillset()
        {

        }
    }
}