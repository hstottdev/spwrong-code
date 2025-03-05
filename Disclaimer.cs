using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Disclaimer : MonoBehaviour
{
    [TextArea]
    [SerializeField] List<string> disclaimers;
    [SerializeField] TextMeshProUGUI disclaimerText;
    // Start is called before the first frame update
    void Start()
    {      

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        disclaimerText.text = disclaimers[Random.Range(0, disclaimers.Count)];
    }
}
