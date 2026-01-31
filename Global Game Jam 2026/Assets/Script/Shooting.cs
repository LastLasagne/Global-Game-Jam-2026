using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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

        if (!CheckMask(tempMask)) 
            return false;
        if (!CheckAim(tempActor)) 
            return false;

        return true;
    }

    private bool CheckMask(GameManager.Mask tempMask)
    {
        return maskSelection.selectedMask == tempMask;
    }

    private bool CheckAim(GameManager.Actor tempActor)
    {
        Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(
            mouseWorldPos,
            Vector2.zero,
            0f
        );

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Target target))
            {
                if (target.actor == tempActor);
                {
                    target.OnShot();
                    return true;
                }
            }
        }

        return false;
    }

}
