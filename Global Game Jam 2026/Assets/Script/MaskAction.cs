using DG.Tweening;
using SonicBloom.Koreo;
using UnityEngine;

public class MaskAction : MonoBehaviour
{
    private Tween activeTween;
    private RectTransform rectTransform;
    [SerializeField] private float scale = 0.25f;
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private GameManager.Actor actor = GameManager.Actor.None;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnMaskAction(KoreographyEvent maskEvent)
    {
        if (maskEvent.GetAssetValue() is MaskSO maskActionSO)
        {
            if (maskActionSO.actor != actor) return;
        }
        else
        {
            return;
        }

        activeTween?.Kill();

        activeTween = rectTransform
        .DOPunchScale(Vector3.one * scale, duration, vibrato: 8, elasticity: 0.8f);
    }
}
