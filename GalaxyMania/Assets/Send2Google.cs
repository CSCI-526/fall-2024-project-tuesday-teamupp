using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Send2Google : MonoBehaviour
{
    //private string URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSfTin5vQazJ86KlDQDvbGMqzUP0SRNv2yty84a0xrzvYMbEMA/formResponse";
    private string selectedAnswer1;
    private string selectedAnswer2;
    private long sessionID;
    private string test_url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfdYAsohFb9clbZX-PXhy8QWj1zRPOXxT3RQ_YdAkrOrDWgCA/formResponse";
    //private string portalfinder;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        // Assign sessionID to identify playtests
        sessionID = DateTime.Now.Ticks;
    }

    //public void Send(string selectedAnswer1, string selectedAnswer2)
    //{
    //    if (!string.IsNullOrEmpty(selectedAnswer1))
    //    {
    //        StartCoroutine(PostLevel1(sessionID.ToString(), selectedAnswer1));
    //    }
    //    if (!string.IsNullOrEmpty(selectedAnswer2))
    //    {
    //        StartCoroutine(PostLevel2(sessionID.ToString(), selectedAnswer2));
    //    }
    //}

    public void SendTest(string selectedAnswer1, string selectedAnswer2)
    {
        if (!string.IsNullOrEmpty(selectedAnswer1))
        {
            StartCoroutine(PostTestLevel1(sessionID.ToString(), selectedAnswer1));
        }
        if (!string.IsNullOrEmpty(selectedAnswer2))
        {
            StartCoroutine(PostTestLevel2(sessionID.ToString(), selectedAnswer2));
        }
    }

    private IEnumerator PostTestLevel1(string sessionID, string selectedAnswer1)
    {
        WWWForm form = new WWWForm();

        Debug.Log($"Sending data - SessionID: {sessionID}, Answer: {selectedAnswer1}");
        form.AddField("entry.751077088", sessionID);  // For session ID
        form.AddField("entry.1165852626", selectedAnswer1);

        using (UnityWebRequest www = UnityWebRequest.Post(test_url, form))
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

    private IEnumerator PostTestLevel2(string sessionID, string selectedAnswer2)
    {
        WWWForm form = new WWWForm();

        Debug.Log($"Sending data - SessionID: {sessionID}, Answer: {selectedAnswer2}");
        form.AddField("entry.751077088", sessionID);  // For session ID
        form.AddField("entry.1285688663", selectedAnswer2);

        using (UnityWebRequest www = UnityWebRequest.Post(test_url, form))
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
    //public void Send(bool getsavelevel2, bool getsavelevel3)
    //{
    //    StartCoroutine(Post(sessionID.ToString(), getsavelevel2.ToString(), getsavelevel3.ToString()));
    //}


    //private IEnumerator PostLevel1(string sessionID, string selectedAnswer1)
    //{
    //    WWWForm form = new WWWForm();

    //    Debug.Log($"Sending data - SessionID: {sessionID}, Answer: {selectedAnswer1}");
    //    form.AddField("entry.995215545", sessionID);  // For session ID
    //    form.AddField("entry.402189590", selectedAnswer1); 
    
    //    using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
    //    {
    //        yield return www.SendWebRequest();

    //        if (www.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(www.error);
    //        }
    //        else
    //        {
    //            Debug.Log("Form upload complete!");
    //        }
    //    }
    //}

    //private IEnumerator PostLevel2(string sessionID, string selectedAnswer2)
    //{
    //    WWWForm form = new WWWForm();

    //    Debug.Log($"Sending data - SessionID: {sessionID}, Answer: {selectedAnswer2}");
    //    form.AddField("entry.995215545", sessionID);  // For session ID
    //    form.AddField("entry.2136440181", selectedAnswer2); 

    //    using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
    //    {
    //        yield return www.SendWebRequest();

    //        if (www.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(www.error);
    //        }
    //        else
    //        {
    //            Debug.Log("Form upload complete!");
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {

    }
}