using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    public float pulseSpeed = 0.5f; 
    public float minScale = 0.8f; 
    public float maxScale = 1.2f; 
    private Vector3 initialScale;
    private bool isPulsing = true;

    void Start()
    {
        initialScale = transform.localScale; 
    }

    void Update()
    {
        if (isPulsing)
        {
            float scale = minScale + Mathf.PingPong(Time.time * pulseSpeed, maxScale - minScale);
            transform.localScale = initialScale * scale; 
        }
    }

    public void TogglePulse(bool pulse)
    {
        isPulsing = pulse;
        if (!pulse)
        {
            transform.localScale = initialScale; 
        }
    }
}
