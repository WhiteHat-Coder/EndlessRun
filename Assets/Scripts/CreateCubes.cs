using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateCubes : MonoBehaviour, ICreateCubes
{
    [SerializeField] private GameObject objRedPrefab;
    [SerializeField] private GameObject objGreenPrefab;

    public void Create(float x, float y, Action<GameObject> clonedObjet)
    {
        var obj = Instantiate(Random.Range(0, 10) < 5 ? objRedPrefab : objGreenPrefab, transform);
        obj.transform.localPosition = new Vector3(x, 1.2f, y);
        clonedObjet?.Invoke(obj);
    }
}