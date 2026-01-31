using UnityEngine;

[CreateAssetMenu(fileName = "PoseSO", menuName = "Scriptable Objects/PoseSO")]
public class PoseSO : ScriptableObject
{
    public Shooting.Actor actor;
    public Shooting.Mask mask;
    public Vector2 position;
}
