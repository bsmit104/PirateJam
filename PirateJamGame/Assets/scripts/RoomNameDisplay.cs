using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomNameDisplayTMP : MonoBehaviour
{
    public TMP_Text roomNameText;

    private void Start()
    {
        // Ensure the room name text is assigned
        if (roomNameText != null)
        {
            roomNameText.text = "Room: " + GameModeHandler.RoomName;
        }
    }
}