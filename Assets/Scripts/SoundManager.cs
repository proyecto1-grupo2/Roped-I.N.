using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource fxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    //SFX Quimicos
    public AudioClip hielo;
    public AudioClip fuego;
    public AudioClip explosivo;
    public AudioClip electrico;
    public AudioClip salud;

    //SFX Entidades
    public AudioClip enemyDamage;
    public AudioClip hurt;
    public AudioClip playerJump;
    public AudioClip playerRun;
    public AudioClip dead;

    public float repeatRate;
    bool run;


    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;
	// Use this for initialization
	void Awake () {
        run = true;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    public void PlaySingle(AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }
	
    public void RandomizeSfx (params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        fxSource.pitch = randomPitch;
        fxSource.clip = clips[randomIndex];

        fxSource.Play();
    }

    public void RunSFX ()
    {
        if (run)
        {
            Debug.Log("run");
            RandomizeSfx(playerRun);
            run = false;
            Invoke("RunControl", repeatRate);
        }
    }

    private void RunControl()
    {
        run = true;
    }


    public void CallSoundManager(string sfx)
    {
       
        if(sfx == "jump")
        {
            RandomizeSfx(playerJump);
            Debug.Log(sfx);
        }
        else if (sfx == "hurt")
        {
            RandomizeSfx(hurt);
            Debug.Log(sfx);
        }
        else if (sfx == "dead")
        {
            RandomizeSfx(dead);
            Debug.Log(sfx);
        }
        else if (sfx == "enemyDamage")
        {
            RandomizeSfx(enemyDamage);
            Debug.Log(sfx);
        }
        else if (sfx == "salud")
        {
            RandomizeSfx(salud);
            Debug.Log(sfx);
        }
        else if (sfx == "hielo")
        {
            RandomizeSfx(hielo);
            Debug.Log(sfx);
        }
        else if (sfx == "explosivo")
        {
            RandomizeSfx(explosivo);
            Debug.Log(sfx);
        }
        else if (sfx == "fuego")
        {
            RandomizeSfx(fuego);
            Debug.Log(sfx);
        }
        else if (sfx == "electrico")
        {
            RandomizeSfx(electrico);
            Debug.Log(sfx);
        }
    }

}
