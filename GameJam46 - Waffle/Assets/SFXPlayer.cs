using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {

    AudioSource aS;

    public AudioClip hoverSound, clickSound, moveSound;
    bool hasPlayed;

	// Use this for initialization
	void Start () {
        aS = GetComponent<AudioSource>();
	}

    public void PlayHoverSound() {
        
        if (!hasPlayed)
        {
            //print("Play Hover Sound");
            aS.clip = hoverSound;
            aS.Play();
            hasPlayed = true;
        }
    }

    public void PlayClickSound() {
        aS.clip = clickSound;
        aS.Play();
    }

    public void PlayMoveSound() {
        aS.clip = moveSound;
        aS.Play();

    }

    public void UnDoHasPlayed() {
        hasPlayed = false;

    }
}
