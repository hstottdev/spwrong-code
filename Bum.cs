﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bum : MonoBehaviour
{
    [SerializeField] Transform bumhole;
    [SerializeField] GameObject poo;
    [SerializeField] float maxSpawnInterval = 10;
    [SerializeField] float minSpawnInterval = 0;
    [SerializeField] float pooForce = 3;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("UnleashTurd", GetRandomInterval());
    }

/*    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣶⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢴⣄⡀⠀⠀⠀⠀⠀⠀⠀⢈⣧⣷⡤⠄⠀⠀⠀⠀⠀⠀⢠⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⡿⠛⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣿⣄⠀⠀⠀⠀⠀⠀⠀⠀⣽⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⣷⡀⠀⠀⠀⠀⠀⠀⠀
⠰⠤⣶⡷⢢⡀⠀⠀⠀⢸⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢈⣿⡅⠀⠀⠀⢀⣴⡿⠟⠛⠋⠀⠀⠀⠀⢀⣄⠀⠀⠀⠀⢹⡇⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠉⠉⠁⠀⠀⠀⠈⣿⣧⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⡿⠋⠀⠀⠀⠀⢹⣿⠁⠀⠀⠀⠀⠀⠀⠀⢸⣿⠀⠀⠀⢀⣼⡇⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡄⠀⠀⠀⠀⠀⠘⣿⣆⠀⠀⠀⠀⠀⠀⣴⣿⠏⠀⠀⣰⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⠘⣿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⢿⡄⠂⠀⠀⠀⠀⢈⣿⡇⠀⠀⠀⠀⠸⣿⡀⠀⠀⠸⣿⠤⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⣀⠞⠀⠀⠀⠀⠸⣿⠀⠀⠀⠀⠀⠀⠀⠑⣄⡀⠀⠀⠀⠀⢻⠀⠀⠀⠀⢴⡟⠋⠀⠀⠀⠀⠀⠀⠹⣿⣜⠁⠀⠙⠷⣄⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠈⣿⠀⠀⠀⠀⠀⣸⡟⠀⠀⠀⠀⠀⠀⠀⠀⢠⣉⡀⠀⠐⠒⠚⠁⠠⠄⠀⢸⣦⠄⠀⠀⠐⠀⠀⠀⠀⠈⢿⡆⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⢠⡿⠀⠀⠀⢀⣼⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⠉⣿⠀⠀⠀⠀⠀⠀⠀⠀⠈⢿⡄⠀⠀⠀⠀⠀⠀⢀⣴⡟⠁⠀⠀⠀⠀⠀⢀⣀⣀⣦⠀⠀⡄
⠀⠀⠀⠀⢀⡟⠁⠀⠀⠀⢸⠃⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⠏⠀⢿⡄⠀⠀⠀⠀⠀⠀⠀⠀⣸⠇⠀⠀⠀⠀⠀⣴⡟⠁⠀⠀⠀⠀⠀⠀⠀⠘⠶⠖⠛⠈⠉⠀
⠀⠀⠀⠀⢸⠀⠀⠀⠀⠀⣹⠀⠀⠀⠀⠀⠀⢀⣤⡾⠟⠁⠀⠀⠈⠻⢶⣤⣀⣀⠀⠀⠀⠊⠁⠀⠀⠀⠀⠀⠀⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠸⡀⠀⠀⠀⠀⠸⡇⠀⠀⠀⠀⢀⣿⠋⠀⠀⠀⠀⢄⣰⠲⡤⡀⠌⠛⢷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠁⠀⠀⠀⠀⠀⠉⠀⠀⠀⠀⢸⡏⠐⡂⠐⠀⠀⠀⠈⠙⠒⠲⠤⠤⠾⠟⠛⢻⢷⣦⣀⠀⠀⠀⠀⠀⠀⠈⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣷⡀⠃⠀⠀⡄⠀⠀⠀⠀⢤⠂⠀⠐⠀⢧⡈⠀⢯⡻⣷⡀⠀⠀⠀⠀⠀⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣽⣷⣄⠀⠀⠃⠳⠸⡄⠄⠀⠀⠀⠀⠀⢀⣀⢀⠀⣅⢸⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⡾⠟⠉⠀⣨⠙⠛⠶⠤⣀⣀⠀⠀⠀⠀⠰⠤⢣⢸⡘⣌⡆⠘⠀⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡿⣫⡀⠀⢠⠖⡇⠀⠀⡀⠀⠀⠀⠀⠀⢀⣀⠀⠀⠠⠤⣅⣈⣠⣴⢾⣿⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⠑⣿⣡⠤⡏⣠⢯⠀⠊⠁⡀⠀⠀⠀⠀⠈⠉⡟⠛⠐⠰⠀⠀⠇⢷⡀⠟⠛⠻⣶⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣼⡵⠃⢰⠃⢀⡞⢠⠀⠀⠇⠀⠀⠀⠀⣰⠶⠃⠀⠀⠀⠀⠀⠀⠀⠹⣄⠀⢄⠲⠙⢿⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⣠⣶⡿⠟⠉⠀⠀⠁⠀⠈⠀⠈⠀⢠⡶⣆⠀⠀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡆⠸⣤⡘⠈⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⣠⣾⠟⣥⢰⠆⢠⣄⠀⠀⠀⢀⡀⠀⠀⠘⠁⠙⠀⠀⠁⠀⠀⠀⠀⠀⢀⠀⢤⠘⡆⠸⣄⢳⠀⣇⣹⣶⣿⣧⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⣰⣿⠃⠰⠃⠚⠀⠈⠁⠽⠄⠀⠀⠉⠙⠒⠦⠤⣄⣀⣀⣀⣀⣀⣀⣀⢀⣘⣂⣘⣃⣷⣤⣽⡾⠿⠟⠛⣉⢈⠙⣿⣧⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⢀⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠀⠀⠀⠀⠀⠀⠈⣍⣉⢉⠉⠛⠛⠛⠛⠋⠉⠉⠉⠀⠀⠀⠀⠀⠹⡌⠂⠁⢻⣷⡀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⢸⣿⠀⢷⣆⡀⠀⠀⠀⠀⠀⠀⠀⠀⠉⢠⠀⠀⠀⠀⠀⠀⠈⠛⠎⠃⠀⠀⠀⠀⢠⠀⣄⠀⡦⠀⢰⠀⠀⡀⢀⠰⡶⡄⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠸⣿⡀⠘⢸⢹⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⠄⠀⠀⠀⠀⢠⡀⣤⢰⡆⣸⠀⣿⠀⠷⠀⠈⡀⣌⢿⠈⡇⠇⠁⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠻⣷⡄⠘⠼⢸⠁⡇⢰⠀⡄⠀⠀⠀⡴⠀⠀⠰⠃⠀⠀⠀⠀⠀⠀⠟⠓⠺⠃⢙⠀⢁⡀⣤⢇⠀⢣⢸⡸⡆⠟⢀⣼⡿⠁⡴⠀⣄⠀⠀⠀⠀⠀
⠀⠀⠀⠀⢀⣠⣿⣿⣄⠀⡄⣤⡀⠈⡄⢀⡀⠀⠀⠃⠸⠰⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⡇⠘⠂⠛⠸⠄⠘⠋⠁⣠⣤⣾⠟⢡⠞⢁⡴⠋⢰⡄⠀⠀⠀
⠀⠀⠀⠶⣫⠵⠋⣩⢿⣷⣝⣁⡇⢀⠇⢸⠀⡆⢠⡀⢰⠀⡀⠀⠀⠀⢠⡀⡄⣦⠠⡄⠒⠒⠀⠀⠀⠀⠀⢀⣠⣤⣶⡿⢟⡿⢁⡴⠋⣠⠞⠁⣠⠞⠁⠀⠀⠀
⠀⠀⠐⣋⣡⠖⠋⠁⣠⠼⠛⠿⢿⣶⣶⣬⣄⣃⣈⣀⣈⣀⠀⠀⠀⠀⠀⢀⣈⣙⣀⣀⣀⣤⣤⣶⣶⣾⠿⠿⢛⡿⠁⡴⠋⣠⠎⢀⡾⠃⢀⣴⠃⠀⠴⠀⠀⠀
⠀⠀⠰⠋⠀⢠⠴⠋⠁⠀⣀⡴⠋⠁⣨⠟⠉⣻⠿⠛⣻⠟⠛⣿⠿⠟⢻⠟⠛⢛⡿⠛⣻⠟⠉⠀⣰⠃⢀⡼⠋⠀⠘⠁⠼⠃⠠⠏⠀⠀⠘⠁⠀⠀⠀⠀⠀*/

    float GetRandomInterval()
    {
        return Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void UnleashTurd()
    {
        GameObject turd = Instantiate(poo, bumhole.position,poo.transform.rotation);
        turd.GetComponent<Rigidbody2D>().AddForce(Vector2.left * pooForce, ForceMode2D.Impulse);
        Invoke("UnleashTurd", GetRandomInterval());
    }
}
