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

        [SerializeField] List<Controller> spawns = new List<Controller>();

        private void Start()
        {
            InitSpawns();

            SpawnCharacter();
        }

        public override void InitSpawns()
        {
            spawns.Clear();

            foreach (var d in data)
                for (int i = 0; i < d.Weight; i++)
                    if (d.Controller != null)
                        spawns.Add(d.Controller);
        }

        public override void SpawnCharacter()
        {
            for (int i = 0; i < partySize; i++)
            {
                int j = Random.Range(0, spawns.Count);

                var obj = Instantiate(spawns[j].gameObject, transform.position, 
                    Quaternion.identity) as GameObject;
                var controller = obj.GetComponent<Controller>();
                obj.transform.SetParent(transform);
                controller.InitController(false, LayerMask.GetMask("Player", "Destructibles"));
                party.AddPartyMember(controller);
            }

            party.InitParty();
        }
    }
}