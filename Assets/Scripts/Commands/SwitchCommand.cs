using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class SwitchCommand : BattleCommand
    {
        PartyManager party;
        Controller newController;
        int index;

        public SwitchCommand(Controller controller, int index) : base(controller)
        {
            actionName = "switch";

            this.index = index;
            party = controller.Party;
        }

        public override void Execute()
        {
            party.StartCoroutine(ExecuteCo());
        }

        public override IEnumerator ExecuteCo()
        {
            party.ChangePartyMember(index);

            yield return new WaitForSeconds(0.2f);

            party.ActionQueue.NextAction();
        }
    }
}