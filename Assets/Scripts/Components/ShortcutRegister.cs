using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum ShortcutMode
    {
        ActiveParty = 0,
        RegisteredItems = 1,
    }

    public class ShortcutRegister : MonoBehaviour
    {
        public event OnActivePartySelect onActivePartySelect;
        public event OnRegisteredItemSelect onRegItemSelect;
        public event OnShortcutSwitch onShortcutSwitch;

        [SerializeField] ShortcutMode mode;

        [SerializeField] PartyManager party;

        Controller[] activeParty;

        public ShortcutMode Mode => mode;

        private void Awake()
        {
            party = GetComponent<PartyManager>();
        }

        private void Start()
        {
            activeParty = party.ActiveParty.ToArray();
        }

        private void Update()
        {
            SwitchShortcut();
        }

        public void InitRegister()
        {
            InitActiveParty();
            InitRegisteredItems();
        }

        public void InitActiveParty()
        {
            activeParty = party.ActiveParty.ToArray();
        }

        public void InitRegisteredItems()
        {

        }

        public void SwitchShortcut()
        {
            if (Input.GetKeyDown("i"))
            {
                print(1);
                switch (mode)
                {
                    case ShortcutMode.ActiveParty:
                        mode = ShortcutMode.RegisteredItems;
                        break;
                    case ShortcutMode.RegisteredItems:
                        mode = ShortcutMode.ActiveParty;
                        break;
                }

                onShortcutSwitch?.Invoke(mode);
            }
        }

        public void SelectRegistered(int index)
        {
            index = Mathf.Abs(index);
            index = index % 4;

            switch (mode)
            {
                case ShortcutMode.ActiveParty:
                    onActivePartySelect?.Invoke(index);
                    break;
                case ShortcutMode.RegisteredItems:
                    onRegItemSelect?.Invoke(index);
                    break;
            }
        }
    }
}