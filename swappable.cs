using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer),typeof(colourFlashRenderer))]
public class swappable : MonoBehaviour
{
    [HideInInspector]public SpriteRenderer rend;
    [SerializeField] Animator animator;
    public string spriteName;

    [HideInInspector] public string originalSpriteName;
    public TextMeshPro nameText;

    [HideInInspector]public colourFlashRenderer flasher;
    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        originalSpriteName = spriteName;
        if (nameText != null)
        {
            nameText.gameObject.SetActive(false);
        }
        flasher = GetComponent<colourFlashRenderer>();
    }

    private void Update()
    {
        if (nameText != null)
        {
            nameText.text = "Sprite: "+spriteName;
        }

        SelectedCheck();
    }


    void SelectedCheck()
    {
        if (GetComponent<PlayerController>() == null)
        {
            bool selected = PlayerController.currentlyTouching == this;
            flasher.enabled = selected;
            nameText.gameObject.SetActive(selected);
        }
    }

    public void SwapWith(swappable toSwapWith)
    {
        //declaring my things
        Sprite mySprite = rend.sprite;
        string myName = spriteName;
        float myTextHeight = nameText.transform.localPosition.y;
        RuntimeAnimatorController myRuntimeController = animator.runtimeAnimatorController;

        animator.runtimeAnimatorController = toSwapWith.animator.runtimeAnimatorController;
        toSwapWith.animator.runtimeAnimatorController = myRuntimeController;

        rend.sprite = toSwapWith.rend.sprite;
        toSwapWith.rend.sprite = mySprite;

        spriteName = toSwapWith.spriteName;
        toSwapWith.spriteName = myName;

        nameText.transform.localPosition = new Vector2(nameText.transform.localPosition.x, toSwapWith.nameText.transform.localPosition.y);
        toSwapWith.nameText.transform.localPosition = new Vector2(toSwapWith.nameText.transform.localPosition.x, myTextHeight);//ur text height is my text height capeesh

    }
}
