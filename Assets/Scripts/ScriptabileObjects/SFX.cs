using UnityEngine;

namespace planTopia.ScriptabileObjects
{
    [CreateAssetMenu(menuName = "planTopia/SFX", fileName = "SFX", order = 0)]
    public class SFX : ScriptableObject
    {
        public AudioClip SoundClip;
        public float Pitch;
    }
}

