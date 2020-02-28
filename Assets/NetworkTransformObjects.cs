using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTransformObjects : MonoBehaviour
{
    public static NetworkTransformObjects Instance { get { return _instance; } }
    static NetworkTransformObjects _instance;

    public Transform PlayerParent;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
