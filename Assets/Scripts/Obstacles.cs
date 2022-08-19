using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacles : MonoBehaviour, IObstacle
{
    
    #region SerializeField

    [SerializeField] private CreateCubes _createCubes;
    [SerializeField] private float offsetZInit;
    [SerializeField] private List<GameObject> _clonedCubeObjects = new List<GameObject>();

    #endregion

    public void SetUp()
    {
        clearOldCubes();
        var zValue = offsetZInit;
        for (int i = 0; i < 10; i++)
        {
            zValue += i+ 1.5f;
            var pos = validatePosition(getRandomFloatX(), zValue);
            _createCubes.Create(pos.x, pos.y, handleClonedObject);
        }
    }

    //Checking for Duplicate Positions
    private Vector2 validatePosition(float x, float y)
    {
        Vector2 vector2 = new Vector2(x, y);
        if (_clonedCubeObjects.Count == 0)
        {
            return vector2;
        }

        foreach (var cube in _clonedCubeObjects)
        {
            var cubeScript = cube.GetComponent<Cube>();
            if ((cubeScript.CheckDistance(vector2) * 0.5f) <= 0 ||  (cube.transform.localPosition.x == x && cube.transform.localPosition.y == y))
            {
                var newX = getRandomFloatX();
                vector2 = validatePosition(newX, y);
            }
        }

        return vector2;
    }

    private void handleClonedObject(GameObject obj)
    {
        _clonedCubeObjects.Add(obj);
    }

    private float getRandomFloatX()
    {
        var value = Random.Range(0, 3);
        switch (value)
        {
            case 0:
                return -1.5f;
                break;
            case 1:
                return 0f;
                break;
            case 2:
                return 1.5f;
                break;
        }

        return 0f;
    }

    private void clearOldCubes()
    {
        if (_clonedCubeObjects.Count <= 0) return;
        _clonedCubeObjects.ForEach(Destroy);
        _clonedCubeObjects.Clear();
    }
}
