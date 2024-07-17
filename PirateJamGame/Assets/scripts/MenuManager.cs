using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void HostGame()
    {
        // Load the game scene and set the game mode to host
        GameModeHandler.GameMode = GameMode.Host;
        SceneManager.LoadScene("Game");
    }

    public void JoinGame()
    {
        // Load the game scene and set the game mode to client
        GameModeHandler.GameMode = GameMode.Client;
        SceneManager.LoadScene("Game");
    }
}