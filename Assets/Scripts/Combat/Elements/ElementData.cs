using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public bool IsWeak(ElementData attack)
        {
            return weaknesses.Contains(attack.id);
        }

        public bool IsResist(ElementData attack)
        {
            return resistances.Contains(attack.id);
        }

        public bool IsImmune(ElementData attack)
        {
            return immunities.Contains(attack.id);
        }
    }
}
