using UnityEngine;

public class GunMouseFollow : MonoBehaviour
{
    [Header("Follow")]
    [SerializeField] private float maxOffset = 200f;      // pixels
    [SerializeField] private float followSpeed = 8f;    
    [SerializeField] private Vector2 verticalBias = new Vector2(1f, 0.5f);

    [Header("Rotation")]
    [SerializeField] private float maxYRotation = 8f;   // degrees
    [SerializeField] private float rotationSpeed = 10f;

    private RectTransform rect;
    private Vector2 startAnchoredPos;
    private Quaternion startRotation;


    void Awake()
    {
        rect = GetComponent<RectTransform>();
        startAnchoredPos = rect.anchoredPosition;
        startRotation = rect.localRotation;
    }

    void Update()
    {
        Vector2 mouseViewport =
            Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // Convert [0..1] → [-1..1]
        Vector2 offset = (mouseViewport - Vector2.one * 0.5f) * 2f;

        offset.x *= verticalBias.x;
        offset.y *= verticalBias.y;

        offset = Vector2.ClampMagnitude(offset, 1f);

        Vector2 targetPos = startAnchoredPos + offset * maxOffset;

        rect.anchoredPosition = Vector2.Lerp(
            rect.anchoredPosition,
            targetPos,
            Time.deltaTime * followSpeed
        );

        float yRot = startRotation.eulerAngles.y + offset.x * maxYRotation;

        Quaternion targetRot =
            Quaternion.Euler(startRotation.eulerAngles.x,
                             yRot,
                             startRotation.eulerAngles.z);

        rect.localRotation = Quaternion.Lerp(
            rect.localRotation,
            targetRot,
            Time.deltaTime * rotationSpeed);
    }
}
