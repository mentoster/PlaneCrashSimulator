using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip SeatBelt;
    public AudioSource Audio;
    public AudioSource Audio2;

    private void Start()
    {
        Audio.mute = true;
    }

    public void PlaySeatBelt()
    {
        Audio2.enabled = false;
        Audio.mute = false;
        Audio.PlayOneShot(SeatBelt);
    }
}
