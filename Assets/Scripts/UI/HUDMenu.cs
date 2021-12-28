using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class HUDMenu : MonoBehaviour
    {
        [SerializeField] UIPanel[] panels = new UIPanel[4];

        private void Awake()
        {
            panels = GetComponentsInChildren<UIPanel>();
            foreach (var p in panels) p.InitPanel();
        }

        private void Update()
        {
            foreach (var p in panels)
                if (p.button.GetInput)
                    p.onButtonPress.Invoke();
        }
    }
}