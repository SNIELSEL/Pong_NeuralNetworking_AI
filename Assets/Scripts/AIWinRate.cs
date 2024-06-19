using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIWinRate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = ($"AI Winrate: {(PlayerPrefs.GetFloat("AIWinsL") / PlayerPrefs.GetFloat("AiGamesPlayed") * 100)}" + "%");
    }
}
