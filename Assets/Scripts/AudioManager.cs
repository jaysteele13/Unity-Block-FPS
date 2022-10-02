using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgm, rinCompleto; //Background Music

    public AudioSource[] soundEffects;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
  

    public void PlayLevelVictory()
    {
        StopBGM();
        rinCompleto.Play();
        
    }

    public void StopBGM()
    {
        bgm.Stop();
        //rinCompleto.Play(); 
    }

    public void PlaySFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
        soundEffects[sfxNumber].Play();
    }

    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }
}
