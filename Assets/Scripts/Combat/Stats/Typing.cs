using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [Serializable]
    public class Typing
    {
        //[SerializeField] TypeID type1 = TypeID.Typeless;
        //[SerializeField] TypeID type2 = TypeID.Typeless;

        //[SerializeField] List<TypeID> additionalResistances = new List<TypeID>();
        //[SerializeField] List<TypeID> additionalWeakness = new List<TypeID>();
        //[SerializeField] List<TypeID> additionalImmunities = new List<TypeID>();

        //[SerializeField] LayerMask walkableTerrain;

        //float weaknessMultiplier;
        //float resistanceMultiplier;

        //TypeDatabase database;

        //public TypeID _type1 => type1;
        //public TypeID _type2 => type2;

        //public LayerMask _walkableTerrain => walkableTerrain;

        //public override string ToString()
        //{
        //    var str1 = "";
        //    var str2 = "";

        //    if (type1 != TypeID.Typeless) str1 = type1.ToString();
        //    if (type2 != TypeID.Typeless) str2 = type2.ToString();

        //    if (str1 == "" && str2 != "") return type2.ToString();
        //    if (str1 != "" && str2 == "") return type1.ToString();

        //    return str1 + "/" + str2;
        //}

        //public Typing(TypeID type1, TypeID type2)
        //{
        //    this.type1 = type1;
        //    this.type2 = type2;

        //    weaknessMultiplier = GameManager.instance._combat._weaknessMultiplier;
        //    resistanceMultiplier = GameManager.instance._combat._resistanceMultiplier;

        //    database = GameManager.instance._typeDatabase;

        //    walkableTerrain = database.GetType(type1)._walkableLayers + database.GetType(type2)._walkableLayers;
        //}

        //public void AddWeakness(TypeID id)
        //{
        //    if (!additionalWeakness.Contains(id)) additionalWeakness.Add(id);
        //}

        //public void AddResistance(TypeID id)
        //{
        //    if (!additionalResistances.Contains(id)) additionalResistances.Add(id);
        //}

        //public void AddImmunity(TypeID id)
        //{
        //    if (!additionalImmunities.Contains(id)) additionalImmunities.Add(id);
        //}

        //public float Effectiveness(TypeID attacking)
        //{
        //    return 1;
        //}

        //public float TypeModifier(TypeID attacking)
        //{
        //    return 1;
        //}
    }
}