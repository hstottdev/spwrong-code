using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Color disabledColor;
    [SerializeField] Color normalColor;
    int whatIThoughtItWas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.health != whatIThoughtItWas)
        {
            whatIThoughtItWas = PlayerController.health;
            Debug.Log("Health Change");
            SetUI();
        }

        if (GameManager.hasInstance())
        {
            if (GameManager.inst.correctUI.activeInHierarchy || GameManager.inst.endScreen.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void SetUI()
    {
        for (int x = 0; x < transform.childCount; x++)
        {
            Image myImg = transform.GetChild(x).GetComponent<Image>();
            if (x < PlayerController.health)
            {
                if(myImg.color != normalColor)
                {
                    myImg.color = normalColor;
                }
            }
            else
            {
                if(myImg.color != disabledColor)
                {
                    myImg.color = disabledColor;
                    colourFlash flasher = transform.GetChild(x).gameObject.AddComponent<colourFlash>();
                    flasher.flashColour = normalColor;
                    flasher.timeInterval = 0.1f;
                    flasher.useMaximum = true;
                    flasher.maximumFlashes = 11;


                    transform.GetChild(x).GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
