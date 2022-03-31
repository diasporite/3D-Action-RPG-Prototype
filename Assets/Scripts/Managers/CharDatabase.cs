using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Combat/Database/Character")]
    public class CharDatabase : ScriptableObject
    {
        [SerializeField] CharData[] characters;
        Dictionary<string, CharData> charDict = new Dictionary<string, CharData>();

        public Controller GetController(string name)
        {
            if (charDict.ContainsKey(name))
                return charDict[name].controller;

            Debug.LogError("No character called " + name + " exists.");
            return null;
        }

        public void InitDatabase()
        {
            charDict.Clear();

            foreach (var c in characters)
                if (!charDict.ContainsValue(c))
                    charDict.Add(c.charName, c);
        }
    }
}