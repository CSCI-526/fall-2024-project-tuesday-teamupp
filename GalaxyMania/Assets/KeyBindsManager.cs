using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindsManager : MonoBehaviour
{
    public GameObject keyBindsPanel;
    // Start is called before the first frame update
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
