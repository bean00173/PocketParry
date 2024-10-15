using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageVisualiser : MonoBehaviour
{
    public GameObject vignette;
    public DirectionalDamageIndicator t, tr, r, br, b, bl, l, tl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit(ParryDirection dir)
    {
        switch (dir)
        {
            case ParryDirection.Up: t.DoHit(); break;
            case ParryDirection.RightUp: tr.DoHit(); break;
            case ParryDirection.Right: r.DoHit(); break;
            case ParryDirection.RightDown: br.DoHit(); break;
            case ParryDirection.Down: b.DoHit(); break;
            case ParryDirection.LeftDown: bl.DoHit(); break;
            case ParryDirection.Left: l.DoHit(); break;
            case ParryDirection.LeftUp: tl.DoHit(); break;
        }
    }

    public void UpdateVignette(float health)
    {
        if(health == 3)
        {
            vignette.SetActive(false);
        }
        else if(health == 2)
        {
            vignette.SetActive(true);
            vignette.transform.localScale = Vector3.one * 1.6f;
            vignette.GetComponent<Image>().color = new Color(1, 1, 1, .25f);
        }
        else if (health == 1)
        {
            vignette.SetActive(true);
            vignette.transform.localScale = Vector3.one * 1.3f;
            vignette.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        }
        else
        {
            vignette.SetActive(true);
            vignette.transform.localScale = Vector3.one * 1f;
            vignette.GetComponent<Image>().color = new Color(1, 1, 1, .75f);
        }
        
    }
}
