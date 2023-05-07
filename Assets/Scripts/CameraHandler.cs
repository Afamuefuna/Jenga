using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenga.Script
{

    public class CameraHandler : MonoBehaviour
    {
        OrbitCamera orbitCamera;

        private void Start()
        {
            orbitCamera = GetComponent<OrbitCamera>();
        }
        public void SwitchTarget(Transform newTarget)
        {
            orbitCamera.target = newTarget;
            orbitCamera.distance = 5.5f;
        }
    }
}

