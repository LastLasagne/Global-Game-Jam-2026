using SonicBloom.Koreo;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public GameManager.Actor actor;
    private RectTransform rectTransform;
    private Image image;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnShot()
    {
        // Add logic for when the target is shot
        Debug.Log("Target has been shot!");
        //Destroy(gameObject);
    }

    public void OnPose(KoreographyEvent koreoEvent)
    {
    }

    public void OnBeat(KoreographyEvent koreoEvent)
    {
        PoseSO mask = (PoseSO)koreoEvent.GetAssetValue();
        image.sprite = mask.sprite;
        image.SetNativeSize();
        image.rectTransform.position += mask.position;
    }
}
