using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class RotatingTower : MonoBehaviour
    {
        public Transform Player;

        private void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 _direction = (Player.position - this.transform.position).normalized;
            var _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime* 0.3f
                );

        }
    }
}
