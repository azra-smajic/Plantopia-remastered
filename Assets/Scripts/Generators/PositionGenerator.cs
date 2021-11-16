using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class PositionGenerator : MonoBehaviour
    {
        [SerializeField]
        private float percentageOfUsage;
        [SerializeField]
        private GameObject Plane;

        public Vector3 origin { get; set; }
        public Vector3 range { get; set; }
       
        void Start()
        {
            SetPosition(Plane);
           
        }
        public (Vector3,Vector3) SetPosition(GameObject Position)
        {
            origin = Position.transform.position;
            range = Position.transform.localScale / 2.0f;
            return (origin, range);
        }

        
    }
}
