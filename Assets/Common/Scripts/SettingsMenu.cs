using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer _masterMixer;
    [SerializeField] Slider _sliderMasterAudio;
    [SerializeField] Slider _sliderBgm;
    [SerializeField] Slider _sliderSfx;
    private void SetAudioMixer(string attrib, float sliderVal, Slider slider)
    {
        slider.minValue = 0.0001f;
        _masterMixer.SetFloat(attrib, Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat(attrib, sliderVal);
        slider.SetValueWithoutNotify(sliderVal);
    }

    public void SetAudioMaster(float value) => SetAudioMixer("Vol_Master", value, _sliderMasterAudio);
    public void SetAudioBgm(float    value) => SetAudioMixer("Vol_BGM",    value, _sliderBgm);
    public void SetAudioSfx(float    value) => SetAudioMixer("Vol_SFX",    value, _sliderSfx);
}
