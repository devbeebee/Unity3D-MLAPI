using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNetworkedObject : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    public void SpawnObject()
    {
        GameObject go = Instantiate(prefab, ExtensionMethods.RandomPos(-15,15), Quaternion.identity);
        ParticleSystem ps = go.transform.GetChild(0).GetComponent<ParticleSystem>();
        ps.startSpeed = Random.Range(20, 40);
        go.GetComponent<NetworkedObject>().Spawn();
    }
}
