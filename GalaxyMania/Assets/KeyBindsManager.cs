using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindsManager : MonoBehaviour
{
    public GameObject keyBindsPanel;
    void Start()
    {
        keyBindsPanel.SetActive(false);
    }

    public void ShowKeyBinds()
    {
        keyBindsPanel.SetActive(true);
    }

    public void CloseKeyBinds()
    {
        keyBindsPanel.SetActive(false);
    }
}
