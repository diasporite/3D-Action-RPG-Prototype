using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class TextPanel : UIPanel
    {
        Text text;

        public override void InitPanel()
        {
            base.InitPanel();
            text = GetComponentInChildren<Text>();
        }

        public void SetText(string message)
        {
            text.text = message;
        }
    }
}