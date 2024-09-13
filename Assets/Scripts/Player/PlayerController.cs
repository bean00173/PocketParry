using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public UnityEvent onFinisherPlay;
    EnemyBehaviour currentEnemy;
    //Animator ac;
    bool playingFinisher;

    public GameObject cam, hands;
    public GameObject newArms, finCam;

    // Start is called before the first frame update
    void Start()
    {
        //ac = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy.CurrentState == EnemyState.Dead && !playingFinisher)
        {
            playingFinisher = true;
            PlayFinisher();
        }
    }

    public void SetEnemy(EnemyBehaviour enemy)
    {
        currentEnemy = enemy;
    }

    public void PlayFinisher()
    {
        cam.SetActive(false);
        finCam.SetActive(true);
        Invoke(nameof(ActivateArms), 1f);
    }

    public void ActivateArms()
    {
        newArms.SetActive(true);
    }

    //private IEnumerator PrepareFinisher()
    //{
    //    float time = 0;

    //    Vector3 camStartPos = cam.position;
    //    Vector3 handsStartPos = hands.position;

    //    while (time < transitionDuration)
    //    {
    //        cam.position = Vector3.Lerp(camStartPos, camTarget.position, time / transitionDuration);
    //        hands.position = Vector3.Lerp(handsStartPos, handsTarget.position, time / transitionDuration);

    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //}
}
