using DG.Tweening;
using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;

public class Shooting : MonoBehaviour
{
    private GameManager.Mask mask;
    private GameManager.Actor actor = GameManager.Actor.None;

    public UnityEvent onShoot = new UnityEvent();
    public UnityEvent onSuccess = new UnityEvent();
    public UnityEvent onFailure = new UnityEvent();

    private List<RaycastResult> raycastResults = new();
    private PointerEventData pointerData;

    private float timeSinceLastShot = 0f;

    [SerializeField] private MaskSelection maskSelection;
    [SerializeField] private Camera cam;

    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private Muzzle muzzle;
    [SerializeField] private VisualEffect muzzleFlashHappy;
    [SerializeField] private VisualEffect muzzleFlashSad;
    [SerializeField] private VisualEffect muzzleFlashAngry;
    [SerializeField] private float minRotation = -45f;
    [SerializeField] private float maxRotation = 30;

    [SerializeField] private Transform impactParent;
    [SerializeField] private VisualEffect impactHappy;
    [SerializeField] private VisualEffect impactSad;
    [SerializeField] private VisualEffect impactAngry;

    [SerializeField] private RectTransform gunTransform;
    [SerializeField] private float scale = 0.25f;
    [SerializeField] private float duration = 0.15f;
    private Tween activeTween;

    private void Awake()
    {
        onShoot.AddListener(Shoot);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onShoot.Invoke();
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }

    void Shoot()
    {
        pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        bool success = Evaluate();

        if (success) onSuccess.Invoke();
        else onFailure.Invoke();

        timeSinceLastShot = 0f;
    }

    public void OnPose(KoreographyEvent koreoEvent)
    {
        MaskSO pose = (MaskSO) koreoEvent.GetAssetValue();

        mask = pose.mask;
        actor = pose.actor;

        if (timeSinceLastShot < 0.5f)
        {
            Evaluate();
        }
    }

    private bool Evaluate()
    {
        var tempMask = mask;
        var tempActor = actor;
        mask = GameManager.Mask.None;
        actor = GameManager.Actor.None;

        Vector3 muzzleWorldPos = muzzle.GetWorldMuzzlePosition();
        muzzleTransform.position = muzzleWorldPos;
        Vector2 mouseViewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float yRot = Mathf.Lerp(minRotation, maxRotation, mouseViewport.x);
        Quaternion targetRot = Quaternion.Euler(0, yRot, 0);
        muzzleTransform.rotation = targetRot;

        switch (maskSelection.selectedMask)
        {
            case GameManager.Mask.Happy:
                muzzleFlashHappy.Play();
                break;
            case GameManager.Mask.Sad:
                muzzleFlashSad.Play();
                break;
            case GameManager.Mask.Angry:
                muzzleFlashAngry.Play();
                break;
        }

        activeTween?.Kill();
        gunTransform
        .DOPunchScale(Vector3.one * scale, duration, vibrato: 8, elasticity: 0.8f);

        if (!CheckMask(tempMask))
            return false;
        if (!CheckAim(tempActor)) 
            return false;

        switch(maskSelection.selectedMask)
        {
            case GameManager.Mask.Happy:
                impactHappy.Play();
                break;
            case GameManager.Mask.Sad:
                impactSad.Play();
                break;
            case GameManager.Mask.Angry:
                impactAngry.Play();
                break;
        }

        return true;
    }

    private bool CheckMask(GameManager.Mask tempMask)
    {
        return maskSelection.selectedMask == tempMask;
    }

    private bool CheckAim(GameManager.Actor tempActor)
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
        {
            if (hit.collider.transform.parent.TryGetComponent(out Target target))
            {
                if (target.actor == tempActor)
                {
                    target.OnShot();
                    impactParent.position = hit.point;
                    return true;
                }
            }
        }

        //Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        //RaycastHit2D hit = Physics2D.Raycast(
        //    mouseWorldPos,
        //    Vector2.zero,
        //    0f
        //);

        //if (hit.collider != null)
        //{
        //    if (hit.collider.TryGetComponent(out Target target))
        //    {
        //        if (target.actor == tempActor);
        //        {
        //            target.OnShot();
        //            return true;
        //        }
        //    }
        //}

        return false;
    }

}
