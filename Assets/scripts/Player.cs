using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed =3;
    public int coins;


    public float bounceForce = 10f;
    public float airJumpForce = 9f;
    public int maxAirJumps = 2; 

    private float moveInput;
    private bool jumpPressed;
    private int airJumpsLeft;


    public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        airJumpsLeft = maxAirJumps;

    }

    
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            jumpPressed = true;
        }
    }

    void FixedUpdate(){
        Move();
        AirJumps();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void AirJumps()
    {
        if (jumpPressed && airJumpsLeft > 0)
        {
            Jump(airJumpForce);
            airJumpsLeft--;
        }
        jumpPressed = false; 
    }

    void Jump(float force){
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    


    void OnCollisionEnter2D(Collision2D col){

        if(!col.gameObject.CompareTag("Ground")) return;

        foreach (ContactPoint2D contact in col.contacts)
        if(contact.normal.y > 0.5f)
        {
            Jump(bounceForce);
            airJumpsLeft = maxAirJumps;
            break;
        }

        if (col.gameObject.CompareTag("Coin")){
            coins ++;
            Destroy(col.gameObject);
        }
    }
}
