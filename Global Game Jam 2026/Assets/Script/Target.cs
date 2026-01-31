using DG.Tweening;
using SonicBloom.Koreo;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager.Actor actor;
    [SerializeField] private float scale = 0.25f;
    [SerializeField] private float duration = 0.15f;
    private Tween activeTween;

    private void Awake()
    {
    }

    public void OnShot()
    {
        activeTween?.Kill();

        transform
        .DOPunchScale(Vector3.one * scale, duration, vibrato: 8, elasticity: 0.8f);
    }

    public void OnBeat(KoreographyEvent koreoEvent)
    {
        PoseSO pose = (PoseSO)koreoEvent.GetAssetValue();
        transform.position = pose.position;
    }
}
