using AYellowpaper.SerializedCollections;
using UnityEngine;

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

    [SerializedDictionary("MaskType", "Sprite")]
    public SerializedDictionary<Mask, Sprite> sprites;

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
