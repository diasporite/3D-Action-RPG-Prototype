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

        [Header("Face Button Menus")]
        public HUDMenu basicSkillset;
        public HUDMenu leftWeaponSkillset;
        public HUDMenu rightWeaponSkillset;

        HUDMenu[] dpadMenus;
        HUDMenu[] skillsets;

        private void Awake()
        {
            dpadMenus = new HUDMenu[] { regItems, activeParty };
            skillsets = new HUDMenu[] { basicSkillset, leftWeaponSkillset, rightWeaponSkillset };
        }

        private void Start()
        {
            ChangeDPadMenu(1);
            ChangeButtonMenu(0);
        }

        public void ChangeDPadMenu(int j)
        {
            for (int i = 0; i < skillsets.Length; i++)
            {
                if (i == j) skillsets[i].gameObject.SetActive(true);
                else skillsets[i].gameObject.SetActive(false);
            }
        }

        public void ChangeDPadMenu(DPadMenuState state)
        {
            int j = (int)state;

            for (int i = 0; i < skillsets.Length; i++)
            {
                if (i == j) skillsets[i].gameObject.SetActive(true);
                else skillsets[i].gameObject.SetActive(false);
            }
        }

        public void ChangeButtonMenu(int j)
        {
            for (int i = 0; i < skillsets.Length; i++)
            {

                if (i == j) skillsets[i].gameObject.SetActive(true);
                else skillsets[i].gameObject.SetActive(false);
            }
        }

        public void ChangeButtonMenu(SkillMenuState state)
        {
            int j = (int)state;

            for (int i = 0; i < skillsets.Length; i++)
            {

                if (i == j) skillsets[i].gameObject.SetActive(true);
                else skillsets[i].gameObject.SetActive(false);
            }
        }
    }
}