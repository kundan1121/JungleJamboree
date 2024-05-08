using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip[] jumpSounds;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        
        int randomIndex = Random.Range(0, jumpSounds.Length); 
        AudioClip clip = jumpSounds[randomIndex]; 
        source.PlayOneShot(clip); 
        Debug.Log(clip.name); 
    }
}

