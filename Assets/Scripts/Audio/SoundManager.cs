using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   
    public AudioSource musicSource;
    public AudioSource efxSource;
    public AudioSource sfxSource;
    public AudioClip[] expQuotes;
    public AudioClip[] generalQuotes;
    public AudioClip furbie;
    public AudioClip lavaLamp;
    public AudioClip throwIt;
 
    public static SoundManager instance = null;
   
    void Awake ()
    {
        if (instance == null)
            instance = this ;
        else if (instance != this)
            Destroy(gameObject);
    }
    
	
    public void playSingleQuote(AudioClip clip)
    {
        if (!efxSource.isPlaying)
        {
            efxSource.clip = clip;
            efxSource.Play();
        }
		
    }

    public void playGeneralQuote()
    {
        playSingleQuote(generalQuotes[Random.Range(0, generalQuotes.Length)]);
    }

    public void playExpQuote()
    {
        playSingleQuote(expQuotes[Random.Range(0,expQuotes.Length)]);
    }

    public void playFurbie()
    {
        sfxSource.clip = furbie;
        sfxSource.Play();
    }

    public void playLavaLamp()
    {
        sfxSource.clip = lavaLamp;
        sfxSource.Play();
    }


    public void playThrowIt()
    {
        sfxSource.clip = throwIt;
        sfxSource.Play();
    }
}
