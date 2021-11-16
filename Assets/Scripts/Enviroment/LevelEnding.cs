using planTopia.Controllers.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Enviroment
{
    public class LevelEnding : MonoBehaviour
    {
        [SerializeField]
        private Transform NextLevelPosition;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == Constants.Tag.PLAYER)
                SwitchOnNextLevel(collision.gameObject);
        }
        private void SwitchOnNextLevel(GameObject Player) => Player.GetComponent<PlayerHealth>().SwitchLevel(NextLevelPosition);
    }
}
