using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameDisplay : MonoBehaviour
{
    public TMP_Text playerNameText;

    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }
}