using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] float uiOffset = 1.5f;
        [SerializeField] float updateSpeed = 10f;

        [SerializeField] PartyManager party;

        [SerializeField] GameObject uiHolder;

        [SerializeField] Text skillName;
        [SerializeField] Slider health;
        [SerializeField] Slider stamina;
        [SerializeField] Text damageText;

        int damageReceived = 0;
        Cooldown damageTime = new Cooldown(3f);

        Vector3 lastPosition = new Vector3(0, 0);
        Vector3 newPosition;

        private void Awake()
        {
            //controller = GetComponentInParent<Controller>();

            //bar = GetComponentInChildren<Slider>();
            //damageText = GetComponentInChildren<Text>();
        }

        private void Start()
        {
            ShowUI(false);
        }

        private void Update()
        {
            damageTime.Tick(Time.deltaTime);

            health.value = party.PartyHealth.ResourceFraction;
            stamina.value = party.PartyStamina.ResourceFraction;

            if (damageTime.Full)
            {
                damageReceived = 0;
                ShowUI(false);
            }
        }

        private void LateUpdate()
        {
            UpdatePosition();
        }

        private void OnDisable()
        {
            //print("d");
            party.OnDamage -= TakeDamage;
        }

        public void InitUI(PartyManager party)
        {
            this.party = party;

            this.party.OnDamage += TakeDamage;
        }

        void ShowUI(bool value)
        {
            if (!value) skillName.text = "";

            skillName.gameObject.SetActive(value);
            health.gameObject.SetActive(value);
            stamina.gameObject.SetActive(value);
            damageText.gameObject.SetActive(value);
        }

        void TakeDamage(int damage)
        {
            lastPosition =
                Camera.main.WorldToScreenPoint(party.transform.position +
                uiOffset * party.transform.up);
            uiHolder.transform.position = lastPosition;

            ShowUI(true);

            health.value = party.PartyHealth.ResourceFraction;
            stamina.value = party.PartyStamina.ResourceFraction;

            damageReceived += Mathf.Abs(damage);
            damageText.text = "-" + Mathf.Abs(damageReceived);
            damageTime.Reset();
        }

        void UpdatePosition()
        {
            // Use update speed / MoveTowards() to reduce jitter (see camera follow)
            newPosition =
                Camera.main.WorldToScreenPoint(party.transform.position +
                uiOffset * Vector3.up);

            uiHolder.transform.position = Vector3.MoveTowards(lastPosition, newPosition, updateSpeed * Time.deltaTime);
            //uiHolder.transform.position = newPosition;
            lastPosition = uiHolder.transform.position;
        }
    }
}