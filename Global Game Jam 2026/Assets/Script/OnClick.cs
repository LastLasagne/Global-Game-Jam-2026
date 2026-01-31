using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour
{
    [Header("Tween Settings")]
    [SerializeField] private float scale = 0.25f;
    [SerializeField] private float duration = 0.15f;

    private RectTransform rectTransform;
    private Tween activeTween;
    private Shooting shooting;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        shooting = GetComponentInParent<Shooting>();
        shooting.onShoot.AddListener(OnShoot);
    }

    private void OnShoot()
    {
        activeTween?.Kill();

        activeTween = rectTransform
        .DOPunchScale(Vector3.one * scale, duration, vibrato: 8, elasticity: 0.8f);
    }
}
