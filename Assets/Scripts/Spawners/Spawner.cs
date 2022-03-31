using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Spawner : MonoBehaviour
    {
        protected PartyManager party;

        protected virtual void Awake()
        {
            party = GetComponent<PartyManager>();
        }

        public virtual void InitSpawns()
        {

        }

        public virtual void SpawnCharacter()
        {

        }
    }
}