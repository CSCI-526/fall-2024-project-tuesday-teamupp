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
    public float timer;
    public int numOfJump = 0;
    //private string portalfinder;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        numOfJump = 0;
    }
    private void Awake()
    {
        // Assign sessionID to identify playtests
        sessionID = DateTime.Now.Ticks;
    }

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

    public void SendShield(bool shield)
    {
        StartCoroutine(PostShield(sessionID.ToString(), shield));
    }

    public void SendFreeze(string freeze)
    {
        StartCoroutine(PostFreeze(sessionID.ToString(), freeze));
    }

    public void SendBulletData(bool isHitByBullet)
    {
        StartCoroutine(PostDeathLevel3(sessionID.ToString(), isHitByBullet));
    }


    public void SendCompleteLevelData(float timer, int numOfJump, string levelname, int NumOfPlatform)
    {
        StartCoroutine(PostCompleteLevelData(sessionID.ToString(), timer.ToString(), numOfJump.ToString(), levelname, NumOfPlatform.ToString()));
    }

    private IEnumerator PostShield(string sessionID, bool shield)
    {
        WWWForm form = new WWWForm();

        Debug.Log($"Sending data - SessionID: {sessionID}, Collected shield in Level 3: {shield}");
        form.AddField("entry.751077088", sessionID);  // For session ID
        if (shield)
        {
            form.AddField("entry.554968727", "Yes");
        }
        else
        {
            form.AddField("entry.554968727", "No");
        }
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

    private IEnumerator PostFreeze(string sessionID, string freeze)
    {
        WWWForm form = new WWWForm();

        Debug.Log($"Sending data - SessionID: {sessionID}, Collected freeze in Level 2: {freeze}");
        form.AddField("entry.751077088", sessionID);  // For session ID
        form.AddField("entry.209725520", freeze);
        
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

    private IEnumerator PostCompleteLevelData(string sessionID, string timer, string numOfJump, string levelname, string NumOfPlatform)
    {
        WWWForm form = new WWWForm();

        Debug.Log($"Sending data - SessionID: {sessionID}, time: {timer}, #OfJump: {numOfJump}, levelname: {levelname}, #ofPlatform: {NumOfPlatform}");
        form.AddField("entry.751077088", sessionID);  // For session ID
        form.AddField("entry.343903498", timer);
        form.AddField("entry.1120816225", numOfJump);
        form.AddField("entry.1916354037", levelname);
        form.AddField("entry.672358194", NumOfPlatform);

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

    private IEnumerator PostDeathLevel3(string sessionID, bool isHitByBullet)
    {
        WWWForm form = new WWWForm();

        //Debug.Log($"Sending data - SessionID: {sessionID}, Answer: {selectedAnswer1}");
        form.AddField("entry.751077088", sessionID);  // For session ID
        if (isHitByBullet)
        {
            form.AddField("entry.468887209", "Player died from bullet");
        }
        else
        {
            form.AddField("entry.468887209", "Player died naturally");
        }

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
    
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}