using SonicBloom.Koreo;
using System.Collections.Generic;
using UnityEngine;

public class PreviewManager : MonoBehaviour
{
    [SerializeField] private Koreography koreography;
    public List<KoreographyEvent> events = new();
    [SerializeField] private string eventID;

    void Awake()
    {
        events = koreography.GetTrackByID(eventID).GetAllEvents();
        events.Reverse();
    }
}