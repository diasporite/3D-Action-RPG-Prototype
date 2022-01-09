using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PartyManager : MonoBehaviour
    {
        public OnCharacterChanged onCharacterChanged;

        public OnHealthChanged onHealthChanged;
        public OnStaminaChanged onStaminaChanged;
        public OnPoiseChanged onPoiseChanged;

        int partyCap = 8;
        int currentMember = 0;
        [SerializeField] List<Controller> party = new List<Controller>();

        [SerializeField] Vector3 currentPos = new Vector3(0, 0, 0);

        public Controller CurrentPartyMember => party[currentMember];

        public Controller GetPartyMember(int index)
        {
            index = Mathf.Abs(index);

            if (index < party.Count) return party[index];

            return null;
        }

        private void Update()
        {
            if (CurrentPartyMember != null)
                currentPos = CurrentPartyMember.transform.position;
        }

        public void ChangePartyMember(int index)
        {
            index = Mathf.Abs(index);
            if (index > party.Count)
            {
                CurrentPartyMember.gameObject.SetActive(false);

                currentMember = index;
                CurrentPartyMember.gameObject.SetActive(true);
            }
        }
    }
}