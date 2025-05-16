using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Behavior : MonoBehaviour
{
    [SerializeField] float spdMin, spdMax;
    [SerializeField] float offsetY;
    [SerializeField] private List<Sprite> sprites;
    private bool isHooked = false;
    private float speed;
    private int rand;
    private float direction;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 screenBounds;
    private GameObject hook;



    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        hook = GameObject.Find("Hook");

        Randomize();
        sr.sprite = sprites[rand];

        GetDirection();
        rb.velocity = new Vector2(speed * direction, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if ((transform.position.x < -screenBounds.x * 1.5 || transform.position.x > screenBounds.x * 1.5) && isHooked == false)
        {
            Destroy(this.gameObject);
        }

    }

    private void FixedUpdate()
    {
        if (isHooked)
        {
            rb.velocity = new Vector2(0, 0);
            
            transform.SetPositionAndRotation(new Vector2(hook.transform.position.x, hook.transform.position.y - offsetY), Quaternion.Euler(0, 0, 90 * direction));
        }
    }

    public void GetDirection()
    {
        if (transform.position.x < screenBounds.x / 2)
        {
            direction = 1;
            sr.flipX = true;

        }
        else
        {
            direction = -1;
        }
    }

    public void Randomize()
    {
        rand = Random.Range(0, sprites.Count);
        speed = Random.Range(spdMin, spdMax);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Catcher"))
        {
            hook.GetComponent<Hook>().fullHook = false;
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Hook>().fullHook == false)
        {
            collision.gameObject.GetComponent<Hook>().fullHook = true;
            isHooked = true;
        }
    }


}
