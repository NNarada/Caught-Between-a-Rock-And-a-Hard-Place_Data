using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public static MusicController musicControllerInstance;
    public AudioSource audioSource;

    public float musicVolume = 1f;

    // Start is called before the first frame update
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);

        if(musicControllerInstance == null)
        {
            musicControllerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void updateVolume( float volume)
    {
        musicVolume = volume;
    }
}
