using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class DPadMenu : UIElement
    {
        ShortcutRegister shortcuts;

        [SerializeField] HUDMenu activePartyMenu;
        [SerializeField] HUDMenu regItemsMenu;

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);

            shortcuts = party.GetComponent<ShortcutRegister>();
        }

        protected override void SubscribeToDelegates()
        {
            shortcuts.onShortcutSwitch += OpenMenu;
        }

        protected override void UnsubscribeFromDelegates()
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