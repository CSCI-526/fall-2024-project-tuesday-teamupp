using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public GameObject freezeUI;  
    public GameObject shieldUI;  
    public TextMeshProUGUI angleText;  

    private Transform levelParent; 
    private Transform referencePoint; 

    private float shieldTimer = 10f; 
    private bool shieldActive = false;

    private float initialRotationZ; 
    private string currentSceneName;  
    private bool hasAssignedReferences = false;  

    private float freezeTimer = 10f;  
    private bool freezeActive = false;  

    private CanvasGroup freezeCanvasGroup;  
    private CanvasGroup shieldCanvasGroup;  

    void Start()
    {
        freezeCanvasGroup = freezeUI.GetComponent<CanvasGroup>();
        shieldCanvasGroup = shieldUI.GetComponent<CanvasGroup>();

        SetInactiveAppearance(freezeCanvasGroup, freezeUI);
        SetInactiveAppearance(shieldCanvasGroup, shieldUI);


        currentSceneName = SceneManager.GetActiveScene().name;
        AssignReferences();
    }

    void Update()
    {
        if (!hasAssignedReferences || SceneManager.GetActiveScene().name != currentSceneName)
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            AssignReferences();
        }

        UpdateCurrentAngle();

        if (shieldActive)
        {
            UpdateShieldStatus();
        }

        if (freezeActive)  
        {
            UpdateFreezeStatus();  
        }
    }

    void AssignReferences()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Assigning references for: " + currentSceneName);

        levelParent = GameObject.Find(currentSceneName)?.transform;
        referencePoint = GameObject.FindWithTag("ReferencePoint")?.transform;

        if (levelParent == null || referencePoint == null)
        {
            Debug.LogError("Level Parent or Reference Point not found in scene: " + currentSceneName);
            return;
        }

        initialRotationZ = levelParent.rotation.eulerAngles.z;

        SetOpacity(freezeCanvasGroup, 0.4f);
        SetOpacity(shieldCanvasGroup, 0.4f);

        hasAssignedReferences = true;
        Debug.Log("HUD references successfully assigned for " + currentSceneName);
    }

    void UpdateCurrentAngle()
    {
        if (levelParent != null)
        {
            float currentRotationZ = levelParent.rotation.eulerAngles.z;
            if (currentRotationZ > 180f) currentRotationZ -= 360f;
            angleText.text = "Angle: " + Mathf.RoundToInt(currentRotationZ) + "\u00B0"; 
        }
    }

    void UpdateShieldStatus()
    {
        shieldTimer -= Time.deltaTime;
        if (shieldTimer <= 0)
        {
            shieldActive = false;
            StartCoroutine(FadeOpacity(shieldCanvasGroup, 0.4f));  
            var shieldText = shieldUI.GetComponentInChildren<TextMeshProUGUI>();
            shieldText.text = "Shield";  
        }
        else
        {
            var shieldText = shieldUI.GetComponentInChildren<TextMeshProUGUI>();
            shieldText.text = "Shield: " + Mathf.Ceil(shieldTimer).ToString() + "s";
        }
    }

    public void CollectFreeze()
    {
        var freezeText = freezeUI.GetComponentInChildren<TextMeshProUGUI>();
        freezeText.text = "Freeze (E)";
        StartCoroutine(FadeOpacity(freezeCanvasGroup, 1f));

        var freezeIcon = freezeUI.GetComponentInChildren<UnityEngine.UI.Image>();
        if (freezeIcon != null)
        {
            freezeIcon.color = Color.green;  
            Debug.Log("Freeze power-up collected: Icon color set to green.");
        }
        else
        {
            Debug.LogError("Freeze icon image component not found!");
        }
    }

    IEnumerator HandleFreezeEffect()
    {
        angleText.color = Color.green;

        yield return new WaitForSeconds(10f);

        angleText.color = Color.white;
    }

    public void UseFreezePowerUp()
    {
        freezeTimer = 10f;  
        freezeActive = true;  

        var iconImage = freezeUI.GetComponentInChildren<UnityEngine.UI.Image>();
        if (iconImage != null)
        {
            iconImage.color = Color.green;  
        }

        StartCoroutine(HandleFreezeEffect());
    }

    void SetOpacity(CanvasGroup canvasGroup, float opacity)
    {
        canvasGroup.alpha = opacity;
    }

    void SetInactiveAppearance(CanvasGroup canvasGroup, GameObject uiElement)
    {
        SetOpacity(canvasGroup, 0.1f);
        var iconImage = uiElement.GetComponentInChildren<UnityEngine.UI.Image>();
        if (iconImage != null)
        {
            iconImage.color = Color.grey;
        }
    }

    IEnumerator FadeOpacity(CanvasGroup canvasGroup, float targetOpacity)
    {
        float startOpacity = canvasGroup.alpha;
        float elapsedTime = 0f;
        float fadeDuration = 1f;  

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startOpacity, targetOpacity, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetOpacity;
    }

    
    IEnumerator FadeBackAfterTime(CanvasGroup canvasGroup, TextMeshProUGUI text, string defaultText, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        text.text = defaultText;  
        StartCoroutine(FadeOpacity(canvasGroup, 0.4f));  
    }

    void UpdateFreezeStatus()  
    {
        freezeTimer -= Time.deltaTime;
        if (freezeTimer <= 0)
        {
            freezeActive = false;
            StartCoroutine(FadeOpacity(freezeCanvasGroup, 0.4f));  

            var freezeText = freezeUI.GetComponentInChildren<TextMeshProUGUI>();
            freezeText.text = "Freeze (E)";  

            var freezeIcon = freezeUI.GetComponentInChildren<UnityEngine.UI.Image>();
            if (freezeIcon != null)
            {
                freezeIcon.color = Color.grey;
            }
            
        }
        else
        {
            var freezeText = freezeUI.GetComponentInChildren<TextMeshProUGUI>();
            freezeText.text = "Freeze: " + Mathf.Ceil(freezeTimer).ToString() + "s";  
        }
    }

    public void ActivateShield()
    {
        shieldActive = true;
        shieldTimer = 10f;
        var shieldText = shieldUI.GetComponentInChildren<TextMeshProUGUI>();
        shieldText.text = "Shield: " + shieldTimer + "s";
        SetOpacity(shieldCanvasGroup, 1.0f);  

        var iconImage = shieldUI.GetComponentInChildren<UnityEngine.UI.Image>();
        if (iconImage != null)
        {
            iconImage.color = Color.blue;  
        }

        StartCoroutine(FadeBackAfterTime(shieldCanvasGroup, shieldText, "Shield", 10f));
    }
}
