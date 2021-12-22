using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class OpenPortal : MonoBehaviour
    {
        [SerializeField]
        private GameObject Boss;
        [SerializeField]
        private ParticleSystem Portal;
    

        // Update is called once per frame
        void Update()
        {
            if (!Boss.activeInHierarchy)
                Portal.Play();
        
        }
    }
}
