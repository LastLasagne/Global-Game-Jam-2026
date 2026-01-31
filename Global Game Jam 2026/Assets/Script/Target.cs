using SonicBloom.Koreo;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager.Actor actor;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();    
    }

    public void OnShot()
    {
        // Add logic for when the target is shot
        Debug.Log("Target has been shot!");
        Destroy(gameObject);
    }

    public void OnPose(KoreographyEvent koreoEvent)
    {
        PoseSO pose = (PoseSO) koreoEvent.GetAssetValue();
        if (pose.actor == actor)
        {
            rectTransform.position = pose.position;
        }
    }
}
