using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods 
{

    public static Vector3 RandomPos(float min,float max)
    {
        return new Vector3(Random.Range(min, max), 1, Random.Range(min, max));
    }

    public static Quaternion PointOnCircle2D(float p, int steps)
    {
        float degrees = 360f / steps;
        return Quaternion.Euler(0f, 0f, p * -degrees);
    }
    public static RaycastHit FireRayForHit(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit;
        }
        return hit;
    }    
}
