using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    [System.Serializable]
    public class BattleAction : ICommand
    {
        [SerializeField] protected string actionName = "";

        [SerializeField] protected Controller controller;
        [SerializeField] protected Vector3 dir;

        protected Controller[] targets;

        protected float duration = 1;

        protected bool canProgress = false;
        protected bool complete = false;

        public bool CanProgress => canProgress;
        public bool Complete => complete;

        public BattleAction(Controller controller)
        {
            this.controller = controller;
            dir = controller.transform.forward;

            canProgress = false;
            complete = false;

            InitAction();
        }

        public BattleAction(Controller controller, Vector3 dir)
        {
            this.controller = controller;
            this.dir = dir;

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