using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementAdvanced : MonoBehaviour
    {
        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            
        }
    }
}