using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    [System.Serializable]
    public class BattleCommand : ICommand
    {
        [SerializeField] protected string actionName = "";

        [SerializeField] protected Controller controller;
        [SerializeField] protected Vector3 dir;

        protected Ability ability;

        protected Controller[] targets;
        protected Target target;

        protected Animator anim;

        protected CombatDatabase combat;

        protected bool canProgress = false;
        protected bool complete = false;

        public bool CanProgress => canProgress;
        public bool Complete => complete;

        public BattleCommand(Controller controller)
        {
            this.controller = controller;
            dir = controller.transform.forward;

            anim = controller.Anim;

            combat = GameManager.instance.Combat;

            canProgress = false;
            complete = false;

            InitAction();
        }

        public BattleCommand(Controller controller, Vector3 dir)
        {
            this.controller = controller;
            this.dir = dir;

            combat = GameManager.instance.Combat;

            canProgress = false;
            complete = false;

            InitAction();
        }

        public virtual void Execute()
        {
            canProgress = true;
            complete = true;
        }

        public virtual IEnumerator ExecuteCo()
        {
            yield return null;

            canProgress = true;
            complete = true;
        }

        protected void InitAction()
        {

        }
    }
}