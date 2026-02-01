using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class MaskPreview : MonoBehaviour
{

    [SerializeField] private List<VisualEffect> previews;

    [SerializeField] private GameManager.Actor actor = GameManager.Actor.None;
    private PreviewManager previewManager;

    private float timePerSample = 28514f;

    private List<KoreographyEvent> nextEvents = new();

    private Image image1;
    private Image image2;
    private Image image3;

    void Awake()
    {
        previewManager = GetComponentInParent<PreviewManager>();

        nextEvents.Add(null);
        nextEvents.Add(null);
        nextEvents.Add(null);

        image1 = transform.GetChild(0).GetComponent<Image>();
        image2 = transform.GetChild(1).GetComponent<Image>();
        image3 = transform.GetChild(2).GetComponent<Image>();
    }

    public void OnBeat(KoreographyEvent beatEvent)
    {
        int currentSample = beatEvent.StartSample;
        Debug.Log("Current Sample: " + currentSample);

        nextEvents[2] = nextEvents[1];
        nextEvents[1] = nextEvents[0];
        nextEvents[0] = null;

        if (previewManager.events.Count > 0)
        {
            KoreographyEvent evt = previewManager.events[previewManager.events.Count - 1];

            if (evt.StartSample <= currentSample + timePerSample * 2 + 500)
            {
                if (((MaskSO)evt.GetAssetValue()).actor == actor)
                {
                    nextEvents[0] = evt;
                    previewManager.events.RemoveAt(previewManager.events.Count - 1);
                }
            }
        }

        UpdateMasks();
    }

    void UpdateMasks()
    {
        if (nextEvents[0] != null)
        {
            image1.sprite = GameManager.Instance.maskAssets[((MaskSO)nextEvents[0].GetAssetValue()).mask].smallLight;
            image1.GetComponent<ScaleTween>().Scale();
        }
        else
        {
            image1.sprite = GameManager.Instance.maskAssets[GameManager.Mask.None].smallLight;
        }

        if (nextEvents[1] != null)
        {
            GameManager.Mask maskPreview = ((MaskSO)nextEvents[1].GetAssetValue()).mask;
            image2.sprite = GameManager.Instance.maskAssets[maskPreview].smallLight;
            image2.GetComponent<ScaleTween>().Scale();
            if (maskPreview != GameManager.Mask.None)
            {
                previews[(int)maskPreview - 1].Play();
            }
        }
        else
        {
            image2.sprite = GameManager.Instance.maskAssets[GameManager.Mask.None].smallLight;
        }

        if (nextEvents[2] != null)
        {
            image3.sprite = GameManager.Instance.maskAssets[((MaskSO)nextEvents[2].GetAssetValue()).mask].bigLight;
        }
        else
        {
            image3.sprite = GameManager.Instance.maskAssets[GameManager.Mask.None].smallLight;
        }
    }
}
