using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   
    public AudioSource musicSource;
    public AudioSource efxSource;
    public AudioClip[] expQuotes;
    public AudioClip[] generalQuotes;
 
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
        playSingleQuote(generalQuotes[Random.Range(0,expQuotes.Length)]);
    }
	
}
