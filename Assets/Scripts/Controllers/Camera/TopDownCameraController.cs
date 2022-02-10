using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class TopDownCameraController : CameraController
    {
        [Range(30f, 90f)]
        public float angleFromHorizontal = 60f;
        [Range(3f, 10f)]
        public float camDist = 8f;
        public float thetaY = 0f;
    }
}