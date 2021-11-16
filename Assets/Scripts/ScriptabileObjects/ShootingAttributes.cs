using UnityEngine;

namespace planTopia.ScriptabileObjects
{
    [CreateAssetMenu(menuName = "planTopia/Shooting Attributes", fileName = "Shooting Attributes", order = 0)]
    public class ShootingAttributes : ScriptableObject
    {
        [Range(1f, 10f)]
        public float Damage = 2;
        [Range(0f, 10f)]
        public float ShootingDistance = 0.75f;
        [Range(0.1f, 5f)]
        public float FireRate = 0.6f;
        [Range(10f, 100f)]
        public int MaxAmmunation = 50;
        public int CurrentAmmunation;
        public Material Weapon;
        public SFX GunSound;
        public Sprite UIImage;

    }
}
