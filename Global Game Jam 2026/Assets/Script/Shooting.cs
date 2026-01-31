using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour
{
    private GameManager.Mask mask;
    private GameManager.Actor actor = GameManager.Actor.None;
    private GameManager.Mask equippedMask = GameManager.Mask.Happy;

    public UnityEvent onShoot = new UnityEvent();
    public UnityEvent onSuccess = new UnityEvent();
    public UnityEvent onFailure = new UnityEvent();

    private List<RaycastResult> raycastResults = new();
    private PointerEventData pointerData;

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

        mask = GameManager.Mask.None;
        actor = GameManager.Actor.None;
    }

    public void OnPose(KoreographyEvent koreoEvent)
    {
        MaskSO pose = (MaskSO) koreoEvent.GetAssetValue();

        mask = pose.mask;
        actor = pose.actor;
    }

    private bool Evaluate()
    {
        if (!CheckMask()) 
            return false;
        if (!CheckAim()) 
            return false;

        return true;
    }

    private bool CheckMask()
    {
        return true;
        //return equippedMask == mask;
    }

    private bool CheckAim()
    {
        raycastResults.Clear(); 
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        foreach (var result in raycastResults)
        {
            if (result.gameObject.TryGetComponent(out Target target))
            {
                if (target.actor != actor) continue;
                target.OnShot();
                return true;
            }
        }

        return false;
    }

}
