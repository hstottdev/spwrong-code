using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class colourFlashRenderer : MonoBehaviour
{
    public Color flashColour;
    Color originalColour;
    public float timeInterval = 0.5f;

    [Header("Limits")]
    public bool useMaximum;
    public int maximumFlashes;
    int flashCount;

    // Start is called before the first frame update
    void Start()
    {
        originalColour = GetComponent<SpriteRenderer>().color;
    }
    private void OnEnable()
    {
        Invoke("ColourSwitch", timeInterval);
    }
    void ColourSwitch()
    {
        SpriteRenderer g = GetComponent<SpriteRenderer>();
        if (g.color == originalColour)
        {
            g.color = flashColour;
        }
        else
        {
            g.color = originalColour;
        }
        if (useMaximum)
        {
            if(flashCount < maximumFlashes)
            {
                flashCount += 1;
                Invoke("ColourSwitch", timeInterval);
            }
            else
            {
                g.color = originalColour;
            }
        }
        else
        {
            Invoke("ColourSwitch", timeInterval);
        }
    }

    private void OnDisable()
    {
        SpriteRenderer g = GetComponent<SpriteRenderer>();
        g.color = originalColour;
        flashCount = 0;
        CancelInvoke();
    }
}
