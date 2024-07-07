using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Setting : MonoBehaviour
{
    public Slider MouseSlider;
    public Slider SoundSlider;
    public float mouseSensitivity = 1f;
    public float soundVolume = 1f;

    private const string mouseSensitivity_KEY = "MouseSensitivity";
    private const string soundVolume_KEY = "SoundVolume";

    private static Setting instance;

    private GameObject Menu;

    void Awake()
    {
        StartMenu[] activeAndInactive = GameObject.FindObjectsOfType<StartMenu>(true);
        StartMenu MainMenu = activeAndInactive[0];
        Menu = MainMenu.gameObject;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartMenu[] activeAndInactive = GameObject.FindObjectsOfType<StartMenu>(true);
        StartMenu MainMenu = activeAndInactive[0];
        Menu = MainMenu.gameObject;
    }

    void Start()
    {
        MouseSlider.onValueChanged.AddListener(OnMouseSliderValueChanged);

        SoundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);

        if (PlayerPrefs.HasKey(mouseSensitivity_KEY))
        {
            mouseSensitivity = PlayerPrefs.GetFloat(mouseSensitivity_KEY);
            MouseSlider.SetValueWithoutNotify(mouseSensitivity);
        }

        if (PlayerPrefs.HasKey(soundVolume_KEY))
        {
            soundVolume = PlayerPrefs.GetFloat(soundVolume_KEY);
            SoundSlider.SetValueWithoutNotify(soundVolume);
        }
    }

    void OnMouseSliderValueChanged(float value)
    {
        mouseSensitivity = value;
        PlayerPrefs.SetFloat(mouseSensitivity_KEY, mouseSensitivity);
    }

    void OnSoundSliderValueChanged(float value)
    {
        soundVolume = value;
        PlayerPrefs.SetFloat(soundVolume_KEY, soundVolume);
        AudioListener.volume = soundVolume;
    }

    public void Return()
    {
        if(Menu != null)
        {
            Menu.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}