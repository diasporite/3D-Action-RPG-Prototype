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

        public override void InitPanel()
        {
            base.InitPanel();
            onButtonPress += ChangeText;

            text = GetComponentInChildren<Text>();
        }

        private void Update()
        {
            if (button.ItemInput) onButtonPress.Invoke();
        }

        void ChangeText()
        {
            currentText++;
            currentText = currentText % panelText.Length;
            text.text = panelText[currentText];
        }
    }
}