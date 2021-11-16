using planTopia.ScriptabileObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace planTopia.Core
{
    public class AudioManager: MonoBehaviour
    {
        [SerializeField]
        private AudioSource AudioSource;
        [SerializeField]
        private List<SFX> SFXList;

        public void Play(SFX AudioClip)
        {
            var sound = SFXList.Where(sound => sound.SoundClip.Equals(AudioClip.SoundClip)).Single();
            AudioSource.PlayOneShot(sound.SoundClip, (float)sound.Pitch / 100);
        }
        public void Stop() => AudioSource.Stop();

        public void Pause() => AudioSource.Pause();
    }
}
