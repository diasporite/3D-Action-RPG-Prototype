using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RPG_Project
{
    //public delegate void OnButtonPress();

    public class UIPanel : MonoBehaviour
    {
        protected bool hidden = false;

        public InputButton button;

        protected Image background;
        [SerializeField] protected CanvasGroup vignette;

        protected Color selected = new Color(0.7f, 0.7f, 0.7f);
        protected Color notSelected = new Color(0.3f, 0.3f, 0.3f);

        public OnButtonPress onButtonPress;

        public virtual void InitPanel()
        {
            background = GetComponent<Image>();
            vignette = GetComponentInChildren<CanvasGroup>();

            onButtonPress += SelectUI;
        }

        public virtual void UpdateUI(bool init)
        {

        }

        public virtual void ShowUI(bool value)
        {
            hidden = value;
        }

        public virtual void SelectUI()
        {
            StopCoroutine(SelectCo());
            StartCoroutine(SelectCo());
        }

        public void HighlightPanel(bool value)
        {
            if (value) background.color = selected;
            else background.color = notSelected;
        }

        IEnumerator SelectCo()
        {
            float t = 0;
            float speed = 4f;

            vignette.alpha = 1;

            while (t < 1)
            {
                t += speed * Time.deltaTime;
                vignette.alpha = 1 - t;
                yield return null;
            }

            vignette.alpha = 0;
        }
    }
}