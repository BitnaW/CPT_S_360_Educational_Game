using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuAudioSettings : MonoBehaviour
{
    private const string MasterVolumeParameter = "MasterVolume";
    // key used to save/load volume in player prefs
    private const string MasterVolumePrefKey = "MasterVolumeLinear";

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private float defaultVolume = 0.75f;
    [SerializeField] private AudioMixer volumeMixer;

    private void OnEnable()
    {
        // load saved volume (or default if none saved yet)
        float savedVolume = PlayerPrefs.GetFloat(MasterVolumePrefKey, defaultVolume);
        ApplyMasterVolume(savedVolume, false);

        // slider visual update
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.SetValueWithoutNotify(savedVolume);
        }
    }

    // called by slider OnValueChanged(float)
    public void SetMasterVolume(float linearVolume)
    {
        ApplyMasterVolume(linearVolume, true);
    }

    private void ApplyMasterVolume(float linearVolume, bool savePreference)
    {
        // clamp so it doesnt get weird values
        float clamped = Mathf.Clamp(linearVolume, 0.0001f, 1f);
        // convert slider linear value to mixer decibels
        float decibels = Mathf.Log10(clamped) * 20f;

        if (audioMixer != null)
        {
            audioMixer.SetFloat(MasterVolumeParameter, decibels);
        }

        // save when user changes setting
        if (savePreference)
        {
            PlayerPrefs.SetFloat(MasterVolumePrefKey, clamped);
            PlayerPrefs.Save();
        }
    }
}