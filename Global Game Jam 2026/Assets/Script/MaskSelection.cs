using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

public class MaskSelection : MonoBehaviour
{
    [SerializedDictionary("MaskType", "RectTransform")]
    public SerializedDictionary<GameManager.Mask, RectTransform> maskDict;

    [SerializeField] private Image gunAmmo;
    [SerializeField] private Image gunAmmoCrosshair;

    public GameManager.Mask selectedMask;

    private void Awake()
    {
        SelectMask(GameManager.Mask.Happy);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectMask(GameManager.Mask.Happy);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SelectMask(GameManager.Mask.Sad);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SelectMask(GameManager.Mask.Angry);
        }
    }

    private void SelectMask(GameManager.Mask mask)
    {
        maskDict[selectedMask].localScale = Vector3.one;
        selectedMask = mask;
        maskDict[selectedMask].localScale = Vector3.one * 1.33f;
        gunAmmo.sprite = GameManager.Instance.maskAssets[selectedMask].maskSprite;
        gunAmmoCrosshair.sprite = GameManager.Instance.maskAssets[selectedMask].maskSprite;
    }
}
