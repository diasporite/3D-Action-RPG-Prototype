using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Cooldown
    {
        [SerializeField] float cooldown = 1;
        [SerializeField] float speed = 1;
        [SerializeField] float count = 0;

        public float _cooldown
        {
            get => cooldown;
            set => cooldown = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public float Count
        {
            get => count;
            set
            {
                count = value;
                if (count > cooldown) count = cooldown;
                if (count < 0) count = 0;
            }
        }

        public float CooldownFraction
        {
            get
            {
                if (cooldown > 0) return count / cooldown;
                return 0;
            }
            set => Count = value * (float)cooldown;
        }

        public bool Empty => count <= 0;

        public bool Full => count >= cooldown;

        public Cooldown(float cooldown, float speed)
        {
            this.cooldown = cooldown;
            this.speed = speed;
        }

        public void Tick(float dt)
        {
            count += speed * dt;
            if (count < 0) count = 0;
            if (count > cooldown) count = cooldown;
        }

        public void Reset()
        {
            count = 0;
        }
    }
}