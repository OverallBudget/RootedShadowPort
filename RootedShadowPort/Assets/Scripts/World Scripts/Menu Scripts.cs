using UnityEngine;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject background;// Reference to the settings menu canvas

    public GameObject displayMenu;
    public GameObject SoundMenu;
    public GameObject ControlsMenu;
    public bool isSettingsOpen = false;
    public Slider volumeSlider; // Reference to the volume slider
    public AudioSource audioSource; // Reference to the audio source
    public Slider sensitivitySlider; // Reference to the sensitivity slider
    public SpriteSwap fullScreenToggle; // Reference to the full screen toggle script



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.fullScreen = true;
        sensitivitySlider.value = FindFirstObjectByType<PlayerController>().sensitivity;
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

        if (Input.GetKeyDown(KeyCode.Escape) && isSettingsOpen == false)
        {
            Cursor.lockState = CursorLockMode.None;
            background.SetActive(true);
            settingsMenu.gameObject.SetActive(true);
            displayMenu.SetActive(true);
            SoundMenu.SetActive(false);
            ControlsMenu.SetActive(false);
            isSettingsOpen = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isSettingsOpen == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            background.SetActive(false);
            settingsMenu.gameObject.SetActive(false);
            isSettingsOpen = false;
        }
    }

            
           

    public void OpenDisplayMenu()
    {
        displayMenu.SetActive(true);
        SoundMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    public void OpenSoundMenu()
    {
        displayMenu.SetActive(false);
        SoundMenu.SetActive(true);
        ControlsMenu.SetActive(false);
    }

    public void OpenControlsMenu()
    {
        displayMenu.SetActive(false);
        SoundMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    public void Volume()
    {
        audioSource.volume = volumeSlider.value;
      
    }

    public void Sensitivity()
    {
        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        playerController.sensitivity = sensitivitySlider.value;
    }

    public void FullScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("FullScreen: " + Screen.fullScreen);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        background.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
        isSettingsOpen = false;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

}
