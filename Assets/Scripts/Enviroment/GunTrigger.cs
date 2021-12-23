using planTopia.Controllers.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia.Enviroment
{
    public class GunTrigger : MonoBehaviour
    {
        private Weapon Weapons { get; set; }
        private void Start()
        {
            Weapons = this.GetComponentInChildren<Weapon>();
        }
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.SetActive(false);

            switch (other.gameObject.tag)
            {
                case Constants.Tag.BlueGun:
                    Weapons.SetBlueWeapon();
                    break;
                case Constants.Tag.RedGun:
                    Weapons.SetRedWeapon();
                    break;
                case Constants.Tag.YellowGun:
                    Weapons.SetYellowWeapon();
                    break;
                case Constants.Tag.GreenGun:
                    Weapons.SetGreenWeapon();
                    break;
                case Constants.Tag.BlackGun:
                    Weapons.SetBlackWeapon();
                    break;
                default: Debug.Log("Weapon is not found!");
                    break;  
            }
        }
    }
}
