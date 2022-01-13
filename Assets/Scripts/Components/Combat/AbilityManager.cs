using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum AbilityOrientation
    {
        Special = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 3,
        BottomRight = 4,
    }

    public class AbilityManager : MonoBehaviour
    {
        bool canDisarm = true;

        [Header("Weapons")]
        [SerializeField] Weapon[] weapons;

        [Header("Abilities")]
        [SerializeField] Ability topLeftAbility;
        [SerializeField] Ability topRightAbility;
        [SerializeField] Ability bottomLeftAbility;
        [SerializeField] Ability bottomRightAbility;

        [Header("Special Actions")]
        [SerializeField] Ability jumpAbility = new Ability("SpecialAction");
        [SerializeField] Ability rollAbility = new Ability("SpecialAction");
        [SerializeField] Ability guardAbility = new Ability("SpecialAction");

        [Header("Resources")]
        [SerializeField] int gunAmmo = 10;

        [SerializeField] Ability currentAbility;

        Dictionary<AbilityOrientation, Ability> abilityDict = new Dictionary<AbilityOrientation, Ability>();

        Controller controller;
        StateMachine csm;
        Animator anim;

        public bool CanDisarm
        {
            get => canDisarm;
            set => canDisarm = value;
        }

        public Ability TopLeftAbility => topLeftAbility;
        public Ability TopRightAbility => topRightAbility;
        public Ability BottomLeftAbility => bottomLeftAbility;
        public Ability BottomRightAbility => bottomRightAbility;

        public Ability JumpAbility => jumpAbility;
        public Ability RollAbility => rollAbility;
        public Ability GuardAbility => guardAbility;

        public Ability CurrentAbility => currentAbility;
        public Weapon CurrentWeapon => currentAbility.action.weapon;

        public Ability GetAbility(AbilityOrientation ability)
        {
            return abilityDict[ability];
        }

        private void Awake()
        {
            controller = GetComponent<Controller>();
        }

        private void Start()
        { 
            csm = controller.Sm;
            anim = controller.Anim;

            foreach (var weapon in weapons)
                weapon.InitWeapon(controller);

            InitAbilities();
        }

        public void InitAbilities()
        {
            topLeftAbility.InitAbility("TopLeft");
            topRightAbility.InitAbility("TopRight");
            bottomLeftAbility.InitAbility("BottomLeft");
            bottomRightAbility.InitAbility("BottomRight");

            abilityDict.Clear();

            abilityDict.Add(AbilityOrientation.TopLeft, topLeftAbility);
            abilityDict.Add(AbilityOrientation.TopRight, topRightAbility);
            abilityDict.Add(AbilityOrientation.BottomLeft, bottomLeftAbility);
            abilityDict.Add(AbilityOrientation.BottomRight, bottomRightAbility);

            currentAbility = bottomRightAbility;
        }

        public void ActivateWeapon()
        {
            currentAbility.action.weapon.ActivateWeapon();
        }

        public void DeactivateWeapon()
        {
            currentAbility.action.weapon.DeactivateWeapon();
        }

        public Ability GetAbility(int index)
        {
            index = Mathf.Abs(index);
            index = index % 4;

            switch (index)
            {
                case 0: return topLeftAbility;
                case 1: return topRightAbility;
                case 2: return bottomLeftAbility;
                case 3: return bottomRightAbility;
                default: return null;
            }
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