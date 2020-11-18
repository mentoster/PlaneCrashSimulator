using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public void LeanForwardNBack(float n)
    {
        transform.eulerAngles = new Vector3(n, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void LeanRightNLeft(float n)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, n);
    }
}
