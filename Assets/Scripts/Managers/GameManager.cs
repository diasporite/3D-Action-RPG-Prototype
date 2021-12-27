using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        CombatManager combat;
        InputManager input;

        public CombatManager Combat => combat;
        public InputManager Input => input;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }
    }
}