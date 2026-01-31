using DG.Tweening;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    private Tween activeTween;
    private RectTransform rectTransform;
    [SerializeField] private float scale = 0.25f;
    [SerializeField] private float duration = 0.15f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Scale()
    {
        activeTween?.Kill();

        activeTween = rectTransform
        .DOPunchScale(Vector3.one * scale, duration, vibrato: 8, elasticity: 0.8f);
    }
}
