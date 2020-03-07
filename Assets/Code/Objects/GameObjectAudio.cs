using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectAudio : MonoBehaviour
{
    public AudioSource audioSource = null;
    public AudioClip[] audioClips = null;
    public float currentAudioClipLength = 0f;

    public void TriggerRandomAudio()
    {
        int i = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[i];
        currentAudioClipLength = audioSource.clip.length;
        audioSource.Play();
    }
}
