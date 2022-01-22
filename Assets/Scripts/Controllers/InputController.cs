using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum InputMode
    {
        None,

        Action,
        Defend,
        Run,
        Walk,
        ToggleShortcut,

        TLAbility,
        TRAbility,
        BLAbility,
        BRAbility,
    }

    public class InputController : MonoBehaviour
    {
        protected PartyManager party;

        protected List<InputMode> inputs = new List<InputMode>();

        protected Vector3 dir1 = new Vector3(0, 0);
        protected Vector3 dir2 = new Vector3(0, 0);

        protected virtual void Awake()
        {
            party = GetComponent<PartyManager>();
        }

        public virtual InputMode GetInput()
        {
            inputs.Clear();

            inputs.Add(InputMode.None);

            return inputs[0];
        }

        // Output dir refers to movement dir (xz plane)
        public virtual Vector3 GetOutputDir1()
        {
            return Vector3.zero;
        }

        public virtual Vector3 GetOutputDir2()
        {
            return Vector3.zero;
        }
    }
}