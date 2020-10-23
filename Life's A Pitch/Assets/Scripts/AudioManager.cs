using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource AudioS;
    
    void Start()
    {
        
        instance = this;
        //ASSIGNS AUDIO SOURCE
        AudioS = GetComponent<AudioSource>();

    }

    public void playClip(string clip, float f)
    {
        //LOADS CLIP AS DEFINED BY STRING FROM RESOURCES
        AudioClip clipPlayed = Resources.Load("Sounds/" + clip.ToString()) as AudioClip;
        //PLAYS CLIP
        AudioS.PlayOneShot(clipPlayed,f);
    }


    
}
