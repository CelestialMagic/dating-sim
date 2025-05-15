using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float offsetY;
    private bool isHooked;
    private Rigidbody2D rb;
    private GameObject hook;

    void Start()
    {
        hook = GameObject.Find("Hook");
        rb = this.GetComponent<Rigidbody2D>();

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isHooked)
        {
            rb.velocity = new Vector2(0, 0);

            transform.position = new Vector2(hook.transform.position.x, hook.transform.position.y - offsetY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.GetComponent<Hook>().fullHook == false)
        {
            collision.gameObject.GetComponent<Hook>().fullHook = true;
            isHooked = true;
        }

        if (collision.CompareTag("Catcher"))
        {
            hook.GetComponent<Hook>().fullHook = false;
            Debug.Log("Target Acquired");
            Destroy(this.gameObject);
        }
    }


}
