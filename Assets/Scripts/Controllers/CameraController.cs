﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    // Resource: https://learn.unity.com/tutorial/controlling-unity-camera-behaviour
    public class CameraController : MonoBehaviour
    {
        public readonly string UNLOCKED = "unlocked";
        public readonly string LOCKED = "locked";

        public Controller player;
        public Transform target;

        [Header("Original Simple Solution")]
        public float speed = 120;
        [Range(3, 10)]
        public float camDist = 10;
        public float thetaXz = 10;
        public float thetaY = 180;
        public Vector3 defaultCamOffset = new Vector3(0, 2, -8);
        Vector3 camPos;
        public Vector3 targetPlayerOffset = new Vector3(5, 0, 5);

        [Header("Unity Tutorial")]
        public Transform follow;
        public float maxDistance = 10;
        public float moveSpeed = 20;
        public float updateSpeed = 10;
        [Range(0, 10)]
        public float currentDistance = 5;
        public float hideDistance = 1.5f;

        string moveAxis = "Mouse ScrollWheel";
        GameObject ahead;
        MeshRenderer mr;

        StateMachine sm = new StateMachine();

        public Transform Target => target;

        public StateMachine Sm => sm;

        private void Start()
        {
            ahead = new GameObject("Ahead");
            mr = follow.GetComponent<MeshRenderer>();

            camPos = follow.transform.position + camDist * defaultCamOffset.normalized;

            sm.AddState(UNLOCKED, new CameraUnlockedState(this));

            sm.ChangeState(UNLOCKED);
        }

        private void Update()
        {
            sm.Update();

            //RotateCamera();
        }

        private void LateUpdate()
        {
            sm.UpdateLate();

            //transform.position = Vector3.MoveTowards(transform.position, 
            //    follow.position + offset, speed * Time.deltaTime);

            //Follow3D();
            //FollowPlayer();
        }

        public void Follow3D()
        {
            ahead.transform.position = follow.position + follow.forward * maxDistance * 0.25f;
            currentDistance += Input.GetAxisRaw(moveAxis) * moveSpeed * Time.deltaTime;
            currentDistance = Mathf.Clamp(currentDistance, 0, maxDistance);
            transform.position = Vector3.MoveTowards(transform.position,
                follow.position + Vector3.up * currentDistance -
                follow.forward * (currentDistance + maxDistance * 0.5f),
                updateSpeed * Time.deltaTime);
            transform.LookAt(ahead.transform);
            mr.enabled = currentDistance > hideDistance;
        }

        public void RotateCamera()
        {
            if (Input.GetKey("left")) thetaY += speed * Time.deltaTime;
            if (Input.GetKey("right")) thetaY -= speed * Time.deltaTime;

            if (Input.GetKey("up")) thetaXz += speed * Time.deltaTime;
            if (Input.GetKey("down")) thetaXz -= speed * Time.deltaTime;

            if (Mathf.Abs(thetaXz) > 30) thetaXz = 30 * Mathf.Sign(thetaXz);
        }

        public void FollowPlayer()
        {
            if (follow != null)
            {
                camPos.x = follow.position.x + camDist * Mathf.Sin(thetaY * Mathf.Deg2Rad);
                camPos.y = follow.position.y + camDist * Mathf.Sin(thetaXz * Mathf.Deg2Rad);
                camPos.z = follow.position.z + camDist * Mathf.Cos(thetaY * Mathf.Deg2Rad);

                transform.position = camPos;
                transform.LookAt(follow);
            }
        }

        public void FollowTarget()
        {
            if (follow != null && target != null)
            {
                camPos.x = follow.position.x + camDist * Mathf.Sin(thetaY * Mathf.Deg2Rad);
                camPos.y = follow.position.y + camDist * Mathf.Sin(thetaXz * Mathf.Deg2Rad);
                camPos.z = follow.position.z + camDist * Mathf.Cos(thetaY * Mathf.Deg2Rad);

                //camPos += targetPlayerOffset;
                var look = Vector3.Lerp(follow.position, target.position, 0.5f);

                transform.position = camPos;
                transform.LookAt(look);
            }
        }
    }
}