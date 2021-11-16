using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class BossesEnabling : MonoBehaviour
    {

        [SerializeField]
        private List<GameObject> Bosses;
        [SerializeField]
        private AudioSource TensionSound;
        public bool Activated = false;
        [SerializeField]
        private AudioSource NormalSound;

        private int Counter = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.PLAYER) && Counter==0)
            {
                SetBossActive();
                NormalSound.Stop();
                Activated = true;
                TensionSound.Play();
                Counter++;
            }
        }

        private void SetBossActive()
        {
            foreach (var item in Bosses)
            {
                item.SetActive(true);
            }
        }
    }
}
