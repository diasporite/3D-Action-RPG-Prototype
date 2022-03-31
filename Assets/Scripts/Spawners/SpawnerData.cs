using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class SpawnerData
    {
        [SerializeField] string charName;
        [Range(1, 10)]
        [SerializeField] int weight;

        public int Weight => weight;
        public Controller Controller => GameManager.instance.characters.GetController(charName);
    }
}