using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class efectosPlayer : MonoBehaviour
{
    public AudioSource audioSource; // Un único AudioSource
    public AudioClip jumpSound;
    public AudioClip hitSound;
    

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

  
}
