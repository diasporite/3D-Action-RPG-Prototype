using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Element", menuName = "Combat/Element")]
    public class ElementData : ScriptableObject
    {
        public ElementID id;
        public Sprite emblem;

        public ElementID[] weaknesses;
        public ElementID[] resistances;
        public ElementID[] immunities;
    }
}
