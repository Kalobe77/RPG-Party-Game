/********************************************************************************
 *   Filename:   MusicLoop.cs
 *   Date:       2023-05-10
 *   Authors:    Kaleb Gearinger and Adam Stefan
 *   Email:      kgearinger@muhlenberg.edu and astefan@muhlenberg.edu
 *   Description:
 *       This file handles the temporary music played in the game currently.
 *       (Needs to be updated when possible)
 ********************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{

    public AudioSource musicSource;
	public AudioClip musicStart;
    
    // Start is called before the first frame update
    void Start()
    {
        musicSource.PlayOneShot(musicStart);
		musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
