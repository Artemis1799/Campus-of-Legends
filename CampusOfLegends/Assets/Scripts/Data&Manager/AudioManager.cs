using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère la lecture et la transition des pistes audio dans une playlist.
/// Permet la lecture continue et aléatoire de pistes, tout en s'assurant qu'une piste ne se répète pas immédiatement.
/// Implémente un singleton pour garantir qu'il n'y ait qu'une seule instance active de AudioManager.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    public AudioClip[] playlist;
    public AudioSource audioPlayer;
    public int musicPlayedIndex;

    private void Start()
    {
        // Sélection d'une piste aléatoire au démarrage
        musicPlayedIndex = Random.Range(0, playlist.Length);
        audioPlayer.clip = playlist[musicPlayedIndex];
        audioPlayer.Play();
    }

    private void Update()
    {
        // Vérifie si la piste actuelle est terminée, puis joue une autre piste
        if (!audioPlayer.isPlaying)
        {
            PlayNextRandomTrack();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlayNextRandomTrack()
    {
        int newIndex;

        do
        {
            // Sélectionne une nouvelle piste aléatoire, différente de l'actuelle
            newIndex = Random.Range(0, playlist.Length);
        }
        while (newIndex == musicPlayedIndex);

        // Met à jour l'index et joue la nouvelle piste
        musicPlayedIndex = newIndex;
        audioPlayer.clip = playlist[musicPlayedIndex];
        audioPlayer.Play();
    }
}
