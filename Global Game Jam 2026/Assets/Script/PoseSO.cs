using UnityEngine;

[CreateAssetMenu(fileName = "PoseSO", menuName = "Scriptable Objects/PoseSO")]
public class PoseSO : ScriptableObject
{
    public GameManager.Actor actor;
    public GameManager.Mask mask;
    public Vector2 position;
}
