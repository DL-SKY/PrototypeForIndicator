using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    public void OnSelfDisable()
    {
        gameObject.SetActive(false);
    }
}
