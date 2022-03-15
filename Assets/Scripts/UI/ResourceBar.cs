using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class ResourceBar : MonoBehaviour
    {
        bool filling = false;

        [SerializeField] Image background;
        [SerializeField] Image shadow;
        [SerializeField] Image fill;

        [SerializeField] int scale = 100;
        [SerializeField] float shadowSpeed = 0.4f;

        float targetFill;

        Cooldown shadowDelay = new Cooldown(1f);

        private void Update()
        {
            shadow.fillAmount = Mathf.MoveTowards(shadow.fillAmount, fill.fillAmount,
                shadowSpeed * Time.deltaTime);
        }

        public void InitUI(float fraction)
        {
            fill.fillAmount = fraction;
            shadow.fillAmount = fraction;
        }

        public void UpdateUI(float fraction)
        {
            fill.fillAmount = fraction;
        }

        void UpdateShadow()
        {
            shadow.fillAmount = Mathf.MoveTowards(shadow.fillAmount, 
                targetFill, shadowSpeed * Time.deltaTime);

            if (shadow.fillAmount == targetFill) filling = false;
        }
    }
}