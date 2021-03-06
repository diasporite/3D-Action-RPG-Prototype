using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class ActionQueue : MonoBehaviour
    {
        [SerializeField] bool executing = false;

        int actionCap = 5;

        [SerializeField] PartyManager party;
        [SerializeField] Controller controller;
        Animator anim;

        [SerializeField] int currentAction = 0;
        [SerializeField] List<BattleCommand> actions = new List<BattleCommand>();

        string initialState;

        public bool Executing => executing;

        public Controller Controller => party.CurrentController;
        public Animator Anim => party.CurrentController.Anim;

        private void Awake()
        {
            //party = GetComponent<PartyManager>();
        }

        public void AddAction(BattleCommand action)
        {
            if (action != null && actions.Count < actionCap)
            {
                actions.Add(action);
                if (!executing) StartAction();
            }
        }

        public void ClearActions()
        {
            actions.Clear();
        }

        public void StartAction()
        {
            executing = true;

            controller = Controller;
            anim = Anim;

            if (currentAction < actions.Count && !controller.Stamina.Empty)
            {
                initialState = controller.Sm.GetCurrentKey.ToString();
                actions[currentAction].Execute();
                controller.Sm.ChangeState(controller.ACTION);
            }
        }

        public void NextAction()
        {
            controller.Movement.UnlockRotation();

            currentAction++;

            controller = Controller;
            anim = Anim;

            if (controller.Stamina.Empty)
            {
                currentAction = 0;

                actions.Clear();
                controller.Sm.ChangeState(controller.RECOVER);

                executing = false;
            }
            else
            {
                if (currentAction < actions.Count)
                {
                    // Start next action
                    actions[currentAction].Execute();
                }
                else
                {
                    currentAction = 0;

                    actions.Clear();
                    controller.Sm.ChangeState(controller.MOVE);

                    executing = false;
                }
            }
        }

        public void StopAction()
        {
            StopCoroutine(ActionChain());

            actions.Clear();

            currentAction = 0;

            executing = false;
        }

        public void StopActionStagger()
        {
            StopCoroutine(ActionChain());

            actions.Clear();

            currentAction = 0;

            executing = false;

            controller = Controller;
            anim = Anim;

            controller.Sm.ChangeState(controller.STAGGER);
        }

        public void StopActionDeath()
        {
            StopCoroutine(ActionChain());

            actions.Clear();

            currentAction = 0;

            executing = false;

            controller = Controller;
            anim = Anim;

            controller.Sm.ChangeState(controller.DEATH);
        }

        IEnumerator ActionChain()
        {
            currentAction = 0;

            executing = true;

            controller.Sm.ChangeState(controller.ACTION);

            while (currentAction < actions.Count)
            {
                if (controller.Stamina.Empty)
                {
                    currentAction = 0;

                    actions.Clear();
                    controller.Sm.ChangeState(controller.RECOVER);

                    executing = false;

                    yield break;
                }

                controller.StartCoroutine(actions[currentAction].ExecuteCo());
                yield return new WaitUntil(() => actions[currentAction].CanProgress);
                currentAction++;
            }

            currentAction = 0;

            actions.Clear();
            controller.Sm.ChangeState(controller.MOVE);

            executing = false;
        }
    }
}