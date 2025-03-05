using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rescale : MonoBehaviour
{
    public float t;
    [SerializeField] float initial_t;
    float old_t;
    [HideInInspector]public bool grow;
    [HideInInspector] public Vector3 targetScale;
    public Vector3 biggest;
    public Vector3 smallest;
    Vector3 current;

    public enum startingScale { biggest, smallest, current };

    [Header("Starting")]
    public startingScale startAt;

    [Header("Stopping")]
    public bool stop;
    private bool stopScaling;
    public startingScale stopAt;
    public bool disableOnStop;

    // Start is called before the first frame update
    void Start()
    {
        current = transform.localScale;
        old_t = t;    
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopScaling)
        {
            if (grow)
            {
                targetScale = biggest;
            }
            else
            {
                targetScale = smallest;
            }
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, t * Time.deltaTime);
            if (transform.localScale == targetScale)
            {
                grow = !grow;
                t = old_t;
                if (targetScale == biggest && stop && stopAt == startingScale.biggest)
                {
                    StopScaling();
                }
                if(targetScale == smallest && stop && stopAt == startingScale.smallest)
                {
                    StopScaling();
                }
            }
        }        
    }

    void StopScaling()
    {
        stopScaling = true;
        if (disableOnStop)
        {
            gameObject.SetActive(false);
        }
    }

    public void reset()
    {
        if (startAt == startingScale.biggest)
        {
            grow = false;
            transform.localScale = biggest;
        }
        else if(startAt == startingScale.smallest)
        {
            grow = true;
            transform.localScale = smallest;
        }
        else
        {
            grow = (transform.localScale.x < smallest.x);
            transform.localScale = current;
        }
        stopScaling = false;
        if (initial_t != 0)
        {
            t = initial_t;
        }
    }
    private void OnDisable()
    {
        reset();
    }

    public void setupForHiding(bool disableOnHide = true)
    {
        if (startAt == startingScale.smallest)
        {
            startAt = startingScale.biggest;
            stopAt = startingScale.smallest;
            disableOnStop = disableOnHide;
            reset();
        }
    }

    public void setupForShowing()
    {
        if (startAt == startingScale.biggest)
        {
            startAt = startingScale.smallest;
            stopAt = startingScale.biggest;
            disableOnStop = false;
            reset();
        }
    }
}
