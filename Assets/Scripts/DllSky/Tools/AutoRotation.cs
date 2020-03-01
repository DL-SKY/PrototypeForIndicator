using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    #region Variables
    public Vector3 speed = Vector3.zero;
    #endregion

    #region Unity methods
    private void Update()
    {
        transform.localRotation *= Quaternion.Euler(speed * Time.deltaTime);
    }
    #endregion
}
