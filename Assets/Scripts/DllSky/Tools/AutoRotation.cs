using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    public float speed = -25.0f;

    private void Update()
    {
        transform.localEulerAngles += new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
    }
}
