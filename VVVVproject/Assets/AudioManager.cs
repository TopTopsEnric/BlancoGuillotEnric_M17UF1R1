using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip backgroundMusic; // Asigna el clip de m�sica de fondo en el Inspector
    private AudioSource audioSource;

    void Awake()
    {
        // Asegurarse de que solo hay un AudioManager en la escena y que persiste entre escenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // Hace que la m�sica se repita
        audioSource.Play();
    }

    // M�todo para controlar el volumen desde otros scripts si es necesario
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    // M�todo para cambiar la m�sica
    public void ChangeMusic(AudioClip newClip)
    {
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
