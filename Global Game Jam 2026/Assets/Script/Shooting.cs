using SonicBloom.Koreo;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private float timeOfLastShot = 0f;
    private float timeOfLastPose = 0f;
    float timingThreshold = 0.2f;

    private Vector2 aimPos;

    private int mask;
    private Mask equippedMask;

    public enum Mask
    {
        Happy,
        Sad,
        Angry
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //check if last pose change was closer than some time ago
        timeOfLastShot = Time.time;
        aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnPose(KoreographyEvent koreoEvent)
    {
        //check if last shot was closer than some time ago
        timeOfLastPose = Time.time;
        mask = koreoEvent.GetIntValue();
    }

    private void Evaluate()
    {
        if (!CheckTiming()) return;
        if (!CheckMask()) return;
        if (!CheckAim()) return;

        //success
    }

    private bool CheckTiming()
    {
        float timeDifference = Mathf.Abs(timeOfLastShot - timeOfLastPose);
        return timeDifference <= timingThreshold;
    }

    private bool CheckMask()
    {
        return ((int)equippedMask) == mask;
    }

    private bool CheckAim()
    {
        return true;
    }

}
