using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace planTopia
{
    [CreateAssetMenu(menuName = "planTopia/Speed", fileName = "Speed", order = 0)]
    public class Speed : ScriptableObject
    {
        [Range(0.1f, 100f)]
        public float speed;
    }
}
