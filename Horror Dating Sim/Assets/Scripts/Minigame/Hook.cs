using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] float starthookSpeed;
    [SerializeField] float reelSpeed;
    [SerializeField] GameObject cage;
    [SerializeField] public bool fullHook;
    private float hookSpeed;
    private bool isReeling = false;
    private Vector2 move;
    private Rigidbody2D rb;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        hookSpeed = starthookSpeed;
    }

    void Update()
    {
        CheckReel();

    }

    private void FixedUpdate()
    {

        if (isReeling)
        {
            Reel();
            rb.MovePosition(move);
        }

        else
        {
            rb.MovePosition(rb.position + (hookSpeed * Time.fixedDeltaTime * move));
            Move();
        }
        
    }

    private void Move()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Reel()
    {
        Vector3 direction = (cage.transform.position - transform.position).normalized;

        move = transform.position + direction * reelSpeed * Time.deltaTime;

    }

    private void CheckReel()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isReeling = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isReeling = false;
        }
    }
}
