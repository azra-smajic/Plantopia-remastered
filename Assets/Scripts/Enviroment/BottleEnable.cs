using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace planTopia
{
    public class BottleEnable : MonoBehaviour
    {
        [SerializeField]
        private GameObject Boss;
        [SerializeField]
        private GameObject Bottle;
        private int counter=0;
        [SerializeField]
        private List<GameObject> Bosses;
        [SerializeField]
        private AudioSource VictorySound;
        [SerializeField]
        private AudioSource EndSound;
        [SerializeField]
        private BossesEnabling bossesEnabling;
        private bool IsMainBossActivated = false;
        bool isVisited = false;




        void Update()
        {
           
            if(BosseesDeactivated()&& !Boss.activeInHierarchy && counter==0 && !isVisited && IsMainBossActivated && bossesEnabling.Activated)
            {
                Bottle.SetActive(true);
                EndSound.Stop();
                VictorySound.Play();
                isVisited = true;
            }
            if (Boss.activeInHierarchy)
                IsMainBossActivated = true;
        
        }

       
        private bool BosseesDeactivated()
        {
            return Bosses.Where(x => x.activeInHierarchy == true).ToList().Count == 0;
        }
    }
}
