using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnHitbox : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] GameObject objectToTurnOn;
    [SerializeField] float disableDelay = 3;
    [SerializeField] bool destroyOnTriggerExit;
    [SerializeField] bool colourFadeObject;
    bool destroying;
    bool leftHitbox;
    float timeSinceLeftHitbox;
    // Start is called before the first frame update
    void Awake()
    {
        objectToTurnOn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHitbox)
        {
            timeSinceLeftHitbox += Time.deltaTime;
        }

        if (timeSinceLeftHitbox > disableDelay && !destroying)
        {
            destroying = true;
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == playerTag && !destroying)
        {
            objectToTurnOn.SetActive(true);
            leftHitbox = false;
            timeSinceLeftHitbox = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerTag && !destroying)
        {
            leftHitbox = true;
            timeSinceLeftHitbox = 0;
        }
    }

    void DisableObject()
    {
        if (destroyOnTriggerExit)
        {
            Destroy(gameObject);
            Destroy(objectToTurnOn);
        }
        if (colourFadeObject)
        {
            colourFadeRenderer.FadeObject(objectToTurnOn);
            destroying = true;
        }
        else
        {
            objectToTurnOn.SetActive(false);
        }
    }

}
