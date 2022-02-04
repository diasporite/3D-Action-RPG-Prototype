using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] Controller controller;

        [SerializeField] Slider bar;
        [SerializeField] Text damageText;

        int damageReceived = 0;
        Cooldown damageTime = new Cooldown(3f);

        private void Awake()
        {
            //controller = GetComponentInParent<Controller>();

            //bar = GetComponentInChildren<Slider>();
            //damageText = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            bar.gameObject.SetActive(false);
            damageText.gameObject.SetActive(false);
        }

        private void Update()
        {
            damageTime.Tick(Time.deltaTime);
            if (damageTime.Full)
            {
                damageReceived = 0;
                bar.gameObject.SetActive(false);
                damageText.gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            //print("d");
            controller.Party.onDamage -= TakeDamage;
        }

        public void InitUI(Controller controller)
        {
            this.controller = controller;

            controller.Party.onDamage += TakeDamage;
        }

        void TakeDamage(int damage)
        {
            bar.gameObject.SetActive(true);
            damageText.gameObject.SetActive(true);

            bar.value = controller.Party.PartyHealth.ResourceFraction;

            damageReceived += Mathf.Abs(damage);
            damageText.text = "-" + Mathf.Abs(damageReceived);
            damageTime.Reset();
        }
    }
}