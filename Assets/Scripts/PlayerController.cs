using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    
    #region SerializeField

    [SerializeField] private Positions playerPosState;
    [SerializeField] private float Zpos;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float speed;
    [SerializeField] private float smoothMoveTimeDeley;

    #endregion

    public event Action<int> OnCollectedGreenCube;
    public event Action OnCollectedRedCube;

    private Rigidbody rb;
    private Vector3 fp;
    private Vector3 lp;
    private float dragDistance;
    private bool isLeft;
    private bool isRight;
    private Vector3 targetPos;

    public void SetUp()
    {
        playerPosState = Positions.Middle;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(GameController.Instance._gameState == GameState.Play)
        {
            move();
        }
    }

    private void usingKeys()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (playerPosState == Positions.Middle)
            {
                playerPosState = Positions.Left;
            }
            else if (playerPosState == Positions.Right)
            {
                playerPosState = Positions.Middle;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (playerPosState == Positions.Middle)
            {
                playerPosState = Positions.Right;
            }
            else if (playerPosState == Positions.Left)
            {
                playerPosState = Positions.Middle;
            }
        }
    }

    private void move()
    {
        Vector3 velocity = new Vector3(0f, 0f, speed);
        rb.velocity = velocity;

        swipeController();
        usingKeys();
        if (playerPosState == Positions.Middle)
        {
            targetPos = new Vector3(0f, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos,
                smoothMoveTimeDeley * Time.deltaTime);
            return;
        }

        if (playerPosState == Positions.Left)
        {
            targetPos = new Vector3(minX, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos,
                smoothMoveTimeDeley * Time.deltaTime);
            return;
        }

        if (playerPosState == Positions.Right)
        {
            targetPos = new Vector3(maxX, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos,
                smoothMoveTimeDeley * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.collider.CompareTag(Constants.SpawnCollider))
        {
            target.collider.enabled = false;
            target.collider.tag = Constants.UnTagged;
            Vector3 position = target.transform.parent.transform.position;
            position = new Vector3(position.x, position.y, position.z + Zpos);
            PoolObjects.Instance.SetUpGround(position);
        }

        if (target.collider.CompareTag(Constants.EndCollider))
        {
            target.collider.tag = Constants.UnTagged;
            target.collider.enabled = false;
            IGround parent = target.transform.parent.gameObject.GetComponent<IGround>();
            parent.ReachedEndPosition();
        }

        if (target.transform.CompareTag(Constants.Finish))
        {
            target.transform.tag = Constants.UnTagged;
            OnCollectedRedCube?.Invoke();
        }

        if (target.transform.CompareTag(Constants.GreenCube))
        {
            IObject cube = target.gameObject.GetComponent<IObject>();
            cube.IsActive = false;
            target.transform.tag = Constants.UnTagged;
            OnCollectedGreenCube?.Invoke(1);
        }
    }


    private void swipeController()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
                return;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
                return;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;
                if ((Mathf.Abs(lp.x - fp.x) > dragDistance ||
                     Mathf.Abs(lp.y - fp.y) > dragDistance) &&
                    Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                {
                    if (lp.x > fp.x)
                    {
                        if (playerPosState == Positions.Middle)
                        {
                            playerPosState = Positions.Right;
                            return;
                        }

                        if (playerPosState == Positions.Left)
                        {
                            playerPosState = Positions.Middle;
                            return;
                        }
                    }
                    else
                    {
                        if (playerPosState == Positions.Middle)
                        {
                            playerPosState = Positions.Left;
                            return;
                        }

                        if (playerPosState == Positions.Right)
                        {
                            playerPosState = Positions.Middle;
                        }
                    }
                }
            }
        }
    }
}