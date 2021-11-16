using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace planTopia
{
    public class Level3Finished : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> Enemies;
       
        
        void Update()
        {

            if (AllFalse())
            {
                this.gameObject.SetActive(false);
            }
        }

        private bool AllFalse()
        {
            
            return Enemies.Where(x => x.activeInHierarchy).Count() == 0;
        }
    }
}
