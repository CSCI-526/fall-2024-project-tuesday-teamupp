using System.Collections;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public Transform levelParent;
    public Transform antiRotatPlatforms;
    public static bool rotationPaused = false;
    private float currentRotation = 0f;
    private float targetRotation = 0f;

    void FixedUpdate()
    {
        if (!rotationPaused)
        {
            RotateLevel();
        }
    }

    private void RotateLevel()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        targetRotation += -horizontalInput * rotationSpeed * Time.deltaTime;
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * 5f);

        // Apply rotation to the level parent
        levelParent.rotation = Quaternion.Euler(0f, 0f, currentRotation);

        if (antiRotatPlatforms)
        {
            // Apply the opposite rotation to the AntiRotatPlatforms
            foreach (Transform platform in antiRotatPlatforms)
            {
                platform.Rotate(Vector3.forward * rotationSpeed * horizontalInput * Time.deltaTime);
            }
        }
    }

    public IEnumerator PauseRotationForSeconds(float duration)
    {
        rotationPaused = true;
        yield return new WaitForSeconds(duration);
        rotationPaused = false;
    }
}