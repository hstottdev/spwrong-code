using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D),typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    int direction = 1;
    bool moving;
    bool jumping;
    bool grounded;
    [SerializeField] GameObject hitboxDebugger;
    [HideInInspector] public swappable _swappable;
    [Header("Physics")]
    [SerializeField] float movementForce;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float jumpCooldown = 0.2f;
    float timeWithoutJumping;
    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float groundRayCastDistance;
    [SerializeField] PhysicsMaterial2D airborneFriction;
    PhysicsMaterial2D normalFriction;
    [SerializeField] Vector3 groundRaycastOffset;
    [Header("Animation")]
    [SerializeField] Animator ani;
    public static swappable currentlyTouching;
    bool won;
    bool lost;
    bool canInput;
    public static int health;
    [Header("Invincibility Frames")]
    [SerializeField] float iTime;
    float timeSinceDamaged;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _swappable = GetComponent<swappable>();
        timeSinceDamaged = iTime;
        normalFriction = rb.sharedMaterial;
        canInput = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.hasInstance())
        {
            foreach(TextMeshProUGUI txt in GameManager.inst.correctText)
            {
                txt.text = "a " + _swappable.originalSpriteName;
            }          
        }
        health = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.paused)
        {
            if (Input.GetKeyDown("escape"))
            {
                GameManager.paused = false;
                Time.timeScale = 1;
            }
        }
        else if(canInput)
        {
            Inputs();

            SwapCheck();

            timeSinceDamaged += Time.deltaTime;

            timeWithoutJumping += Time.deltaTime;
        }

        if (!canInput)
        {
            moving = false;
            jumping = false;
        }


        if (GameManager.hasInstance())
        {
            GameManager.inst.currentObjectText.text = "Sprite: " + _swappable.spriteName;
        }

        LoseCheck();
    }

    private void FixedUpdate()
    {
        grounded = CanJump();

        if (grounded)
        {
            rb.sharedMaterial = normalFriction;
        }
        else
        {
            rb.sharedMaterial = airborneFriction;
        }

        

        Movement();
    }

    void Movement()
    {
        if (moving)
        {
            SpeedLimitedForce(movementForce, maxMoveSpeed, rb, ForceMode2D.Force);
        }

        if (jumping && grounded && timeWithoutJumping > jumpCooldown)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        timeWithoutJumping = 0;
    }

    public void SpeedLimitedForce(float intensity, float maxVelocity, Rigidbody2D body1, ForceMode2D type)
    {
        if (Mathf.Abs(body1.velocity.x) <= Mathf.Abs(maxVelocity))
        {
            body1.AddForce(transform.right * intensity, type);
        }
    }

    void Inputs()
    {
        if (Input.GetKeyDown("a"))
        {
            moving = true;

            if(direction == 1)
            {
                direction = -1;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }           
        }

        if(Input.GetKeyDown("d"))
        {
            moving = true;

            if (direction == -1)
            {
                direction = 1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if(moving && !Input.GetKey("a") && !Input.GetKey("d"))
        {
            moving = false;
        }

        ani.SetBool("moving", moving);


        if (Input.GetKeyDown("space"))
        {
            jumping = true;
        }

        if (Input.GetKeyUp("space"))
        {
            jumping = false;
        }

        bool showHitbox = Input.GetKey("left shift") || won || !CanTakeDamage();

        hitboxDebugger.SetActive(showHitbox);

        if (Input.GetKeyDown("escape"))
        {
            GameManager.paused = true;
            Time.timeScale = 0;
        }
    }

    bool CanJump()
    {
        Vector3 rayPosition = transform.position + groundRaycastOffset;

        bool hit = Physics2D.Raycast(rayPosition, Vector3.down, groundRayCastDistance, groundLayers);

        Debug.DrawRay(rayPosition, Vector3.down * groundRayCastDistance, Color.blue);

        //Debug.Log(hit);

        return hit;
    }

    void SwapCheck()
    {
        if (GameManager.hasInstance())
        {
            if (currentlyTouching != null && Input.GetKeyDown("e") && !won && !lost)
            {
                _swappable.SwapWith(currentlyTouching);

                WinCheck();
            }
        }


    }

    void WinCheck()
    {
        if(_swappable.spriteName == _swappable.originalSpriteName)
        {
            won = true;

            if(SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
            {
                GameManager.inst.endScreen.SetActive(true);
            }
            else
            {
                GameManager.inst.correctUI.SetActive(true);
            }

            currentlyTouching = null;

        }
        else
        {
            TakeDamage(true);
        }
    }

    bool CanTakeDamage()
    {
        return timeSinceDamaged > iTime;
    }

    void TakeDamage(bool ignoreITime = false,int amount = 1)
    {
        if (CanTakeDamage() || ignoreITime)
        {
            health -= amount;
            timeSinceDamaged = 0;//now I get 'iTime' seconds until I can be damaged again
            _swappable.flasher.enabled = false;
            _swappable.flasher.maximumFlashes = Mathf.RoundToInt(iTime / 0.1f);
            Invoke("FlashRed", 0.01f);
        }
    }

    void FlashRed()
    {
        _swappable.flasher.enabled = true;
    }

    void LoseCheck()
    {
        if (health <= 0 && GameManager.hasInstance() && !won)
        {
            GameManager.inst.failedUI.SetActive(true);

            currentlyTouching = null;

            lost = true;

            canInput = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject ob = collision.gameObject;

        if(ob.TryGetComponent(out swappable s) && GameManager.hasInstance() && !won && !lost)
        {
            currentlyTouching = s;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject ob = collision.gameObject;

        if (ob.TryGetComponent(out swappable s) && GameManager.hasInstance())
        {
            if (s == currentlyTouching)
            {
                currentlyTouching = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Spike"))
        {
            TakeDamage();
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Death"))
        {
            TakeDamage(true,2);
            canInput = false;
        }
    }
}
