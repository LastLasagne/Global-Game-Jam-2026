using UnityEngine;

public class resetScale : MonoBehaviour
{
    public void ResetScale()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
