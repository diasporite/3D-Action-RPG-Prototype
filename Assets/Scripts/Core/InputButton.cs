using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class InputButton
    {
        [SerializeField] string key;
        [SerializeField] bool hold;

        [SerializeField] bool directional;
        Vector3 dir = new Vector3(0, 0);

        public string _key => key;

        public bool ItemInput
        {
            get
            {
                if (hold) return Input.GetKey(key);
                return Input.GetKeyDown(key);
            }
        }

        public Vector3 Dir
        {
            get
            {
                dir.x = Input.GetAxisRaw("Horizontal");
                dir.y = Input.GetAxisRaw("Vertical");
                return dir;
            }
        }
    }
}