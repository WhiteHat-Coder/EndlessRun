using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Ground : MonoBehaviour, IGround
{
    #region SerializeField
    [SerializeField] private GameObject initialPosGameObject;
    [SerializeField] private Collider SpawnGroundCollider;
    [SerializeField] private Collider EndGroundCollider;
    [SerializeField] private Obstacles obstacles;
    [SerializeField] private Vector3 _initialPosition;
    #endregion
    
    public event Action OnGroundCreated;
    
    public bool IsActive
    {
        get => gameObject.activeSelf;
        set
        {
            gameObject.SetActive(value);
            if (value)
            {
                groundActive();
            }
        } 
    }
    
    private bool _isInitialGround;
    
    public void SetupInitialGround()
    {
        Position = Vector3.zero;
        IsActive = true;
        initialPosGameObject.SetActive(true);
        _isInitialGround = true;
    }

    private void groundActive()
    {
        SpawnGroundCollider.enabled = true;
        EndGroundCollider.enabled = true;
        SpawnGroundCollider.tag = Constants.SpawnCollider;
        EndGroundCollider.tag = Constants.EndCollider;
        OnGroundCreated?.Invoke();

        if (!_isInitialGround) return;
        _isInitialGround = false;
        initialPosGameObject.SetActive(false);
    }



    public Vector3 InitialPlayerPosition =>_initialPosition;

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void Awake()
    {
        bind();
    }

    private void bind()
    {
        unbind();
        OnGroundCreated += handleOnGroundCreated;
    }

    private void unbind()
    {
        OnGroundCreated -= handleOnGroundCreated;
    }

    private void handleOnGroundCreated()
    {
        obstacles.SetUp();
    }

    public void SetUpPosition(Vector3 position, bool activeState)
    {
        IsActive = activeState;
        transform.position = position;
    }

    public void ReachedEndPosition()
    {
        StartCoroutine(reachedEnd());
    }

    private IEnumerator reachedEnd()
    {
        yield return new WaitForSeconds(Constants.WaitTimeForGroundDisable);
        IsActive = false;
    }

    private void OnDestroy()
    {
        unbind();
    }
}
