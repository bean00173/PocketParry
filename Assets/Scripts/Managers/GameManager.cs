using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LevelInformation selectedLevel;

    private Button playButton;

    public UnityEvent onLevelLoad = new UnityEvent();

    private InputSetting[] inputSettings = new InputSetting[8];

    public Scene currentScene { get; private set; }

    //float m_tr_fill;
    //float m_tr_angle;
    //float m_tl_fill;
    //float m_tl_angle;
    //float m_br_fill;
    //float m_br_angle;
    //float m_bl_fill;
    //float m_bl_angle;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateReferences(Button playBtn)
    {
        this.playButton = playBtn;
        playButton.onClick.AddListener(LoadLevel);
    }

    public void UpdateLevelReference(Button exitBtn, Button quitBtn)
    {
        exitBtn.onClick.AddListener(ExitLevel);
        quitBtn.onClick.AddListener(Quit);
    }

    public void UpdateLevelInformation(LevelInformation info)
    {
        this.selectedLevel = info;
    }

    private void ExitLevel()
    {
        SceneManager.instance.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void LoadLevel()
    {
        SceneManager.instance.LoadScene("LevelScene", LoadSceneMode.Single);
    }

    public void UpdateCurrentScene(string name)
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StoreInputSettings(InputSetting tlf, InputSetting tla, InputSetting trf, InputSetting tra, InputSetting blf, InputSetting bla, InputSetting brf, InputSetting bra)
    {
        inputSettings = new InputSetting[]{tlf, tla, trf, tra, blf, bla, brf, bra};
        try
        {
            PlayerInput.Instance.LoadSettings(inputSettings);
        }
        catch(System.Exception e)
        {
            Debug.Log($"{e.GetType()} No Object of Type PlayerInput found"); 
        }
    }

    public InputSetting[] ReturnSavedInputSettings()
    {
        return inputSettings;
    }

    //public void StoreMixerInfo(Transform main)
    //{
    //    Slider sfxSlider = main.Find("soundbg/SFXSlider").GetComponent<Slider>();
    //    sfxSlider.onValueChanged.AddListener(AudioManager.instance.SetSfxVol);

    //    Slider musicSlider = main.Find("soundbg/MusicSlider").GetComponent<Slider>();
    //    musicSlider.onValueChanged.AddListener(AudioManager.instance.SetMusicVol);

    //    Slider ambienceSlider = main.Find("soundbg/AmbienceSlider").GetComponent<Slider>();
    //    ambienceSlider.onValueChanged.AddListener(AudioManager.instance.SetAmbienceVol);

    //    Slider playerSlider = main.Find("soundbg/PlayerSlider").GetComponent<Slider>();
    //    playerSlider.onValueChanged.AddListener(AudioManager.instance.SetPlayerVol);

    //    Slider enemySlider = main.Find("soundbg/EnemySlider").GetComponent<Slider>();
    //    enemySlider.onValueChanged.AddListener(AudioManager.instance.SetEnemyVol);

    //    Slider masterSlider = main.Find("soundbg/MasterSlider").GetComponent<Slider>();
    //    masterSlider.onValueChanged.AddListener(AudioManager.instance.SetMasterVol);
    //}
}
