using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemySpawner : Spawner
    {
        [Range(1, 4)]
        [SerializeField] int partySize = 1;
        [SerializeField] SpawnerData[] data;

        List<Controller> spawns = new List<Controller>();

        private void Start()
        {
            InitSpawns();
        }

        public override void InitSpawns()
        {
            spawns.Clear();

            foreach (var d in data)
                for (int i = 0; i < d.Weight; i++)
                    spawns.Add(d.Controller);
        }

        public override void SpawnCharacter()
        {
            for (int i = 0; i < partySize; i++)
            {
                int j = Random.Range(0, spawns.Count);

                var obj = Instantiate(spawns[j].gameObject, transform.position, 
                    Quaternion.identity) as GameObject;
                party.AddPartyMember(obj.GetComponent<Controller>());
            }
        }
    }
}