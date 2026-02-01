using SonicBloom.Koreo;
using UnityEngine;

public class Cone : MonoBehaviour
{
    [SerializeField] private GameObject coneAngry;
    [SerializeField] private GameObject coneSad;
    [SerializeField] private GameObject coneHappy;
    [SerializeField] private GameManager.Actor actor = GameManager.Actor.None;

    public void OnMaskAction(KoreographyEvent maskEvent)
    {
        if (maskEvent.GetAssetValue() is MaskSO maskActionSO)
        {
            if (maskActionSO.actor != actor)
                return;

            switch (maskActionSO.mask)
            {
                case GameManager.Mask.Angry:
                    coneAngry.SetActive(true);
                    coneSad.SetActive(false);
                    coneHappy.SetActive(false);
                    break;
                case GameManager.Mask.Sad:
                    coneAngry.SetActive(false);
                    coneSad.SetActive(true);
                    coneHappy.SetActive(false);
                    break;
                case GameManager.Mask.Happy:
                    coneAngry.SetActive(false);
                    coneSad.SetActive(false);
                    coneHappy.SetActive(true);
                    break;
            }
        }
    }

    public void OnBeatAction(KoreographyEvent maskEvent)
    {
        coneAngry.SetActive(false);
        coneSad.SetActive(false);
        coneHappy.SetActive(false);
    }
}
