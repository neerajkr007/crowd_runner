using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject test;
    public void changeScene1()
    {
        SceneManager.LoadScene(1);
    }
    public void changeScene2()
    {
        SceneManager.LoadScene(2);
    }
}
