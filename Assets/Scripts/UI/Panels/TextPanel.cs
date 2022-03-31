using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class TextPanel : UIPanel
    {
        Text text;

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);

            text = GetComponentInChildren<Text>();
        }

        public void SetText(string message)
        {
            text.text = message;
        }
    }
}