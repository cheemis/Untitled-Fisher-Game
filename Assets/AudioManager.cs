using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : UnitySingleton<AudioManager>
{
    [System.Serializable]
    public class AudioObject
    {
        public string id;
        public AudioSource sound;
        private float volume;
        public AudioObject(string newId, AudioSource newSound, float newVolume)
        {
            id = newId;
            sound = newSound;
            sound.volume = newVolume;
            volume = sound.volume;
        }

        public void Play(float pitch = 1, float pitchOffset = 0f)
        {
            sound.pitch = pitch + Random.Range(-pitchOffset, pitchOffset);
            sound.Play();
        }

        public void Stop()
        {
            sound.Stop();
        }
        public IEnumerator FadeOut(float FadeTime = 1f)
        {
            float startVolume = sound.volume;

            while (sound.volume > 0)
            {
                sound.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            sound.Stop();
            sound.volume = startVolume;
        }
        /*public IEnumerator FadeIn(float FadeTime=1f) {
            while (sound.volume < volume) {
                sound.volume += startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            sound.Stop();
            sound.volume = startVolume;
        }*/
    }

    public List<AudioObject> audioObjects = new List<AudioObject>();
    // Start is called before the first frame update
    void Start()
    {

    }
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(string key, float pitch = 1f, float offset = 0f)
    {
        foreach (AudioObject obj in audioObjects)
        {
            if (obj.id == key)
            {
                obj.Play(pitch, offset);
                return;
            }
        }
    }
    public void Stop(string key, bool fade = true)
    {
        foreach (AudioObject obj in audioObjects)
        {
            if (obj.id == key)
            {
                StartCoroutine(obj.FadeOut(1f));
                return;
            }
        }
    }
}