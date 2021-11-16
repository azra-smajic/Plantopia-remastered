using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace planTopia.Enviroment
{
    public class Messages : MonoBehaviour
    {
        [SerializeField]
        private GameObject Message;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag==Constants.Tag.PLAYER)
                Message.SetActive (true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == Constants.Tag.PLAYER)
                Message.SetActive(false);
        }
    }
}
