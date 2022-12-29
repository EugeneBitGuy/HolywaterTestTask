using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public static class Extensions
{
    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        var component = obj.GetComponent<T>();
        if (component == null)
        {
            component = obj.AddComponent<T>();
        }

        return component;
    }
    
    public static void PlaySFX(string audioClipName)
    {
        var sfxClip = AssetLoader.AudioClips.FirstOrDefault(clip => clip.name == audioClipName);
        if(sfxClip == null)
            return;
        
        AudioSource audioSource = new GameObject().GetOrAddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sfxClip;
        audioSource.outputAudioMixerGroup = Resources.Load<AudioMixer>("Mixer").FindMatchingGroups("SFX")[0];
        audioSource.Play();
        Object.Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}
