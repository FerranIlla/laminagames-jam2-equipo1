using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance; //Singleton pattern
    [HideInInspector] public AudioSource gameplayMusicSource;
    [SerializeField] public float musicMinVolume = 0.2f;
    [SerializeField] private float musicMaxVolume = 0.6f;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }


    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    public AudioSource PlaySoundGetReference(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return null;
        }

        s.source.Play();
        return s.source;
    }

    public void PlayGameplayMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return ;
        }

        s.source.Play();
        gameplayMusicSource = s.source;
    }


    public void PlaySoundAdditive(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        Sound newSound = new Sound();
        newSound.source = gameObject.AddComponent<AudioSource>();
        newSound.source.clip = s.clip;

        newSound.source.volume = s.volume;
        newSound.source.pitch = s.pitch;

        newSound.source.Play();
        StartCoroutine(DestroyAudioSource(newSound));
    }

    IEnumerator DestroyAudioSource(Sound sound)
    {
        yield return new WaitForSeconds(4f);
        Destroy(sound.source);
    }

    public void ZoomIn()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeVolumeOverTime(gameplayMusicSource.volume, musicMaxVolume));
    }

    public void ZoomOut()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeVolumeOverTime(gameplayMusicSource.volume, musicMinVolume));
    }

    IEnumerator ChangeVolumeOverTime(float currentVolume, float targetVolume)
    {
        if (gameplayMusicSource != null)
        {
            float zoomSpeed = 1f;
            float t = 0;
            while (t < 1f)
            {
                gameplayMusicSource.volume = Mathf.SmoothStep(currentVolume, targetVolume, t);
                t += Time.deltaTime * zoomSpeed;
                yield return null;
            }
        }
    }
}
