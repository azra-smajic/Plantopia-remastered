using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Enemies
{
    public class HealthBarBehaviour : MonoBehaviour
    {
        private Camera MainCamera { get; set; }

        private void Start()
        {
            MainCamera = Camera.main;
        }

        private void Update()
        {
            var CameralookAt = MainCamera.transform.position - this.transform.position;

            CameralookAt.z = 0;

            this.transform.LookAt(MainCamera.transform.position - CameralookAt);
            this.transform.rotation = MainCamera.transform.rotation;
        }
    }
}
