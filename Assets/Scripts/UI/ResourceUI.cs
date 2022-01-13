using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class ResourceUI : MonoBehaviour
    {
        public StatType stat;
        public string textHeader;

        [SerializeField] PartyManager player;
        [SerializeField] Resource resource;

        [SerializeField] Slider bar;
        [SerializeField] Text text;

        public void InitUI(PartyManager player)
        {
            this.player = player;

            bar = GetComponentInChildren<Slider>();
            text = GetComponentInChildren<Text>();
        }

        public void UpdateUI()
        {
            bar.value = resource.ResourceFraction;
            text.text = textHeader.Trim() + " " + 
                resource.ResourcePointValue + "/" + resource.ResourceStatValue;
        }

        public void UpdateCharacter(Resource resource)
        {
            this.resource = resource;

            UpdateUI();
        }
    }
}