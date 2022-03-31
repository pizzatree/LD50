using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace _Plugins.TopherUtils.Audio
{
    [CreateAssetMenu(menuName = "Audio Event")]
    public class AudioEvent : ScriptableObject
    {
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private AudioClip[]     _clips;
        
        [SerializeField, Tooltip("Low bound, High bound, Range 0f-1f")]
        private Vector2 _volume = Vector2.one;

        [SerializeField, Tooltip("Low bound, High bound, Range 0f-2f")]
        private Vector2 _pitch = Vector2.one;

        public void Play(AudioSource audioSource, float? volume = null, float? pitch = null)
        {
            SetState(audioSource, volume, pitch);
            audioSource.Play();
        }

        public void PlayDelayed(AudioSource audioSource, float delay, float? volume = null, float? pitch = null)
        {
            SetState(audioSource, volume, pitch);
            audioSource.PlayDelayed(delay);
        }

        public void PlayOneShot(AudioSource audioSource, float? volume = null, float? pitch = null)
        {
            SetState(audioSource, volume, pitch);
            audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
        }
        
        private void SetState(AudioSource audioSource, float? volume = null, float? pitch = null)
        {
            audioSource.outputAudioMixerGroup = _mixer;
            audioSource.clip                  = _clips.RandomElement();
            audioSource.volume                = volume ?? _volume.RandomValue();
            audioSource.pitch                 = pitch  ?? _pitch.RandomValue();
        }

#if UNITY_EDITOR

        private bool _stopPreview;
        
        [ContextMenu("Start Preview")]
        private async void Preview()
        {
            var preview = EditorUtility.CreateGameObjectWithHideFlags(
                                                                      "*** Audio Preview",
                                                                      HideFlags.DontSave,
                                                                      typeof(AudioSource)
                                                                     ).GetComponent<AudioSource>();

            Play(preview);
            while(preview.isPlaying && !_stopPreview)
                await Task.Delay(100);

            DestroyImmediate(preview.gameObject);
            _stopPreview = false;
        }

        [ContextMenu("Stop Preview")]
        private void StopPreview() => _stopPreview = true;
        
#endif
    }
}