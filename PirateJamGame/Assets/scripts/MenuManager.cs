using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Add this for TextMeshPro

public class MenuManager : MonoBehaviour
{
    public TMP_InputField roomNameInputField;  // Change to TMP_InputField
    public TMP_InputField playerNameInputField;  // Change to TMP_InputField

    private string roomName;
    private string playerName;

    private void Start()
    {
        // Assign default values if needed
        roomName = GenerateRandomNumber();
        playerName = GenerateRandomNumber();

        // Add listeners for the input fields
        roomNameInputField.onValueChanged.AddListener(OnRoomNameChanged);
        playerNameInputField.onValueChanged.AddListener(OnPlayerNameChanged);
    }

    private string GenerateRandomNumber()
    {
        return Random.Range(1000, 9999).ToString();
    }

    public void OnRoomNameChanged(string newRoomName)
    {
        roomName = string.IsNullOrEmpty(newRoomName) ? GenerateRandomNumber() : newRoomName;
    }

    public void OnPlayerNameChanged(string newPlayerName)
    {
        playerName = string.IsNullOrEmpty(newPlayerName) ? GenerateRandomNumber() : newPlayerName;
    }

    public void HostGame()
    {
        // Check if input fields are empty and assign default values if necessary
        roomName = string.IsNullOrEmpty(roomNameInputField.text) ? GenerateRandomNumber() : roomNameInputField.text;
        playerName = string.IsNullOrEmpty(playerNameInputField.text) ? GenerateRandomNumber() : playerNameInputField.text;

        GameModeHandler.GameMode = GameMode.Host;
        GameModeHandler.RoomName = roomName;
        GameModeHandler.PlayerName = playerName;
        SceneManager.LoadScene("Game");
    }

    public void JoinGame()
    {
        // Check if input fields are empty and assign default values if necessary
        roomName = string.IsNullOrEmpty(roomNameInputField.text) ? GenerateRandomNumber() : roomNameInputField.text;
        playerName = string.IsNullOrEmpty(playerNameInputField.text) ? GenerateRandomNumber() : playerNameInputField.text;

        GameModeHandler.GameMode = GameMode.Client;
        GameModeHandler.RoomName = roomName;
        GameModeHandler.PlayerName = playerName;
        SceneManager.LoadScene("Game");
    }
}
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class MenuManager : MonoBehaviour
// {
//     public void HostGame()
//     {
//         // Load the game scene and set the game mode to host
//         GameModeHandler.GameMode = GameMode.Host;
//         SceneManager.LoadScene("Game");
//     }

//     public void JoinGame()
//     {
//         // Load the game scene and set the game mode to client
//         GameModeHandler.GameMode = GameMode.Client;
//         SceneManager.LoadScene("Game");
//     }
// }