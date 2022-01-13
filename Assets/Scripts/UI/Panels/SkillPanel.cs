using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class SkillPanel : UIPanel
    {
        [SerializeField] Image element;
        [SerializeField] Image weapon;
        [SerializeField] Text skillName;
        [SerializeField] Text spCost;
        [SerializeField] Text uses;
    }
}