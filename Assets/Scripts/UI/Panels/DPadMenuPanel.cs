using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class DPadMenuPanel : UIPanel
    {
        Text text;

        int currentText = 0;
        string[] panelText = new string[] { "Items", "1st Party" };

        public override void InitUI(PartyManager party)
        {
            base.InitUI(party);

            text = GetComponentInChildren<Text>();
        }

        void ChangeText()
        {
            currentText++;
            currentText = currentText % panelText.Length;
            text.text = panelText[currentText];
        }
    }
}