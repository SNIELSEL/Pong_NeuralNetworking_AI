using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellectButton : MonoBehaviour
{
    public EventSystem eventSystem;

    public void SelectButton(Button buttonToSelect)
    {
        eventSystem.SetSelectedGameObject(buttonToSelect.gameObject);
    }
}
