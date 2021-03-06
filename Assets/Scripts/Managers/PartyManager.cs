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
        public event Action<int> OnCharacterSelect;

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
        public event Action<int> OnAbilitySelect;

        //public event OnHealthChanged OnHealthChanged;
        //public event OnStaminaChanged OnStaminaChanged;
        //public event OnPoiseChanged OnPoiseChanged;

        protected int partyCap = 4;
        [SerializeField] int currentMember = 0;
        [SerializeField] List<Controller> party = new List<Controller>();

        [SerializeField] Vector3 currentPos = new Vector3(0, 0, 0);
        [SerializeField] Controller currentPartyMember;

        Health partyHealth;
        Stamina partyStamina;
        ActionQueue actionQueue;
        ShortcutRegister shortcuts;
        LockOn lockOn;
        InputController inputController;
        AIController ai;

        CharacterHealth charStats;

        public List<Controller> Party => party;

        public Controller CurrentController
        {
            get
            {
                if (currentMember < party.Count)
                    return party[currentMember];
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

                foreach (var member in party)
                    if (member != null)
                        health += member.Character.Vitality.CurrentStatValue;

                return health;
            }
        }

        public int AverageHealth => Mathf.RoundToInt((float)TotalHealth / party.Count);

        public int PartyHp => Mathf.RoundToInt((0.428571f * (party.Count - 1) + 1) * AverageHealth);

        public int TotalStamina
        {
            get
            {
                int stamina = 0;

                foreach (var member in party)
                    if (member != null)
                        stamina += member.Character.Endurance.CurrentStatValue;

                return stamina;
            }
        }

        public int AverageStamina => Mathf.RoundToInt((float)TotalStamina / party.Count);

        public int PartySp => Mathf.RoundToInt((0.142857f * (party.Count - 1) + 1) * AverageStamina);

        public Controller GetPartyMember(int index)
        {
            index = Mathf.Abs(index);

            if (index < party.Count) return party[index];

            return null;
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

            charStats = GetComponentInChildren<CharacterHealth>();
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

        public void InvokeCharSelect(int index)
        {
            OnAbilitySelect?.Invoke(index);
        }

        public void InvokeHealthChange()
        {
            OnHealthChange?.Invoke();
        }

        public void InvokeStaminaChange()
        {
            OnStaminaChange?.Invoke();
        }

        public void InvokePoiseChange()
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

        public void InvokeAbilityUse(int index)
        {
            OnAbilityUse?.Invoke(index);
        }

        public void InvokeAbilitySelect(int index)
        {
            OnAbilitySelect?.Invoke(index);
        }
        #endregion

        public void InitParty()
        {
            currentPartyMember = CurrentController;

            partyHealth.InitResource();
            partyStamina.InitResource();

            charStats.InitUI(this);
        }

        public void AddPartyMember(Controller member)
        {
            if (party.Count < partyCap) party.Add(member);
        }

        public void RemovePartyMember(Controller member)
        {
            if (party.Contains(member)) party.Remove(member);
        }

        public void ChangePartyMember(int index)
        {
            if (party.Count <= 0) return;
            //if (currentMember == index) return;

            index = Mathf.Abs(index);

            //CurrentPartyMember.gameObject.SetActive(false);

            if (index >= party.Count) return;

            currentMember = index;
            //CurrentPartyMember.gameObject.SetActive(true);

            for(int i = 0; i < party.Count; i++)
            {
                if (i == currentMember)
                {
                    party[i].gameObject.SetActive(true);
                    party[i].transform.position = currentPos;
                }
                else party[i].gameObject.SetActive(false);
            }

            currentPartyMember = CurrentController;
            OnCharacterChanged?.Invoke(CurrentCombatant);
        }

        public void ChangePartyComp()
        {

        }
    }
}