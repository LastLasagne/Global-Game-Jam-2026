using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour
{
    private float timeOfLastShot = 0f;
    private float timeOfLastPose = 0f;
    float timingThreshold = 0.2f;

    private float reloadTime = 0.25f;
    private float timeSinceLastShot = 0.0f;

    private GameManager.Mask mask;
    private GameManager.Actor actor = 0;
    private GameManager.Mask equippedMask = GameManager.Mask.Happy;

    public UnityEvent onShoot = new UnityEvent();
    public UnityEvent onSuccess = new UnityEvent();
    public UnityEvent onFailure = new UnityEvent();

    private List<RaycastResult> raycastResults = new();
    private PointerEventData pointerData;

    private void Awake()
    {
        timeSinceLastShot = reloadTime;
        onShoot.AddListener(Shoot);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (timeSinceLastShot >= reloadTime)
            {
                onShoot.Invoke();
                timeSinceLastShot = 0.0f;
            }
         }
        
        timeSinceLastShot += Time.deltaTime;
    }

    void Shoot()
    {
        pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        timeOfLastShot = Time.time;

        bool success = Evaluate();

        if (success) onSuccess.Invoke();
        else onFailure.Invoke();
    }

    public void OnPose(KoreographyEvent koreoEvent)
    {
        timeOfLastPose = Time.time;
        MaskSO pose = (MaskSO) koreoEvent.GetAssetValue();

        mask = pose.mask;
        actor = pose.actor;
    }

    private bool Evaluate()
    {
        if (!CheckTiming()) return false;
        if (!CheckMask()) return false;
        if (!CheckAim()) return false;

        return true;
    }

    private bool CheckTiming()
    {
        float timeDifference = Mathf.Abs(timeOfLastShot - timeOfLastPose);
        return timeDifference <= timingThreshold;
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
