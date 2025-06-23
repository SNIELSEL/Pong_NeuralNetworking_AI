using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DynamicCamera : MonoBehaviour
{
    [Header("Reference Settings")]
    [Tooltip("The base resolution your game is designed for (width x height).")]
    public Vector2 referenceResolution = new Vector2(2560, 1440);

    [Tooltip("Extra space around objects in world units.")]
    public float padding = 2f; // Extra space around objects

    [Header("Sprite Settings")]
    [Tooltip("Pixels per unit setting of your sprites.")]
    public float pixelsPerUnit = 100f; // Set this to match your sprites' PPU

    [Header("Orthographic Size Limits")]
    [Tooltip("Maximum orthographic size to prevent excessive zooming out.")]
    public float maxOrthographicSize = 10f;

    [Tooltip("Minimum orthographic size to prevent excessive zooming in.")]
    public float minOrthographicSize = 5f;

    private Camera cam;
    private Vector2 lastResolution;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true; // Ensure the camera is orthographic
        lastResolution = new Vector2(Screen.width, Screen.height);
        AdjustCamera();
    }

    void Update()
    {
        // Detect resolution changes and adjust the camera accordingly
        if (Screen.width != lastResolution.x || Screen.height != lastResolution.y)
        {
            lastResolution = new Vector2(Screen.width, Screen.height);
            AdjustCamera();
        }
    }

    void AdjustCamera()
    {
        // Calculate target and current aspect ratios
        float targetAspect = referenceResolution.x / referenceResolution.y;
        float windowAspect = (float)Screen.width / Screen.height;

        // Calculate orthographic size based on height
        float orthographicSizeHeight = (referenceResolution.y / pixelsPerUnit) / 2f + padding;

        // Calculate orthographic size based on width
        float orthographicSizeWidth = (referenceResolution.x / pixelsPerUnit) / (2f * windowAspect) + padding;

        // Choose the larger orthographic size to ensure both dimensions fit
        float orthographicSize = Mathf.Max(orthographicSizeHeight, orthographicSizeWidth);

        // Clamp the orthographic size to prevent excessive zooming
        orthographicSize = Mathf.Clamp(orthographicSize, minOrthographicSize, maxOrthographicSize);

        cam.orthographicSize = orthographicSize;

        // Debugging Logs
        Debug.Log($"Reference Resolution: {referenceResolution}");
        Debug.Log($"Screen Resolution: {Screen.width}x{Screen.height}");
        Debug.Log($"Target Aspect: {targetAspect}");
        Debug.Log($"Window Aspect: {windowAspect}");
        Debug.Log($"Orthographic Size (Height-Based): {orthographicSizeHeight}");
        Debug.Log($"Orthographic Size (Width-Based): {orthographicSizeWidth}");
        Debug.Log($"Final Orthographic Size Set To: {orthographicSize}");
    }
}
