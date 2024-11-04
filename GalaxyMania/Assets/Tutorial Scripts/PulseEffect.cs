using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    public float pulseSpeed = 0.5f; // Speed of pulsing
    public float minScale = 0.8f; // Minimum scale factor
    public float maxScale = 1.2f; // Maximum scale factor

    private Vector3 initialScale;
    private bool isPulsing = true;

    void Start()
    {
        initialScale = transform.localScale; // Save the object's original scale
    }

    void Update()
    {
        if (isPulsing)
        {
            float scale = minScale + Mathf.PingPong(Time.time * pulseSpeed, maxScale - minScale);
            transform.localScale = initialScale * scale; // Apply pulsing effect
        }
    }

    // You can call this method to start or stop pulsing
    public void TogglePulse(bool pulse)
    {
        isPulsing = pulse;
        if (!pulse)
        {
            transform.localScale = initialScale; // Reset scale when pulsing stops
        }
    }
}
