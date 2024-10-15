using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpriteSelector : MonoBehaviour
{
    public List<Sprite> sprites;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
