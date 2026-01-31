using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Mask
    {
        Happy,
        Sad,
        Angry
    }

    public enum Actor
    {
        Actor1,
        Actor2
    }

    public SerializedDictionary<Mask, Sprite> sprites = new();

    public Sprite HappyMask;
    public Sprite SadMask;
    public Sprite AngryMask;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
