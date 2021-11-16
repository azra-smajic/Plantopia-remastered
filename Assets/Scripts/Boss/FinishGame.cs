using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace planTopia
{
    public class FinishGame : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> Bosses;
        [SerializeField]
        private GameObject GameObject;
        bool isVisited = false;
        [SerializeField]
        private AudioSource TensionSound;
        [SerializeField]
        private AudioSource StartSound;
        [SerializeField]
        private BossesEnabling bossesEnabling;



        private int counter=0;
        

        void Update()
        {
            if (BosseesDeactivated() && !isVisited && bossesEnabling.Activated)
            {
                GameObject.SetActive(!GameObject.activeInHierarchy);
                TensionSound.Stop();
                StartSound.Play();
                isVisited = true;
            }
       
        }

        private bool BosseesDeactivated()
        {
            return Bosses.Where(x => x.activeInHierarchy == true).ToList().Count == 0;
        }
    }
}
