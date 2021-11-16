using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class SetActiveFence : MonoBehaviour
    {
        [SerializeField]
        private GameObject Fence;
        [SerializeField]
        private GameObject Boss;
        [SerializeField]
        private AudioSource OrdinaryMusic;
        [SerializeField]
        private AudioSource TensionMusic;



        private int Counter { get; set; } = 0;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.Tag.PLAYER)
            {
                Fence.SetActive(true);
                Boss.SetActive(true);
                Counter++;
                OrdinaryMusic.Stop();
                TensionMusic.Play();

            }
        }
        private void Update()
        {
            if (Boss.activeInHierarchy == false && Counter > 0)
            {
                Fence.SetActive(false);
                TensionMusic.Stop();
                if (!OrdinaryMusic.isPlaying)
                    OrdinaryMusic.Play();
            }
        }
    }
}
