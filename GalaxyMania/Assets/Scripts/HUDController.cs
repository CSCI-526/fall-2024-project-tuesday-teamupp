using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public GameObject freezeUI;  // UI group for the freeze power-up (contains both image and text)
    public GameObject shieldUI;  // UI group for the shield power-up (contains both image and text)
    public TextMeshProUGUI angleText;  // UI text element for the current angle display

    private Transform levelParent;  // The parent object of the level that is rotating
    private Transform referencePoint;  // Reference point for angle calculation

    private float shieldTimer = 10f;  // Shield countdown timer
    private bool shieldActive = false;

    private float initialRotationZ;  // Track the initial rotation of the level
    private string currentSceneName;  // Track the active scene
    private bool hasAssignedReferences = false;  // To track if we've assigned references

    private float freezeTimer = 10f;  // Timer for Freeze power-up countdown
    private bool freezeActive = false;  // Flag to check if Freeze is active

    private CanvasGroup freezeCanvasGroup;  // To manage opacity for FreezeUI
    private CanvasGroup shieldCanvasGroup;  // To manage opacity for ShieldUI

    void Start()
    {
        // Get the CanvasGroup components for managing opacity
        freezeCanvasGroup = freezeUI.GetComponent<CanvasGroup>();
        shieldCanvasGroup = shieldUI.GetComponent<CanvasGroup>();

        // Set initial opacity to 40% (0.4)
        SetOpacity(freezeCanvasGroup, 0.4f);
        SetOpacity(shieldCanvasGroup, 0.4f);

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

        if (freezeActive)  // Check if Freeze is active and update the countdown
        {
            UpdateFreezeStatus();  // Call UpdateFreezeStatus to show the countdown
        }
    }

    // Assign levelParent and referencePoint based on the current scene
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

        // Initialize HUD elements (opacity starts at 40%)
        SetOpacity(freezeCanvasGroup, 0.4f);
        SetOpacity(shieldCanvasGroup, 0.4f);

        hasAssignedReferences = true;
        Debug.Log("HUD references successfully assigned for " + currentSceneName);
    }

    // Update the current angle display
    void UpdateCurrentAngle()
    {
        if (levelParent != null)
        {
            float currentRotationZ = levelParent.rotation.eulerAngles.z;
            if (currentRotationZ > 180f) currentRotationZ -= 360f;
            angleText.text = "Angle: " + Mathf.RoundToInt(currentRotationZ) + "°";  // Append the degree symbol
        }
    }

    // Update shield status and timer
    void UpdateShieldStatus()
    {
        shieldTimer -= Time.deltaTime;
        if (shieldTimer <= 0)
        {
            shieldActive = false;
            StartCoroutine(FadeOpacity(shieldCanvasGroup, 0.4f));  // Fade back to 40% opacity
            var shieldText = shieldUI.GetComponentInChildren<TextMeshProUGUI>();
            shieldText.text = "Shield";  // Change text back to "Shield"
        }
        else
        {
            var shieldText = shieldUI.GetComponentInChildren<TextMeshProUGUI>();
            shieldText.text = "Shield: " + Mathf.Ceil(shieldTimer).ToString() + "s";
        }
    }

    // Collect Freeze Power-Up
    public void CollectFreeze()
    {
        var freezeText = freezeUI.GetComponentInChildren<TextMeshProUGUI>();
        freezeText.text = "Freeze";
        StartCoroutine(FadeOpacity(freezeCanvasGroup, 1f));
    }

    // Coroutine to handle the color change of AngleText for the duration of the Freeze power-up
    IEnumerator HandleFreezeEffect()
    {
        // Change AngleText color to green
        angleText.color = Color.green;

        // Wait for 10 seconds (effect duration)
        yield return new WaitForSeconds(10f);

        // Revert AngleText color to white
        angleText.color = Color.white;
    }

    // Use Freeze Power-Up (called from PlayerDiamondCollision)
    public void UseFreezePowerUp()
    {
        freezeTimer = 10f;  // Reset the freeze timer
        freezeActive = true;  // Mark Freeze as active

        StartCoroutine(HandleFreezeEffect());
    }

    // Helper function to set initial opacity
    void SetOpacity(CanvasGroup canvasGroup, float opacity)
    {
        canvasGroup.alpha = opacity;
    }

    // Coroutine to fade opacity from current value to the target value
    IEnumerator FadeOpacity(CanvasGroup canvasGroup, float targetOpacity)
    {
        float startOpacity = canvasGroup.alpha;
        float elapsedTime = 0f;
        float fadeDuration = 1f;  // Duration for fading the shield back

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startOpacity, targetOpacity, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetOpacity;
    }

    // Coroutine to fade opacity back to 40% after 10 seconds
    IEnumerator FadeBackAfterTime(CanvasGroup canvasGroup, TextMeshProUGUI text, string defaultText, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        text.text = defaultText;  // Reset text after timer
        StartCoroutine(FadeOpacity(canvasGroup, 0.4f));  // Fade back to 40% opacity
    }

    // Update freeze status and countdown timer
    void UpdateFreezeStatus()  // Update Freeze countdown
    {
        freezeTimer -= Time.deltaTime;
        if (freezeTimer <= 0)
        {
            freezeActive = false;
            StartCoroutine(FadeOpacity(freezeCanvasGroup, 0.4f));  // Fade back to 40% opacity

            var freezeText = freezeUI.GetComponentInChildren<TextMeshProUGUI>();
            freezeText.text = "Freeze";  // Change text back to "Freeze"
        }
        else
        {
            var freezeText = freezeUI.GetComponentInChildren<TextMeshProUGUI>();
            freezeText.text = "Freeze: " + Mathf.Ceil(freezeTimer).ToString() + "s";  // Display countdown
        }
    }

    // Activate Shield Power-Up
    public void ActivateShield()
    {
        shieldActive = true;
        shieldTimer = 10f;
        var shieldText = shieldUI.GetComponentInChildren<TextMeshProUGUI>();
        shieldText.text = "Shield: " + shieldTimer + "s";
        SetOpacity(shieldCanvasGroup, 1.0f);  // Set full opacity

        // After 10 seconds, fade back to 40% opacity
        StartCoroutine(FadeBackAfterTime(shieldCanvasGroup, shieldText, "Shield", 10f));
    }
}
