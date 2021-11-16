using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    [CreateAssetMenu(menuName = "planTopia/Health", fileName = "Health", order = 0)]
    public class Health : ScriptableObject
    {
        [Range(1f,1000f)]
        public float StartingHealt = 10;
  
    }
}
