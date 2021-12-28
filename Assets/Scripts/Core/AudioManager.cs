using System;
using planTopia.ScriptabileObjects;
using System.Collections.Generic;
using System.Linq;
using planTopia.Enviroment;
using UnityEngine;

namespace planTopia.Core
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private List<SFX> SFXList;
        [SerializeField] private AudioSource AudioSource;
        private bool IsDay { get; set; }
        
        public void Play(SFX AudioClip)
        {
            Debug.Log(AudioClip.name);
            var sound = SFXList.Single(sound => sound.SoundClip.Equals(AudioClip.SoundClip));
            if (sound.name == "DaySound" || sound.name == "NightSound" || sound.name == "GameOver")
                Stop();
            if (sound.name == "GameOver")
            {
                Stop();
                Invoke(nameof(SetBackGroundMusic), 4.5f);
            }

            AudioSource.PlayOneShot(sound.SoundClip, (float) sound.Pitch / 100);
        }

        public void Stop() => AudioSource.Stop();

        public void Pause() => AudioSource.Pause();

        public void CheckIsDay(bool isDay) => IsDay = isDay;

        public void SetBackGroundMusic()
        {
            if (IsDay)
                Play(SFXList.SingleOrDefault(x => x.SoundClip.name == "DaySound"));
            else
                Play(SFXList.SingleOrDefault(x => x.SoundClip.name == "NightSound"));
        }
    }
}