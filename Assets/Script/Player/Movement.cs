using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    private Boolean isMoving;
    private Vector2 input;
    private Animator animator;
    public LayerMask solidObjLayer;
    public LayerMask grassLayer;


    private void Awake()
    {
        //Take the animation in animator
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            //Take input from player
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Removing diagonal movement
            if (input.x != 0) input.y = 0;

            //Check does player input something
            if(input != Vector2.zero)
            {
                //Set for both parameter to let the animation know which direction
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;

                if (IsWalkAble(targetPosition)) {
                    StartCoroutine(Move(targetPosition));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);
    }
    //Function to move a player one tiles
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
        checkForEncounter(targetPos);
    }

    //Check collision with something that can't not pass
    private bool IsWalkAble(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.1f, solidObjLayer) != null)
        {
            return false;
        }
        return true;
    }

    private bool checkForEncounter(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, grassLayer) != null)
        {
            if(Random.Range(1, 101) <= 10)
            {
                Debug.Log("Battle fight");
                return true;
            }
        }
        return false;
    }
}
