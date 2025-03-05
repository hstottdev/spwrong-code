using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerController activeControllable;
    public List<swappable> startSwappables;
    /// <summary>
    ///<strong>shorthand for 'instance'</strong>
    /// </summary>
    public static GameManager inst;
    public static bool paused;
    public static float playerTime;

    [Header("UI")]
    public TextMeshProUGUI currentObjectText;
    public List<TextMeshProUGUI> correctText;
    public GameObject correctUI;
    public GameObject failedUI;
    public GameObject swapPrompt;
    public GameObject pauseMenu;
    [SerializeField] AudioSource music;
    public GameObject endScreen;

    // Start is called before the first frame update

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        RandomSwap();
        swapPrompt.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        swapPrompt.SetActive(PlayerController.currentlyTouching != null);
        pauseMenu.SetActive(paused);
        music.mute = paused;

        if (!endScreen.activeInHierarchy)
        {
            playerTime += Time.deltaTime;
        }
    }


    void RandomSwap()
    {
        swappable toSwapWith = startSwappables[Random.Range(0, startSwappables.Count)];

        activeControllable._swappable.SwapWith(toSwapWith);
    }

    public static bool hasInstance()
    {
        return inst != null;
    }
}
