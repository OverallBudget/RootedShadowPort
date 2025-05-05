using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject background;// Reference to the settings menu canvas
    public bool sound;
    public bool controls;

    public GameObject displayMenu;
    public GameObject SoundMenu;
    public GameObject ControlsMenu;

    public static bool isSettingsOpen = false;
    public Slider volumeSlider; // Reference to the volume slider
    public AudioSource audioSource; // Reference to the audio source
    public AudioSource[] MonsterAudioSource; // Reference to the monster audio source

    public Slider sensitivitySlider; // Reference to the sensitivity slider
    public SpriteSwap fullScreenToggle; // Reference to the full screen toggle script
    public bool display;

    public TextMeshProUGUI displayButton; // Reference to the display button
    public TextMeshProUGUI soundButton; // Reference to the sound button
    public TextMeshProUGUI controlsButton; // Reference to the controls button




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.fullScreen = true;
        sensitivitySlider.value = FindFirstObjectByType<PlayerController>().sensitivity;
        sound = false;
        controls = false;
        display = false;
        SpiderTree[] monsters = FindObjectsOfType<SpiderTree>();
        MonsterAudioSource = new AudioSource[monsters.Length];
        for (int i = 0; i < monsters.Length; i++)
        {
            MonsterAudioSource[i] = monsters[i].GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        OpenSettings();
        Volume();
        Sensitivity();
        
        
    }

    public void OpenSettings()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isSettingsOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            background.SetActive(true);
            settingsMenu.SetActive(true);
            displayMenu.SetActive(true);
            SoundMenu.SetActive(false);
            ControlsMenu.SetActive(false);
            isSettingsOpen = true;
            display = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isSettingsOpen)
        {
            Resume(); // Call the Resume method to ensure consistent behavior
            display = false;
        }

        if (displayButton != null && display)
        {
            displayButton.text = "<u>Display</u>";
        }
        else
        {
            displayButton.text = "Display";

        }

        if (soundButton != null && sound)
        {
            soundButton.text = "<u>Sound</u>";
        }
        else
        {
            soundButton.text = "Sound";
        }

        if (controlsButton != null && controls)
        {
            controlsButton.text = "<u>Controls</u>";
        }
        else
        {
            controlsButton.text = "Controls";
        }
    }

            
           

    public void OpenDisplayMenu()
    {
        display = true;
        sound = false;
        controls = false;
        displayMenu.SetActive(true);
        SoundMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        Debug.Log("Display Menu Opened");




    }

    public void OpenSoundMenu()
    {
        display = false;
        sound = true;
        controls = false;
        displayMenu.SetActive(false);
        SoundMenu.SetActive(true);
        ControlsMenu.SetActive(false);
        Debug.Log("Sound Menu Opened");
    }

    public void OpenControlsMenu()
    {
        display = false;
        sound = false;
        controls = true;
        displayMenu.SetActive(false);
        SoundMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    public void Volume()
    {
        if(audioSource != null)
        {
            audioSource.volume = volumeSlider.value;
            foreach(AudioSource audio in MonsterAudioSource)
            {
                audio.volume = volumeSlider.value;
            }
        }
        
      
    }

    public void Sensitivity()
    {
        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        playerController.sensitivity = sensitivitySlider.value;
    }

    public void FullScreenToggle()
    {
        Debug.Log("Before Toggle: " + Screen.fullScreen);
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("After Toggle: " + Screen.fullScreen);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        background.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        isSettingsOpen = false;
        display = false;
        sound = false;
        controls = false;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

}
