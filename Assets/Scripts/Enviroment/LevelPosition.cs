using planTopia.Controllers.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Enviroment
{
    public class LevelPosition : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == Constants.Tag.PLAYER)
                SwitchPosition(collision.gameObject);
        }
        private void SwitchPosition(GameObject Player) => Player.GetComponent<PlayerHealth>().SwitchLevel(this.transform, false);
    }
}
