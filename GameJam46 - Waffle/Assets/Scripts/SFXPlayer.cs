using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {

    AudioSource aS;

    public AudioClip hoverSound, clickSound, moveSound, enemyMoveSound;
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

    public void PlayEnemeyMoveSound() {
        aS.clip = enemyMoveSound;
        aS.Play();
    }

    public void UnDoHasPlayed() {
        hasPlayed = false;

    }
}
