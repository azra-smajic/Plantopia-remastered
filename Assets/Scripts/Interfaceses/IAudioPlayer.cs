using planTopia.ScriptabileObjects;

namespace planTopia.Interfaceses
{
    public interface IAudioPlayer
    {
        public void Play(SFX AudioClip);
        public void Stop();
        public void Pause();
    }
}
