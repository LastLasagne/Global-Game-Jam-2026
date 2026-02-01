using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [SerializeField] private RectTransform muzzleUI;
    [SerializeField] private Camera uiCamera;     // null if Screen Space Overlay
    [SerializeField] private Camera worldCamera;
    [SerializeField] private float depthFromCamera = 5f;

    public Vector3 GetWorldMuzzlePosition()
    {
        Vector3 screenPos =
            RectTransformUtility.WorldToScreenPoint(uiCamera, muzzleUI.position);

        screenPos.z = depthFromCamera;

        return worldCamera.ScreenToWorldPoint(screenPos);
    }
}
