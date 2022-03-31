using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "SkillDatabase", menuName = "Combat/Database/Skill")]
    public class SkillDatabase : ScriptableObject
    {
        [SerializeField] SkillData[] skills;
        Dictionary<string, SkillData> skillDict = new Dictionary<string, SkillData>();

        public Skill GetSkill(string name)
        {
            if (skillDict.ContainsKey(name))
                return skillDict[name].GetSkill();

            Debug.LogError("No skill named " + name + " exists.");
            return null;
        }

        public void InitDatabase()
        {
            skillDict.Clear();

            foreach (var s in skills)
                if (!skillDict.ContainsValue(s))
                    skillDict.Add(s.skillName, s);
        }
    }
}