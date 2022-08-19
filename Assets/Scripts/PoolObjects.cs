using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

public class PoolObjects : Singleton<PoolObjects>, IPoolObjects
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private GameObject _groundGameObject;
    [SerializeField] private Camera _mainCameraGameObject;
    [SerializeField] private Vector3 _mainPosition;

    private List<IGround> Grounds = new List<IGround>();

    private IGround _initialGround;

    private void Start()
    {
        SpawnController();
    }

    public GameObject SpawnPlayer()
    {
        var player = Instantiate(_playerGameObject, _initialGround.InitialPlayerPosition, Quaternion.identity);
        _mainCameraGameObject.transform.parent = player.transform;
        _mainCameraGameObject.transform.localPosition = _mainPosition;
        return player;
    }

    public void SpawnController()
    {
        var controller = Instantiate(_gameController,Vector3.zero, Quaternion.identity);
        controller.SetUp();
    }

    public void SpawnGround()
    {
        for (int i = 0; i < 5; i++)
        {
            var groundObj = Instantiate(_groundGameObject, transform);
            groundObj.transform.position =new Vector3(0,100,-10000);
            groundObj.transform.rotation = Quaternion.identity;
            var ground = groundObj.GetComponent<IGround>();
            ground.IsActive = false;
            Grounds.Add(ground);
        }

        //setup initialGround
        _initialGround = Grounds.FirstOrDefault();
        _initialGround.SetupInitialGround();
    }

    public void SetUpGround(Vector3 position)
    {
        var ground = Grounds.FirstOrDefault(x => !x.IsActive);
        if (ground != null)
        {
            ground.SetUpPosition(position, true);
        }
    }
}