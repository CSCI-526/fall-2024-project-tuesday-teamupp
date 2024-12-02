using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class Send2Google : MonoBehaviour
{
    private string selectedAnswer1;
    private string selectedAnswer2;
    private long sessionID;
    private string test_url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfdYAsohFb9clbZX-PXhy8QWj1zRPOXxT3RQ_YdAkrOrDWgCA/formResponse";
    public float timer;
    public int numOfJump = 0;

    void Start()
    {
        timer = 0f;
        numOfJump = 0;
    }
    
    private void Awake()
    {
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

    public void SendPU(string power)
    {
        StartCoroutine(PostPU(sessionID.ToString(), power));
    }

    public void SendBulletData(bool isHitByBullet, string level)
    {
        StartCoroutine(PostDeathLevel34(sessionID.ToString(), isHitByBullet, level));
    }


    public void SendCompleteLevelData(float timer, int numOfJump, string levelname, int NumOfPlatform)
    {
        StartCoroutine(PostCompleteLevelData(sessionID.ToString(), timer.ToString(), numOfJump.ToString(), levelname, NumOfPlatform.ToString()));
    }

    private IEnumerator PostShield(string sessionID, bool shield)
    {
        WWWForm form = new WWWForm();

        Debug.Log($"Sending data - SessionID: {sessionID}, Collected shield in Level 3: {shield}");
        form.AddField("entry.751077088", sessionID);  
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
        form.AddField("entry.751077088", sessionID);  
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

    private IEnumerator PostPU(string sessionID, string power)
    {
        WWWForm form = new WWWForm();

        Debug.Log($"Sending data - SessionID: {sessionID}, Power-Ups collected in Level 4: {power}");
        form.AddField("entry.751077088", sessionID);  
        form.AddField("entry.1660044848", power);
        
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
        form.AddField("entry.751077088", sessionID);  
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

    private IEnumerator PostDeathLevel34(string sessionID, bool isHitByBullet, string level)
    {
        WWWForm form = new WWWForm();

        form.AddField("entry.751077088", sessionID);  
        if (isHitByBullet)
        {
            if (level == "Level 3")
            {
                form.AddField("entry.468887209", "Player died from bullet");
            }
            else
            {
                form.AddField("entry.1791812891", "Player died from bullet");
            }
        }
        else
        {
            if (level == "Level 3")
            {
                form.AddField("entry.468887209", "Player died naturally");
            }
            else
            {
                form.AddField("entry.1791812891", "Player died naturally");
            }
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
        form.AddField("entry.751077088", sessionID);  
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
        form.AddField("entry.751077088", sessionID);  
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
    
    void Update()
    {
        timer += Time.deltaTime;
    }
}