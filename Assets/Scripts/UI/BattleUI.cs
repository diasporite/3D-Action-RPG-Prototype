using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum DPadMenuState
    {
        Items = 0,
        Party = 1,
    }

    public enum SkillMenuState
    {
        BasicSkills = 0,
        LeftWeaponSkills = 1,
        RightWeaponSkills = 2,
    }

    public class BattleUI : MonoBehaviour
    {
        //public DPadMenu dPadMenu;

        [Header("D-Pad Menus")]
        public HUDMenu activeParty;
        public HUDMenu regItems;

        HUDMenu[] dpadMenus;

        private void Awake()
        {
            dpadMenus = new HUDMenu[] { regItems, activeParty };
        }

        private void Start()
        {
            ChangeDPadMenu(1);
        }

        public void ChangeDPadMenu(int j)
        {
            for (int i = 0; i < dpadMenus.Length; i++)
            {
                if (i == j) dpadMenus[i].gameObject.SetActive(true);
                else dpadMenus[i].gameObject.SetActive(false);
            }
        }

        public void ChangeDPadMenu(DPadMenuState state)
        {
            int j = (int)state;

            for (int i = 0; i < dpadMenus.Length; i++)
            {
                if (i == j) dpadMenus[i].gameObject.SetActive(true);
                else dpadMenus[i].gameObject.SetActive(false);
            }
        }
    }
}