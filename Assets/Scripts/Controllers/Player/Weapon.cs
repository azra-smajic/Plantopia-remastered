using planTopia.Core;
using planTopia.ScriptabileObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace planTopia.Controllers.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private List<ShootingAttributes> Weapons;
        [SerializeField]
        private Image UIGunImage;
        [SerializeField]
        private Text UIAmmunation;
        [SerializeField]
        private SFX GunLoadingSound;
        private SkinnedMeshRenderer Renderer { get; set; }
        private AudioManager AudioManager { get; set; }


        private void Start()
        {
            Renderer = this.GetComponentInChildren<SkinnedMeshRenderer>();
            AudioManager = this.GetComponent<AudioManager>();
            SetGreenWeapon();
        }
        private void SetWeaponAndSound(ShootingAttributes Gun)
        {
            Renderer.material = Gun.Weapon;
            UIGunImage.sprite = Gun.UIImage;
            Gun.CurrentAmmunation = Gun.MaxAmmunation;
            UIAmmunation.text = $"{Gun.CurrentAmmunation}/{Gun.MaxAmmunation}";
            this.GetComponent<ShootingWeapon>().SetShootingAttributes(Gun);
            AudioManager.Play(GunLoadingSound);
        }
        public void SetRedWeapon()
        {
            var Gun = Weapons.Where(x => x.name == Constants.Tag.RedWeapon).FirstOrDefault();
            if (Gun != null)
                SetWeaponAndSound(Gun);
        }
        public void SetYellowWeapon()
        {
            var Gun = Weapons.Where(x => x.name == Constants.Tag.YellowWeapon).FirstOrDefault();
            if (Gun != null)
                SetWeaponAndSound(Gun);
        }
        public void SetBlueWeapon()
        {
            var Gun = Weapons.Where(x => x.name == Constants.Tag.BlueWeapon).FirstOrDefault();
            if (Gun != null)
                SetWeaponAndSound(Gun);
        }
        public void SetGreenWeapon()
        {
            var Gun = Weapons.Where(x => x.name == Constants.Tag.GreenWeapon).FirstOrDefault();
            if (Gun != null)
                SetWeaponAndSound(Gun);
        }
        public void SetBlackWeapon()
        {
            var Gun = Weapons.Where(x => x.name == Constants.Tag.BlackWeapon).FirstOrDefault();
            if (Gun != null)
                SetWeaponAndSound(Gun);
        }

    }
}
