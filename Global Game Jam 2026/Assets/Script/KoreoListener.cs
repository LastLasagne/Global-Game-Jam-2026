using SonicBloom.Koreo;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KoreoListener : MonoBehaviour
{
    [SerializeField] private string eventID;
    [SerializeField] private UnityEvent<KoreographyEvent> onEventReceived = new();

    void Start()
    {
        Koreographer.Instance.RegisterForEvents(eventID, OnKoreoEvent);
    }

    private void OnKoreoEvent(KoreographyEvent koreoEvent)
    {
        onEventReceived.Invoke(koreoEvent);
    }
}
