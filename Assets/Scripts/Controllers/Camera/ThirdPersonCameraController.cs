using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    // Resource: https://learn.unity.com/tutorial/controlling-unity-camera-behaviour
    public class ThirdPersonCameraController : MonoBehaviour
    {
        public readonly string UNLOCKED = "unlocked";
        public readonly string LOCKED = "locked";

        public string currentState;

        public Transform follow;
        public Transform target;

        [Header("Original Solution")]
        public Image reticle;

        public float speed = 120;
        [Range(2f, 6f)]
        public float camDist = 4f;
        [Range(0f, 5f)]
        public float camHeight = 2.5f;

        public Vector3 defaultCamOffset = new Vector3(0, 2, -8);
        Vector3 targetPos;
        public Vector3 targetPlayerOffset = new Vector3(5, 0, 5);

        public float updateSpeed = 50f;

        public Vector3 lastViewedPosition;

        public float thetaXz = 10;
        public float thetaY = 180;
        float xzMax = 30;
        float xzMin = -30;

        public Target LockedTarget { get; set; }
        public LockOn lockOn { get; set; }

        PlayerLock playerLock;

        [Header("Unity Tutorial")]
        //public Transform follow;
        //public float maxDistance = 10;
        //public float moveSpeed = 20;
        //public float updateSpeed = 10;
        //[Range(0, 10)]
        //public float currentDistance = 5;
        //public float hideDistance = 1.5f;

        //string moveAxis = "Mouse ScrollWheel";
        //GameObject ahead;
        //MeshRenderer mr;

        StateMachine sm = new StateMachine();

        public Transform Target => target;

        public StateMachine Sm => sm;

        private void Start()
        {
            //ahead = new GameObject("Ahead");
            //mr = follow.GetComponent<MeshRenderer>();

            targetPos = follow.transform.position + camDist * defaultCamOffset.normalized;
            lastViewedPosition = targetPos;

            lockOn = FindObjectOfType<PlayerLock>();

            sm.AddState(UNLOCKED, new CameraUnlockedState(this));
            sm.AddState(LOCKED, new CameraLockedState(this));

            //sm.ChangeState(LOCKED);
            sm.ChangeState(UNLOCKED);
        }

        private void Update()
        {
            sm.Update();

            currentState = (string)sm.GetCurrentKey;
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
            //ahead.transform.position = follow.position + follow.forward * maxDistance * 0.25f;
            //currentDistance += Input.GetAxisRaw(moveAxis) * moveSpeed * Time.deltaTime;
            //currentDistance = Mathf.Clamp(currentDistance, 0, maxDistance);
            //transform.position = Vector3.MoveTowards(transform.position,
            //    follow.position + Vector3.up * currentDistance -
            //    follow.forward * (currentDistance + maxDistance * 0.5f),
            //    updateSpeed * Time.deltaTime);
            //transform.LookAt(ahead.transform);
            //mr.enabled = currentDistance > hideDistance;
        }

        public void RotateCamera()
        {
            if (Input.GetKey("left")) thetaY += speed * Time.deltaTime;
            if (Input.GetKey("right")) thetaY -= speed * Time.deltaTime;

            if (Input.GetKey("up")) thetaXz += speed * Time.deltaTime;
            if (Input.GetKey("down")) thetaXz -= speed * Time.deltaTime;

            if (thetaXz > xzMax) thetaXz = xzMax;
            if (thetaXz < xzMin) thetaXz = xzMin;
        }

        public void FollowPlayer()
        {
            if (follow != null)
            {
                targetPos.x = follow.position.x - camDist * Mathf.Sin(thetaY * Mathf.Deg2Rad);
                targetPos.y = follow.position.y + camDist * Mathf.Sin(thetaXz * Mathf.Deg2Rad) + camHeight;
                targetPos.z = follow.position.z - camDist * Mathf.Cos(thetaY * Mathf.Deg2Rad);

                //transform.position = targetPos;
                //transform.LookAt(follow);

                SmoothFollow();
            }
        }

        public void FollowTarget()
        {
            if (follow != null && target != null)
            {
                var ds = target.position - follow.position;
                var newPos = follow.position - 5f * ds.normalized;

                newPos.y = follow.position.y + 2f;
                newPos += 2f * follow.transform.right;

                //transform.position = Vector3.MoveTowards(transform.position, newPos, 
                //    updateSpeed * Time.deltaTime);

                //transform.LookAt(0.5f * (follow.position + target.position));

                SmoothFollow(newPos, 0.5f * (follow.position + target.position));
            }
        }

        protected void SmoothFollow()
        {
            var look = Vector3.MoveTowards(lastViewedPosition, targetPos, 
                updateSpeed * Time.deltaTime);
            transform.position = look;
            lastViewedPosition = look;
            transform.LookAt(follow);
        }

        protected void SmoothFollow(Vector3 targetPos, Vector3 lookAt)
        {
            var look = Vector3.MoveTowards(lastViewedPosition, targetPos,
                updateSpeed * Time.deltaTime);
            transform.position = look;
            lastViewedPosition = look;
            transform.LookAt(follow);
        }

        public void ReticleFollow()
        {
            var target = lockOn.CurrentTarget;

            if (target != null)
                reticle.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
        }
    }
}