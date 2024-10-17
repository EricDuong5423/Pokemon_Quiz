using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed;
    private Boolean isMoving;
    private Vector2 input;
    private Animator animator;

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

                StartCoroutine(Move(targetPosition));
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
    }
}
