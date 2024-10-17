using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Send2Google : MonoBehaviour
{
    private string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdYDoB93-Cv6HGU2ZeAenc5O_3o5X4RIr8HE4qULU35LM1whQ/formResponse";
    private long sessionID;
    private bool getsavelevel2;
    private bool getsavelevel3;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        // Assign sessionID to identify playtests
        sessionID = DateTime.Now.Ticks;
    }


    public void Send(bool getsavelevel2, bool getsavelevel3)
    {
        StartCoroutine(Post(sessionID.ToString(), getsavelevel2.ToString(), getsavelevel3.ToString()));
    }


    private IEnumerator Post(string sessionID, string getsavelevel2, string getsavelevel3)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.995316009", sessionID);
        form.AddField("entry.973479010", getsavelevel2);
        form.AddField("entry.1047782149", getsavelevel3);

        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
