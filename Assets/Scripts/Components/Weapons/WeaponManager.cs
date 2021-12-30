using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class WeaponManager : MonoBehaviour
    {
        bool canDisarm = true;

        [Header("Weapons")]
        [SerializeField] Weapon leftWeapon;
        [SerializeField] Weapon rightWeapon;

        [Header("Parameters")]
        [SerializeField] int raycastAmmo = 10;

        [SerializeField] Weapon currentWeapon;

        Controller controller;
        Animator anim;

        public bool CanDisarm
        {
            get => canDisarm;
            set => canDisarm = value;
        }

        private void Start()
        {
            controller = GetComponent<Controller>();
            anim = controller.Anim;

            leftWeapon.InitWeapon(controller);
            rightWeapon.InitWeapon(controller);

            SwitchWeapon(WeaponHand.Empty);
        }

        public void SwitchWeapon(WeaponHand hand)
        {
            switch (hand)
            {
                case WeaponHand.Left:
                    currentWeapon = leftWeapon;
                    GameManager.instance.battleUi.ChangeButtonMenu(SkillMenuState.LeftWeaponSkills);
                    anim.SetBool("LeftWeapon", true);
                    break;
                case WeaponHand.Right:
                    currentWeapon = rightWeapon;
                    GameManager.instance.battleUi.ChangeButtonMenu(SkillMenuState.RightWeaponSkills);
                    anim.SetBool("RightWeapon", true);
                    break;
                default:
                    if (canDisarm)
                    {
                        currentWeapon = null;
                        GameManager.instance.battleUi.ChangeButtonMenu(SkillMenuState.BasicSkills);
                        anim.SetBool("LeftWeapon", false);
                        anim.SetBool("RightWeapon", false);
                    }
                    break;
            }
        }

        public void ActivateWeapon()
        {
            if (currentWeapon != null) currentWeapon.ActivateWeapon();
        }

        public void DeactivateWeapon()
        {
            if (currentWeapon != null) currentWeapon.DeactivateWeapon();
        }
    }
}