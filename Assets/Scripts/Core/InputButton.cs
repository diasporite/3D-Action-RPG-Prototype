using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class InputButton
    {
        public event OnButtonPress onButtonPress;

        [SerializeField] string key;
        [SerializeField] bool hold;

        [SerializeField] bool directional;
        Vector3 dir = new Vector3(0, 0);

        public string _key => key;

        public bool GetInput
        {
            get
            {
                if (hold) return Input.GetKey(key);
                return Input.GetKeyDown(key);
            }
        }

        public Vector3 DirXy
        {
            get
            {
                dir.x = Input.GetAxisRaw("Horizontal");
                dir.y = Input.GetAxisRaw("Vertical");
                return dir;
            }
        }

        public Vector3 DirXz
        {
            get
            {
                dir.x = Input.GetAxisRaw("Horizontal");
                dir.z = Input.GetAxisRaw("Vertical");
                return dir;
            }
        }

        public InputButton(string key, bool hold, bool directional)
        {
            this.key = key;
            this.hold = hold;
            this.directional = directional;
        }

        public void Invoke()
        {
            if (GetInput)
                onButtonPress.Invoke();
        }
    }
}