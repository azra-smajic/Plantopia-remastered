using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class EnemyBridgeFinished : MonoBehaviour
    {
        [SerializeField]
        private GameObject CollidePlane;
        private void OnTriggerEnter(Collider other)
        {
          
                CollidePlane.SetActive(false);
            
        }
    }
}
