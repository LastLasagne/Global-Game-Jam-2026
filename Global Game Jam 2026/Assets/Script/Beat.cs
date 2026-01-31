using SonicBloom.Koreo;
using System;
using UnityEngine;

public class Beat : MonoBehaviour
{
    public void OnBeat(KoreographyEvent koreoEvent)
    {
        Debug.Log("Beat received at time: " + Time.time);
    }
}
