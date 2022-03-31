using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class KillPlane : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var hitObj = other.gameObject;

            if (hitObj.tag == "Player") hitObj.transform.position = Vector3.zero;
        }
    }
}