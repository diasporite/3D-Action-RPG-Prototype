using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{

    public class DPadMenu : MonoBehaviour
    {
        public UIPanel menuSwitchPanel;

        int currentMenu = 0;
        [SerializeField] HUDMenu[] menus = new HUDMenu[3];

        private void Awake()
        {
            menuSwitchPanel.InitPanel();
            menuSwitchPanel.onButtonPress += SwitchMenu;
        }

        void SwitchMenu()
        {
            menus[currentMenu].gameObject.SetActive(false);
            currentMenu++;
            currentMenu = currentMenu % menus.Length;
            menus[currentMenu].gameObject.SetActive(true);
        }
    }
}