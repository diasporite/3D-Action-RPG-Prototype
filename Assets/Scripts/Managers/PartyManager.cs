using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PartyManager : MonoBehaviour
    {
        // Delegates for UI
        public event OnCharacterChanged onCharacterChanged;

        // Read up on C# actions
        public event OnHealthTick onHealthTick;
        public event OnStaminaTick onStaminaTick;
        public event OnPoiseTick onPoiseTick;

        public event OnHealthChanged onHealthChanged;
        public event OnStaminaChanged onStaminaChanged;
        public event OnPoiseChanged onPoiseChanged;

        protected int partyCap = 8;
        [SerializeField] int currentMember = 0;
        [SerializeField] List<Controller> party = new List<Controller>();
        [SerializeField] Controller[] activeParty = new Controller[4];

        [SerializeField] Vector3 currentPos = new Vector3(0, 0, 0);

        Health partyHealth;
        ActionQueue actionQueue;
        ShortcutRegister shortcuts;
        LockOn lockOn;

        public List<Controller> Party => party;
        public Controller[] ActiveParty => activeParty;

        public Controller CurrentPartyMember => activeParty[currentMember];
        public BattleChar CurrentCharacter => CurrentPartyMember.Combatant.Character;

        public Health PartyHealth => partyHealth;
        public ActionQueue ActionQueue => actionQueue;
        public ShortcutRegister Shortcuts => shortcuts;
        public LockOn LockOn => lockOn;

        public int TotalHealth
        {
            get
            {
                int health = 0;

                foreach (var member in activeParty)
                    if (member != null)
                        health += member.Character.Health.CurrentStatValue;

                return health;
            }
        }

        public Controller GetPartyMember(int index)
        {
            if (party.Count <= 0) return null;

            index = Mathf.Abs(index);
            index = index % party.Count;

            return party[index];
        }

        public Controller GetActivePartyMember(int index)
        {
            index = Mathf.Abs(index);
            index = index % 4;

            return party[index];
        }

        private void Awake()
        {
            partyHealth = GetComponent<Health>();
            actionQueue = GetComponent<ActionQueue>();
            shortcuts = GetComponent<ShortcutRegister>();
            lockOn = GetComponent<LockOn>();
        }

        private void Update()
        {
            if (CurrentPartyMember != null)
                currentPos = CurrentPartyMember.transform.position;
        }

        // Events can only be invoked from within the class they are located in
        #region InvokeDelegates
        public void InvokeCharChange(Combatant combatant)
        {
            // ?. - shorthand for null check
            onCharacterChanged?.Invoke(combatant);
        }

        public void InvokeHealthTick()
        {
            onHealthTick?.Invoke();
        }

        public void InvokeStaminaTick()
        {
            onStaminaTick?.Invoke();
        }

        public void InvokePoiseTick()
        {
            onPoiseTick?.Invoke();
        }
        #endregion

        public void InitParty()
        {
            party.Clear();
            activeParty = new Controller[4];

            Controller[] partyMembers = GetComponentsInChildren<Controller>();
            print(partyMembers.Length);
            foreach (var member in partyMembers)
            {
                if (party.Count < partyCap)
                {
                    member.InitController();
                    party.Add(member);
                }
            }

            for (int i = 0; i < 4; i++)
                if (i < party.Count) activeParty[i] = party[i];

            partyHealth.InitResource();
        }

        public void ChangePartyMember(int index)
        {
            if (party.Count <= 0) return;

            index = Mathf.Abs(index);
            index = index % party.Count;

            //CurrentPartyMember.gameObject.SetActive(false);

            currentMember = index;
            //CurrentPartyMember.gameObject.SetActive(true);

            for(int i = 0; i < party.Count; i++)
            {
                if (i == currentMember)
                {
                    activeParty[i].gameObject.SetActive(true);
                    activeParty[i].transform.position = currentPos;
                }
                else activeParty[i].gameObject.SetActive(false);
            }

            onCharacterChanged?.Invoke(CurrentPartyMember.Combatant);
        }

        public void ChangePartyComp()
        {

        }
    }
}