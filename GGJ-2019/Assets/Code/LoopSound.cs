using UnityEngine;

public class LoopSound : MonoBehaviour
{
    public AudioSource Sound;
    public float Delay;
    public float PitchDiff;

    private bool _playing;
    private float _delayLeft;

    public void Play()
    {
        _playing = true;
    }

    public void Stop()
    {
        _playing = false;
    }

    public void Update()
    {
        _delayLeft -= Time.deltaTime;

        if(_playing && !Sound.isPlaying && _delayLeft < 0)
        {
            Sound.pitch = 1 + Random.Range(-PitchDiff, PitchDiff);
            Sound.Play();
            _delayLeft = Delay;
        }
    }
}