using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectGameScene : MonoBehaviour
{
    public AudioSource soundPlayButtonDrill;
    public AudioSource soundPlayButtonHammer;
    public AudioSource soundPlayButtonLockPick;
    public AudioSource soundPlayButtonUI;
    public AudioSource soundPlayButtonDynamite;


    public void PlaySoundEffectButtonDrill()
    {
        soundPlayButtonDrill.Play();
    }

    public void PlaySoundEffectButtonHammer()
    {
        soundPlayButtonHammer.Play();
    }

    public void PlaySoundEffectButtonLockPick()
    {
        soundPlayButtonLockPick.Play();
    }
    
    public void PlaySoundEffectButtonUI()
    {
        soundPlayButtonUI.Play();
    }
    
    public void PlaySoundEffectButtonDynamite()
    {
        soundPlayButtonDynamite.Play();
    }

}
