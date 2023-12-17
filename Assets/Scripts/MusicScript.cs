using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScripting : MonoBehaviour
{
    public AudioClip loopMusic;

    AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        StartCoroutine(introSection());
    }

    IEnumerator introSection()
    {
        yield return new WaitUntil(() => !musicSource.isPlaying);
        musicSource.clip = loopMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}
