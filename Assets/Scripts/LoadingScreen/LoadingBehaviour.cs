using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LoadingBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject progressBar/*, continuePrompt, bg*/;
    //[SerializeField] private TextMeshProUGUI progressText;

    public float loadDelay;
    //public GameObject tipPanel;

    //public List<Sprite> backgrounds = new List<Sprite>();

    private void Start()
    {
        //AudioManager.instance.SetState(GameState.Loading);
        //AudioInterrupt(true);
        StartCoroutine(LoadSceneAsync());
    }

    //private void ChooseBG()
    //{
    //    if((LoadingData.sceneToLoad == "TowerTest" && GameManager.instance.currentScene.name == "LevelTest") || (LoadingData.sceneToLoad == "LevelTest"))
    //    {
    //        int bgIndex = 0;
    //        switch (GameManager.activeCharacter.Id)
    //        {
    //            case Character.CharacterId.seb: bgIndex = 0; break;
    //            case Character.CharacterId.rav: bgIndex = 1; break; // CHANGE WHEN MORE CHARACTERS
    //            case Character.CharacterId.abi: bgIndex = 2; break;
    //        }
    //        bg.GetComponent<Image>().sprite = backgrounds[bgIndex];

    //        tipPanel.SetActive(true);
    //    }
    //    else
    //    {
    //        bg.GetComponent<Image>().sprite = backgrounds[backgrounds.Count - 1];
    //    }
    //}

    IEnumerator LoadSceneAsync()
    {
        //ChooseBG();

        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadingData.mode);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            progressBar.GetComponent<Image>().fillAmount = Mathf.Lerp(progressBar.GetComponent<Image>().fillAmount, operation.progress + 0.1f, loadDelay * Time.deltaTime);
            //yield return new WaitUntil(() => progressBar.GetComponent<Image>().fillAmount == 1f);
            //progressText.text = "(" + (operation.progress * 100 + 10).ToString() + "%)";

            if (operation.progress >= .9f/* && progressBar.GetComponent<Image>().fillAmount >= operation.progress*/) 
            {
                GameManager.Instance.UpdateCurrentScene(LoadingData.sceneToLoad);

                //continuePrompt.gameObject.SetActive(true);

                //if (Input.GetKeyDown(KeyCode.Return))
                //{
                //    //AudioInterrupt(false);
                //    //operation.allowSceneActivation = true;
                //    //UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LoadingScreen");
                //}

                operation.allowSceneActivation = true;
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LoadingScreen");
            }
            yield return null;
        }

        //if (LoadingData.sceneToLoad == "TowerTest" && GameManager.Instance.currentScene.name == "LevelTest")
        //{
        //    UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LevelTest");

        //    GameManager.Instance.UpdateCurrentScene(LoadingData.sceneToLoad);

        //    //while (progressBar.transform.localScale != new Vector3(1,1,1))
        //    //{
        //    //    yield return new WaitForSeconds(1f);


        //    //}
        //    while(progressBar.GetComponent<Image>().fillAmount < .9f)
        //    {
        //        progressBar.GetComponent<Image>().fillAmount = Mathf.Lerp(progressBar.GetComponent<Image>().fillAmount, 1f, loadDelay * Time.deltaTime);
        //    }
        //    //progressText.text = "(100%)";
        //    //yield return new WaitUntil(() => progressBar.GetComponent<Image>().fillAmount == 1f);

        //    continuePrompt.gameObject.SetActive(true);

        //    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        //    continuePrompt.gameObject.SetActive(false);
        //    ;
        //    UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LoadingScreen");

        //    AudioInterrupt(false);

        //    //GameManager.Instance.deathListener = false;
        //    SceneManager.instance.ToggleObjects(true);

        //    //if (Input.GetKeyDown(KeyCode.Return))
        //    //{
        //    //    continuePrompt.gameObject.SetActive(false);
        //    //    ;
        //    //    UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LoadingScreen");

        //    //    AudioInterrupt(false);

        //    //    GameManager.instance.deathListener = false;
        //    //    SceneManager.instance.ToggleObjects(true);
        //    //}
        //    yield return null;
        //}
        //else if(LoadingData.sceneToLoad == "LevelTest" && GameManager.Instance.currentScene.name == "LevelTest")
        //{
        //    UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LevelTest");

        //    AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadingData.mode);

        //    operation.allowSceneActivation = false;

        //    while (!operation.isDone)
        //    {
        //        progressBar.GetComponent<Image>().fillAmount = Mathf.Lerp(progressBar.GetComponent<Image>().fillAmount, operation.progress + 0.1f, loadDelay * Time.deltaTime);
        //        //yield return new WaitUntil(() => progressBar.GetComponent<Image>().fillAmount == 1f);
        //        //progressText.text = "(" + (operation.progress * 100 + 10).ToString() + "%)";

        //        if (operation.progress >= .9f && progressBar.GetComponent<Image>().fillAmount >= operation.progress)
        //        {
        //            GameManager.Instance.UpdateCurrentScene(LoadingData.sceneToLoad);

        //            continuePrompt.gameObject.SetActive(true);

        //            if (Input.GetKeyDown(KeyCode.Return))
        //            {
        //                AudioInterrupt(false);
        //                continuePrompt.gameObject.SetActive(false);
        //                operation.allowSceneActivation = true;
        //                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LoadingScreen");
        //            }
        //        }
        //        yield return null;
        //    }
        //}
        //else
        //{
        //    AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadingData.mode);

        //    operation.allowSceneActivation = false;

        //    while (!operation.isDone)
        //    {
        //        progressBar.GetComponent<Image>().fillAmount = Mathf.Lerp(progressBar.GetComponent<Image>().fillAmount, operation.progress + 0.1f, loadDelay * Time.deltaTime);
        //        //yield return new WaitUntil(() => progressBar.GetComponent<Image>().fillAmount == 1f);
        //        //progressText.text = "(" + (operation.progress * 100 + 10).ToString() + "%)";

        //        if (operation.progress >= .9f && progressBar.GetComponent<Image>().fillAmount >= operation.progress)
        //        {
        //            GameManager.Instance.UpdateCurrentScene(LoadingData.sceneToLoad);

        //            continuePrompt.gameObject.SetActive(true);

        //            if (Input.GetKeyDown(KeyCode.Return))
        //            {
        //                AudioInterrupt(false);
        //                operation.allowSceneActivation = true;
        //                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LoadingScreen");
        //            }
        //        }
        //        yield return null;
        //    }
        //}        
    }

    private void AudioInterrupt(bool interrupt)
    {
        //AudioManager.instance.interrupt = interrupt;
    }
}
