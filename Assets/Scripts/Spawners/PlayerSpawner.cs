using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerSpawner : Spawner
    {
        [SerializeField] PlayerParty partyData;

        [SerializeField] List<Controller> playerParty = new List<Controller>();
        
        private void Start()
        {
            InitSpawns();
        }

        public override void InitSpawns()
        {
            SpawnCharacter();

            GetComponent<LockOn>().InitLockOn();

            GameManager.instance.Ui.InitUI(party);

            party.ChangePartyMember(0);
        }

        public override void SpawnCharacter()
        {
            foreach(var p in playerParty)
            {
                var obj = Instantiate(p.gameObject, transform.position, 
                    Quaternion.identity) as GameObject;
                obj.transform.SetParent(transform);
                var controller = obj.GetComponent<Controller>();
                controller.InitController(true, LayerMask.GetMask("Enemies", "Destructibles"));
                party.AddPartyMember(controller);
            }

            party.InitParty();
        }
    }
}