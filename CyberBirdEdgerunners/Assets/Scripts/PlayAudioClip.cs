using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private SoundEffectsManager _soundEffectsManager;
    [SerializeField] private GameObject _soundEffect;
    [SerializeField] private float normalVolume;
    [SerializeField] private float normalPitch = 1;
    [SerializeField] private float volumeRange;
    [SerializeField] private float pitchRange;

    public void PayClip()
    {
        int randomClipIndex = Random.Range(0, _clips.Length);
        AudioClip randomClip = _clips[randomClipIndex];
        AudioSource audioSource = Instantiate(_soundEffect, transform.position, transform.rotation).GetComponent<AudioSource>();
        _soundEffectsManager.PlaySoundRandomized(randomClip, volumeRange, normalVolume, normalPitch, pitchRange, audioSource);
    }

    public void PayClip(Component component, int variable)
    {
        int randomClipIndex = Random.Range(0, _clips.Length);
        AudioClip randomClip = _clips[randomClipIndex];
        AudioSource audioSource = Instantiate(_soundEffect, transform.position, transform.rotation).GetComponent<AudioSource>();
        _soundEffectsManager.PlaySoundRandomized(randomClip, volumeRange, normalVolume, normalPitch, pitchRange, audioSource);
    }
}
