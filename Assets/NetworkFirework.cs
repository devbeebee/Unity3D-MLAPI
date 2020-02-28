using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkFirework : NetworkedBehaviour
{
    [SerializeField]
    Transform Trails;
    [SerializeField]
    ParticleSystem Explosion;

    public NetworkedVar<Vector3> startPosition;
    public NetworkedVar<Vector3> targetPosition;
    public NetworkedVar<float> speed;
    public NetworkedVar<int> burstCount;
    public NetworkedVar<Color> colorEx;
    private System.Random rnd = new System.Random();
    // Start is called before the first frame update

    void Start()
    {
        Explosion.Stop();
    }
    ParticleSystem.PlaybackState playbackState;
    // Update is called once per frame
    void Update()
    {
        float step = speed.Value * Time.deltaTime; // calculate distance to move
        Trails.position = Vector3.MoveTowards(Trails.position, targetPosition.Value, step);
        if (Vector3.Distance(Trails.position, targetPosition.Value) < 0.001f)
        {
            Explosion.startColor = colorEx.Value;
            Explosion.transform.position = Trails.position;
            Trails.position = startPosition.Value;
            burstCount = new NetworkedVar<int>(Random.Range(15, 250));
            Explosion.Emit(burstCount.Value);
        }
    }
}
