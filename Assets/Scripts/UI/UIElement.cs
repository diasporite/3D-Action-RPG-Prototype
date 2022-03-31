using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class UIElement : MonoBehaviour
    {
        [SerializeField] protected PartyManager party;

        private void OnDisable()
        {
            UnsubscribeFromDelegates();
        }

        public virtual void InitUI(PartyManager party)
        {
            this.party = party;
        }

        protected virtual void SubscribeToDelegates()
        {

        }

        protected virtual void UnsubscribeFromDelegates()
        {

        }
    }
}