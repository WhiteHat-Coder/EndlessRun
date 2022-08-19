using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Cube : MonoBehaviour, IObject
{
    public bool IsActive
    {
        get => gameObject.activeSelf;
        set => gameObject.SetActive(value);
    }

    public float CheckDistance(Vector2 vector2)
    {
        return Vector3.Distance(new Vector3(vector2.x,1.2f,vector2.y), transform.localPosition);
    }
}
