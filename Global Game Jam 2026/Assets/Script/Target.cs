using DG.Tweening;
using SonicBloom.Koreo;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager.Actor actor;
    private RectTransform rectTransform;
    private SpriteRenderer image;
    [SerializeField] private float scale = 0.25f;
    [SerializeField] private float duration = 0.15f;
    private Tween activeTween;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<SpriteRenderer>();
    }

    public void OnShot()
    {
        activeTween?.Kill();

        rectTransform
        .DOPunchScale(Vector3.one * scale, duration, vibrato: 8, elasticity: 0.8f);
    }

    public void OnPose(KoreographyEvent koreoEvent)
    {
    }

    public void OnBeat(KoreographyEvent koreoEvent)
    {
        PoseSO mask = (PoseSO)koreoEvent.GetAssetValue();
        image.sprite = mask.sprite;
        image.transform.position = mask.position;
    }
}
