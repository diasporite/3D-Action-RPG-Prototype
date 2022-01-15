using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerInputController : MonoBehaviour
    {
        PartyManager party;

        private void Awake()
        {
            party = GetComponent<PartyManager>();
        }
    }
}