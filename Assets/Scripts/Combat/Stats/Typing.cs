using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [Serializable]
    public class Typing
    {
        [SerializeField] ElementID element1 = ElementID.Typeless;
        [SerializeField] ElementID element2 = ElementID.Typeless;

        [SerializeField] List<ElementID> additionalResistances = new List<ElementID>();
        [SerializeField] List<ElementID> additionalWeakness = new List<ElementID>();
        [SerializeField] List<ElementID> additionalImmunities = new List<ElementID>();

        [SerializeField] LayerMask walkableTerrain;

        float weaknessMultiplier;
        float resistanceMultiplier;

        //TypeDatabase database;

        public ElementID Element1 => element1;
        public ElementID Element2 => element2;

        public LayerMask _walkableTerrain => walkableTerrain;

        public override string ToString()
        {
            var str1 = "";
            var str2 = "";

            if (element1 != ElementID.Typeless) str1 = element1.ToString();
            if (element2 != ElementID.Typeless) str2 = element2.ToString();

            if (str1 == "" && str2 != "") return element2.ToString();
            if (str1 != "" && str2 == "") return element1.ToString();

            return str1 + "/" + str2;
        }

        public void AddWeakness(ElementID id)
        {
            if (!additionalWeakness.Contains(id)) additionalWeakness.Add(id);
        }

        public void AddResistance(ElementID id)
        {
            if (!additionalResistances.Contains(id)) additionalResistances.Add(id);
        }

        public void AddImmunity(ElementID id)
        {
            if (!additionalImmunities.Contains(id)) additionalImmunities.Add(id);
        }

        public float Effectiveness(ElementID attacking)
        {
            return 1f;
        }

        public float TypeModifier(ElementID attacking)
        {
            return 1f;
        }
    }
}