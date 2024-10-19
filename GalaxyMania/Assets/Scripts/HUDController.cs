using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public GameObject freezeIcon;  // UI element for the freeze power-up
    public GameObject shieldIcon;  // UI element for the shield power-up
    public TextMeshProUGUI freezeText;  // Text display for Freeze status
    public TextMeshProUGUI shieldText;  // Text display for Shield status
    public TextMeshProUGUI angleText;  // UI text element for the current angle display

    private Transform levelParent;  // The parent object of the level that is rotating
    private Transform referencePoint;  // Reference point for angle calculation

    private float shieldTimer = 10f;  // Shield countdown timer
    private bool shieldActive = false;
    private bool freezeCollected = false;  // Track Freeze power-up status

    private float initialRotationZ;  // Track the initial rotation of the level
    private string currentSceneName;  // Track the active scene
    private bool hasAssignedReferences = false;  // To track if we've assigned references

    void Start()
    {
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

        // Initialize HUD elements
        SetIconState(freezeIcon, freezeCollected);
        SetIconState(shieldIcon, shieldActive);
        freezeText.text = freezeCollected ? "Active" : "Inactive";
        shieldText.text = shieldActive ? "Active" : "Inactive";

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
            angleText.text = "Angle: " + Mathf.RoundToInt(currentRotationZ);
        }
    }

    // Update shield status and timer
    void UpdateShieldStatus()
    {
        shieldTimer -= Time.deltaTime;
        if (shieldTimer <= 0)
        {
            shieldActive = false;
            shieldText.text = "Inactive";
            SetIconState(shieldIcon, false);
        }
        else
        {
            shieldText.text = "Active: " + Mathf.Ceil(shieldTimer).ToString() + "s";
        }
    }

    // Change icon state (active/inactive)
    void SetIconState(GameObject icon, bool isActive)
    {
        icon.GetComponent<UnityEngine.UI.Image>().color = isActive ? Color.blue : Color.gray;
    }

    public void CollectFreeze()
    {
        freezeCollected = true;
        freezeText.text = "Active";
        SetIconState(freezeIcon, true);
    }

    public void ActivateShield()
    {
        shieldActive = true;
        shieldTimer = 10f;
        SetIconState(shieldIcon, true);
        shieldText.text = "Active: " + shieldTimer + "s";
    }
}
