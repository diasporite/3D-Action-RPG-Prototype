using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class PartyMemberData
    {
        public string memberName;
        public string exp;
    }

    [CreateAssetMenu(fileName = "PlayerParty", menuName = "Combat/Database/Player Party")]
    public class PlayerParty : ScriptableObject
    {
        [SerializeField] PartyMemberData[] currentParty;

        [SerializeField] PartyMemberData[] reservedParty;
    }
}