using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class FireTarget : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;

        Ray Ray;
        RaycastHit HitInfo;

        private void Update()
        {
            Ray.origin = mainCamera.transform.position;
            Ray.direction = mainCamera.transform.forward;
            Physics.Raycast(Ray, out HitInfo);
            transform.position = HitInfo.point;
        }
    }
}
