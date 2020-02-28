using UnityEngine;

public class PlayerNetworkStart : MonoBehaviour
{
    public static PlayerNetworkStart Instance { get { return _instance; } }
    static PlayerNetworkStart _instance;
   

    public void Start()
    {
        transform.SetParent(NetworkTransformObjects.Instance.PlayerParent);
    }
}
