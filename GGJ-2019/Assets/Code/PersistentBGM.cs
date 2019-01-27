using UnityEngine;

public class PersistentBGM : Singleton<PersistentBGM>
{
    public AudioSource Audio;

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}