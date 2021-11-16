using planTopia.Controllers.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    public class SwordAttack : MonoBehaviour
    {
        [SerializeField]
        [Range(0.1f,50)]
        private float Damage=10;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.PLAYER))
            {
                other.GetComponent<PlayerHealth>().DecreaseHealth(Damage);
            }
        }
    }
}
