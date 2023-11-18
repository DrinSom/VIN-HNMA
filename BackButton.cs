using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void Back_Button()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
