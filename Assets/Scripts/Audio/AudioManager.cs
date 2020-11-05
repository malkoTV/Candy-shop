using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    static Dictionary<LoopAudioName, AudioClip> loopAudio =
        new Dictionary<LoopAudioName, AudioClip>();

    public static bool Initialized
    {
        get { return initialized; }
    }

    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;

        var values1 = Enum.GetValues(typeof(AudioClipName));
        foreach (var item in values1)
        {
            AudioClipName objName = (AudioClipName)Convert.ChangeType(item, typeof(AudioClipName));
            audioClips.Add(objName, Resources.Load<AudioClip>("Audio/" + objName));
        }

        var values2 = Enum.GetValues(typeof(LoopAudioName));
        foreach (var item in values2)
        {
            LoopAudioName objName = (LoopAudioName)Convert.ChangeType(item, typeof(LoopAudioName));
            loopAudio.Add(objName, Resources.Load<AudioClip>("Music/" + objName));
        }
    }

    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    public static void PlayLoop(LoopAudioName name)
    {
        audioSource.clip = loopAudio[name];
        audioSource.Play();
        audioSource.loop = true;
    }
}
