using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
/// <summary>
/// Cette classe g�re le menu des param�tres du jeu, permettant � l'utilisateur de modifier les r�glages
/// tels que le volume de la musique, le volume des sons, le mode plein �cran et la r�solution de l'�cran.
/// </summary>
public class settingsMenu : MonoBehaviour
{
    private Resolution[] resolutions;

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Slider musicSlider;
    public Slider soundSlider;
    public Toggle fullScreenToggle;
    //public AudioSource[] soundsPlayer;
    //public AudioSource musicPlayer;

    /// <summary>
    /// M�thode d'initialisation qui r�cup�re la liste des r�solutions disponibles et charge les param�tres du jeu.
    /// </summary>
    private void Start()
    {
        Debug.Log("Settings default volume ="+SettingsManager.Instance.MusicVolume);
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //Screen.fullScreen = SettingsManager.Instance.IsFullScreen;
        LoadSettings();

    }

    /// <summary>
    /// M�thode appel�e lorsque l'utilisateur modifie le volume de la musique.
    /// </summary>
    public void SetVolume()
    {
        SettingsManager.Instance.MusicVolume = musicSlider.value;
        SettingsManager.Instance.MusicSource.volume =  musicSlider.value;
    }

    /// <summary>
    /// M�thode appel�e lorsque l'utilisateur modifie le volume des effets sonores.
    /// </summary>
    public void SetSoundVolume()
    {
        SettingsManager.Instance.SoundVolume = soundSlider.value;
        foreach(var soundPlayer in SettingsManager.Instance.SoundsSource){
            soundPlayer.volume =  soundSlider.value;
        }
    }

    /// <summary>
    /// M�thode appel�e lorsque l'utilisateur modifie l'�tat du mode plein �cran.
    /// </summary>
    public void SetFullScreen()
    {
        SettingsManager.Instance.IsFullScreen = fullScreenToggle.isOn;
        Screen.fullScreen = fullScreenToggle.isOn;
    }

    /// <summary>
    /// M�thode appel�e lorsque l'utilisateur change la r�solution.
    /// </summary>
    public void SetResolution()
    {
        if (resolutions == null || resolutionDropdown.value < 0 || resolutionDropdown.value >= resolutions.Length)
        {
            Debug.LogError("Invalid resolution index.");
            return;
        }

        SettingsManager.Instance.ResolutionIndex = resolutionDropdown.value;
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// M�thode pour charger les param�tres actuels � partir de SettingsManager et les afficher dans le menu.
    /// </summary>
    private void LoadSettings()
    {
        musicSlider.value = SettingsManager.Instance.MusicVolume;
        soundSlider.value = SettingsManager.Instance.SoundVolume;
        fullScreenToggle.isOn = SettingsManager.Instance.IsFullScreen;
        resolutionDropdown.value = SettingsManager.Instance.ResolutionIndex;
    }
}
