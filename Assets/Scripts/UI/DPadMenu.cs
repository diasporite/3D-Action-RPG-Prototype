using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class DPadMenu : MonoBehaviour
    {
        [SerializeField] PartyManager party;
        ShortcutRegister shortcuts;

        [SerializeField] HUDMenu activePartyMenu;
        [SerializeField] HUDMenu regItemsMenu;

        private void OnDisable()
        {
            UnsubscribeFromDelegates();
        }

        public void InitUI(PartyManager party)
        {
            this.party = party;
            shortcuts = party.GetComponent<ShortcutRegister>();

            SubscribeToDelegates();
        }

        void SubscribeToDelegates()
        {
            shortcuts.onShortcutSwitch += OpenMenu;
        }

        void UnsubscribeFromDelegates()
        {
            shortcuts.onShortcutSwitch -= OpenMenu;
        }

        void OpenMenu()
        {
            switch (shortcuts.Mode)
            {
                case ShortcutMode.ActiveParty:
                    OpenActiveParty();
                    break;
                case ShortcutMode.RegisteredItems:
                    OpenRegItems();
                    break;
            }
        }

        void OpenActiveParty()
        {
            activePartyMenu.gameObject.SetActive(true);
            regItemsMenu.gameObject.SetActive(false);
        }

        void OpenRegItems()
        {
            regItemsMenu.gameObject.SetActive(true);
            activePartyMenu.gameObject.SetActive(false);
        }
    }
}