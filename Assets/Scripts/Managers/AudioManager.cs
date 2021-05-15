using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip loseSound;
    [SerializeField]
    private AudioClip successSound;
    [SerializeField]
    private AudioClip victorySound;

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound, 1f);
    }

    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound, 15f);
    }

    public void PlaySuccessSound()
    {
        audioSource.PlayOneShot(successSound, 1f);
    }

    public void PlayVictorySound()
    {
        audioSource.clip = victorySound;
        audioSource.PlayDelayed(0.25f);
    }
}