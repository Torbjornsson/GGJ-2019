using System.Collections.Generic;
using UnityEngine;

public class VariationSounds : MonoBehaviour
{
    public AudioSource Source;
    public float PitchDiff = 0;
    public List<AudioClip> Clips = new List<AudioClip>();

    public void Play()
    {
        Source.clip = Clips[Random.Range(0, Clips.Count - 1)];
        Source.pitch = 1 + Random.Range(-PitchDiff, PitchDiff);
        Source.Play();
    }
}