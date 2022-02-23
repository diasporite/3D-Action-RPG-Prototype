using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG_Project
{
    public class AbilityManager : MonoBehaviour
    {
        [Header("Weapons")]
        [SerializeField] Weapon[] weapons;

        [Header("Actions")]
        [SerializeField] CombatAction[] actions;

        [Header("Current Skills")]
        [SerializeField] SkillData[] skills;

        [Header("Abilities")]
        [SerializeField] int currentAbilityIndex = 0;
        [SerializeField] Ability[] abilities;
        Ability currentAbility;

        string[] triggers = new string[4] { "TopLeft", "TopRight", "BottomLeft", "BottomRight" };

        [Header("Special Actions")]
        [SerializeField] Ability jumpAbility = new Ability("SpecialAction");
        [SerializeField] Ability rollAbility = new Ability("SpecialAction");
        [SerializeField] Ability guardAbility = new Ability("SpecialAction");

        Controller controller;
        StateMachine csm;
        Animator anim;
        LockOn lockOn;

        public Ability JumpAbility => jumpAbility;
        public Ability RollAbility => rollAbility;
        public Ability GuardAbility => guardAbility;

        public Ability CurrentAbility
        {
            get => abilities[currentAbilityIndex];
            set => currentAbility = value;
        }

        public Skill CurrentSkillEffect => CurrentAbility.skill;

        private void Awake()
        {
            controller = GetComponent<Controller>();
        }

        //private void Start()
        //{ 
        //    csm = controller.Sm;
        //    anim = controller.Anim;
        //    lockOn = controller.LockOn;

        //    foreach (var weapon in weapons)
        //        weapon.InitWeapon(controller);

        //    ammunition = new PointStat(gunAmmo, gunAmmo, 99);

        //    InitAbilities();
        //}

        public void InitAbilities(LayerMask hittables)
        {
            csm = controller.Sm;
            anim = controller.Anim;
            lockOn = controller.LockOn;

            foreach (var weapon in weapons)
                weapon.InitWeapon(controller, hittables);

            abilities = new Ability[4];

            for(int i = 0; i < 4; i++)
                abilities[i] = skills[i].GetSkill().GetAbility(triggers[i], actions[i]);

            currentAbilityIndex = 0;
        }

        public Ability GetAbility(int index)
        {
            index = Mathf.Abs(index);
            index = index % abilities.Length;

            return abilities[index];
        }

        public void SetAbility(Ability ability)
        {
            CurrentAbility = ability;
        }

        public Ability GetSpecialAction(WeightClass weight)
        {
            switch (weight)
            {
                case WeightClass.Lightweight:
                    return jumpAbility;
                case WeightClass.Middleweight:
                    return rollAbility;
                case WeightClass.Heavyweight:
                    return guardAbility;
                default:
                    return rollAbility;
            }
        }
    }
}