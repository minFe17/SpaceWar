using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    
    public void ClickButton()
    {
        SceneManager.LoadScene("Main");
    }
}