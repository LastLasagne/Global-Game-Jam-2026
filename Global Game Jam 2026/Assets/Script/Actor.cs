using SonicBloom.Koreo;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private SpriteRenderer image;
    [SerializeField] private GameManager.Actor actor = GameManager.Actor.None;

    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
    }

    public void OnPose(KoreographyEvent koreoEvent)
    {
        PoseSO pose = (PoseSO)koreoEvent.GetAssetValue();
        image.sprite = pose.sprite;
    }

    public void OnMask(KoreographyEvent koreoEvent)
    {
        MaskSO mask = (MaskSO)koreoEvent.GetAssetValue();

        if (mask.actor == actor)
            transform.position = new Vector3(GameManager.Instance.positions[mask.mask].x, transform.position.y, transform.position.z);
    }
}
