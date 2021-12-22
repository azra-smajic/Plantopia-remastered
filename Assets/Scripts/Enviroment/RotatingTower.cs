using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class RotatingTower : MonoBehaviour
    {
        public Transform Player;
        [SerializeField]
        private Fireball Fireball;
     

        private void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 _direction = (Player.position - this.transform.position);
            
            var _lookRotation = Quaternion.LookRotation(_direction);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, _direction, 0.5f, 0.0f);
            //rotate us over time according to speed until we are in the required rotation
            this.transform.rotation = Quaternion.LookRotation(newDirection);
            Fireball.Push(_direction, 900);

        }
    }
}
