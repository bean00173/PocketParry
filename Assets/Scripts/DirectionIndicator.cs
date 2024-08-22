using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionIndicator : MonoBehaviour
{
    public PlayerInput input;
    public DraggedDirection direction;
    // Start is called before the first frame update
    void Start()
    {
        input.inputHandled.AddListener(StartColourChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartColourChange(DraggedDirection dir)
    {
        if(direction == dir) StartCoroutine(DoColourChange());
    }

    private IEnumerator DoColourChange()
    {
        this.GetComponent<Image>().color = Color.green;

        yield return new WaitForSeconds(.1f);

        this.GetComponent<Image>().color = Color.red;
    }
}
