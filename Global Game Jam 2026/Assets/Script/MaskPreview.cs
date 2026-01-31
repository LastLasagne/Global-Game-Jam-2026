using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class MaskPreview : MonoBehaviour
{
    [SerializeField] private Koreography koreography;
    [SerializeField] private string eventID;
    [SerializeField] private GameManager.Actor actor;

    private float timePerSample = 28514f;

    private List<KoreographyEvent> nextEvents = new();
    private List<KoreographyEvent> events = new();

    private Image image1;
    private Image image2;
    private Image image3;

    void Awake()
    {
        events = koreography.GetTrackByID(eventID).GetAllEvents();
        events.Reverse();

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

        if (events.Count > 0)
        {

            KoreographyEvent evt = events[events.Count - 1];

            if (evt.StartSample <= currentSample + timePerSample * 2 + 500)
            {
                if (((MaskSO)evt.GetAssetValue()).actor == actor)
                {
                    nextEvents[0] = evt;
                    events.RemoveAt(events.Count - 1);
                }
            }
        }

        UpdateMasks();
    }

    void UpdateMasks()
    {
        image1.sprite = nextEvents[0] != null ? GameManager.Instance.sprites[((MaskSO)nextEvents[0].GetAssetValue()).mask] : null;
        image2.sprite = nextEvents[1] != null ? GameManager.Instance.sprites[((MaskSO)nextEvents[1].GetAssetValue()).mask] : null;
        image3.sprite = nextEvents[2] != null ? GameManager.Instance.sprites[((MaskSO)nextEvents[2].GetAssetValue()).mask] : null;
    }
}
