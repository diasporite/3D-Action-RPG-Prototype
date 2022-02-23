using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PartyManager : MonoBehaviour
    {
        // Delegates for UI
        public event OnCharacterChanged OnCharacterChanged;

        // Read up on C# actions
        //public event OnHealthTick onHealthTick;
        //public event OnStaminaTick onStaminaTick;
        //public event OnPoiseTick onPoiseTick;

        public event Action OnHealthChange;
        public event Action OnStaminaChange;
        public event Action OnPoiseChange;

        public event Action<int> OnDamage;
        public event Action OnDeath;

        public event Action<int> OnAbilityUse;

        //public event OnHealthChanged OnHealthChanged;
        //public event OnStaminaChanged OnStaminaChanged;
        //public event OnPoiseChanged OnPoiseChanged;

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

        public Controller CurrentController
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
                if (CurrentController != null)
                    return CurrentController.Combatant.Character;
                return null;
            }
        }

        public Combatant CurrentCombatant
        {
            get
            {
                if (CurrentController != null)
                    return CurrentController.Combatant;
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
                        health += member.Character.Vitality.CurrentStatValue;

                return health;
            }
        }

        public int AverageHealth => Mathf.RoundToInt((float)TotalHealth / activeParty.Count);

        public int TotalStamina
        {
            get
            {
                int stamina = 0;

                foreach (var member in activeParty)
                    if (member != null)
                        stamina += member.Character.Endurance.CurrentStatValue;

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
            if (CurrentController != null)
            {
                currentPos = CurrentController.transform.position;
                transform.position = currentPos;
            }
        }

        // Events can only be invoked from within the class they are located in
        #region InvokeDelegates
        public void InvokeCharChange(Combatant combatant)
        {
            // ?. - shorthand for null check
            OnCharacterChanged?.Invoke(combatant);
        }

        public void InvokeHealthTick()
        {
            OnHealthChange?.Invoke();
        }

        public void InvokeStaminaTick()
        {
            OnStaminaChange?.Invoke();
        }

        public void InvokePoiseTick()
        {
            OnPoiseChange?.Invoke();
        }

        public void InvokeDamage(int damage)
        {
            OnDamage?.Invoke(damage);
        }

        public void InvokeDeath()
        {
            OnDeath?.Invoke();
        }

        public void InvokeAbility(int index)
        {
            OnAbilityUse?.Invoke(index);
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

            currentPartyMember = CurrentController;
            OnCharacterChanged?.Invoke(CurrentCombatant);
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