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
        //public event OnHealthTick onHealthTick;
        //public event OnStaminaTick onStaminaTick;
        //public event OnPoiseTick onPoiseTick;

        public event Action onHealthTick;
        public event Action onStaminaTick;
        public event Action onPoiseTick;

        public event Action<int> onDamage;
        public event Action onDeath;

        public event OnHealthChanged onHealthChanged;
        public event OnStaminaChanged onStaminaChanged;
        public event OnPoiseChanged onPoiseChanged;

        protected int partyCap = 4;
        [SerializeField] int currentMember = 0;
        [SerializeField] List<Controller> party = new List<Controller>();
        [SerializeField] List<Controller> activeParty = new List<Controller>();

        [SerializeField] Vector3 currentPos = new Vector3(0, 0, 0);
        [SerializeField] Controller currentPartyMember;

        Health partyHealth;
        Stamina partyStamina;
        ActionQueue actionQueue;
        ShortcutRegister shortcuts;
        LockOn lockOn;
        InputController inputController;
        AIController ai;

        public List<Controller> Party => party;
        public List<Controller> ActiveParty => activeParty;

        public Controller CurrentPartyMember
        {
            get
            {
                if (currentMember < activeParty.Count)
                    return activeParty[currentMember];
                return null;
            }
        }
                
        public BattleChar CurrentCharacter
        {
            get
            {
                if (CurrentPartyMember != null)
                    return CurrentPartyMember.Combatant.Character;
                return null;
            }
        }

        public Combatant CurrentCombatant
        {
            get
            {
                if (CurrentPartyMember != null)
                    return CurrentPartyMember.Combatant;
                return null;
            }
        }

        public Health PartyHealth => partyHealth;
        public Stamina PartyStamina => partyStamina;
        public ActionQueue ActionQueue => actionQueue;
        public ShortcutRegister Shortcuts => shortcuts;
        public LockOn LockOn => lockOn;
        public InputController InputController => inputController;
        public AIController AIController => ai;

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

        public int TotalStamina
        {
            get
            {
                int stamina = 0;

                foreach (var member in activeParty)
                    if (member != null)
                        stamina += member.Character.Stamina.CurrentStatValue;

                return stamina;
            }
        }

        public int AverageStamina => Mathf.RoundToInt((float)TotalStamina / activeParty.Count);

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
            partyStamina = GetComponent<Stamina>();
            actionQueue = GetComponent<ActionQueue>();
            shortcuts = GetComponent<ShortcutRegister>();
            lockOn = GetComponent<LockOn>();
            inputController = GetComponent<InputController>();
            ai = GetComponent<AIController>();
        }

        private void Update()
        {
            if (CurrentPartyMember != null)
            {
                currentPos = CurrentPartyMember.transform.position;
                transform.position = currentPos;
            }
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

        public void InvokeDamage(int damage)
        {
            onDamage?.Invoke(damage);
        }

        public void InvokeDeath()
        {
            onDeath?.Invoke();
        }
        #endregion

        public void InitParty()
        {
            partyHealth.InitResource();
            partyStamina.InitResource();
        }

        public void AddPartyMember(Controller member)
        {
            if (activeParty.Count < partyCap) activeParty.Add(member);
        }

        public void RemovePartyMember(Controller member)
        {
            if (activeParty.Contains(member)) activeParty.Remove(member);
        }

        public void ChangePartyMember(int index)
        {
            if (activeParty.Count <= 0) return;

            index = Mathf.Abs(index);
            index = index % party.Count;

            //CurrentPartyMember.gameObject.SetActive(false);

            currentMember = index;
            //CurrentPartyMember.gameObject.SetActive(true);

            for(int i = 0; i < activeParty.Count; i++)
            {
                if (i == currentMember)
                {
                    activeParty[i].gameObject.SetActive(true);
                    activeParty[i].transform.position = currentPos;
                }
                else activeParty[i].gameObject.SetActive(false);
            }

            currentPartyMember = CurrentPartyMember;
            onCharacterChanged?.Invoke(CurrentCombatant);
        }

        public void ChangePartyComp()
        {

        }

        public void GetActiveParty()
        {
            for (int i = 0; i < 4; i++)
                if (i < party.Count)
                    activeParty[i] = party[i];
        }
    }
}