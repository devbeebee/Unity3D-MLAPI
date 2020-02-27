using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkNetworkedObject : MonoBehaviour
{
    [SerializeField]
    GameObject firework;
    public void SpawnObject()
    {
        float rndHeight = Random.Range(40, 125);
        float rndSpeed = Random.Range(5, 20);
        GameObject go = Instantiate(firework, ExtensionMethods.RandomPos(-45, 45), Quaternion.identity,this.transform);
        go.GetComponent<NetworkFirework>().speed = new MLAPI.NetworkedVar.NetworkedVar<float>(rndSpeed);
        go.GetComponent<NetworkFirework>().colorEx = new MLAPI.NetworkedVar.NetworkedVar<Color>(new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F)));

        go.GetComponent<NetworkFirework>().startPosition = new MLAPI.NetworkedVar.NetworkedVar<Vector3>( go.transform.position);
        go.GetComponent<NetworkFirework>().targetPosition = new MLAPI.NetworkedVar.NetworkedVar<Vector3>(go.transform.position + (Vector3.up * rndHeight));


        go.GetComponent<NetworkedObject>().Spawn();
        ChatController.Instance.SendOutMessage($"{PlayerProfile.Instance.Gamer_Profile.GamerTag} Firework||GAMERTAG||<color=#000000FF>Firework launched with a height of {rndHeight} total Firworks : {transform.childCount}</color>");
    }


}
