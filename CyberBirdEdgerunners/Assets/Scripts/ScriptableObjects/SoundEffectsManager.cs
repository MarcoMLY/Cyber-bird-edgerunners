using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

namespace UnityEngine.Audio
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SoundEffectsManager")]
    public class SoundEffectsManager : ScriptableObject
    {
        public void PlaySound(AudioClip clip, Transform transform)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }

        public void PlaySoundRandomized(AudioClip clip, float volumeRange, float normalVolume, float normalPitch, float pitchRange, AudioSource audioSource)
        {
            audioSource.pitch = Random.Range(normalPitch - (pitchRange / 2), normalPitch + (pitchRange / 2));
            audioSource.volume = Random.Range(normalVolume - volumeRange, normalVolume);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
