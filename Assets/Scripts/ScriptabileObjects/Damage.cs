using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    [CreateAssetMenu(menuName = "planTopia/Damage", fileName = "Damage", order = 0)]
    public class Damage : ScriptableObject
    {
        public int AmountOfDamage  = 1;
        public int StartingDamage;
    }
}
