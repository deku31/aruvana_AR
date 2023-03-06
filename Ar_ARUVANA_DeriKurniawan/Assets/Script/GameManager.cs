using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource sfx;
    public AudioSource Bgm;
    public List<ParticleSystem> fireparticel;
   
    public void FireSoundPlay()
    {
        foreach (var fire in fireparticel)
        {
            fire.Play();
        }
        Bgm.Play();
        sfx.Play();
        sfx.loop = true;
    }
    public void FireSoundStop()
    {
        foreach (var fire in fireparticel)
        {
            fire.Stop();
        }
        Bgm.Pause();
        sfx.Stop();
    }

}
