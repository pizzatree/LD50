using UnityEngine;

namespace _Plugins.TopherUtils.Audio
{
    public class AudioReaction : MonoBehaviour
    {
        private AudioSource AudioSource
        {
            get
            {
                if(_audioSource is not null)
                    return _audioSource;

                _audioSource = !TryGetComponent<AudioSource>(out var existingAudioSource)
                    ? gameObject.AddComponent<AudioSource>()
                    : existingAudioSource;
                _audioSource.playOnAwake = false;
                return _audioSource;
            }
        }

        private AudioSource _audioSource;
        private float       _pitch;

        private void Start() => AudioSource.Stop();

        public void PlayEvent(AudioEvent audioEvent)
            => audioEvent.Play(AudioSource);

        public void PlayEventOneShot(AudioEvent audioEvent)
            => audioEvent.PlayOneShot(AudioSource);

        public void PlayEventOneShotWithSetPitch(AudioEvent audioEvent)
            => audioEvent.PlayOneShot(AudioSource, pitch: _pitch);

        public void PlayEventInSeconds(AudioEvent audioEvent, float delay)
            => audioEvent.PlayDelayed(AudioSource, delay);

        public void SetPitch(float pitch)
            => _pitch = pitch;
    }
}
