using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public bool locked;

    private Vector3 velocity;


    private string horizontalAxis;
	private string verticalAxis;
    private float speed;
    private float maxSpeed;


	private PlayerStats playerStats;

    public Sprite[] bossSprites;
    private SpriteRenderer rend;

    Rigidbody rb;


    float moveX, moveY;
	void Awake() {
		locked = false;
	}

	void Start() {
        playerStats = GetComponent<PlayerStats>();
        rend = GetComponentInChildren<SpriteRenderer>();

        speed = playerStats.speed;
        maxSpeed = playerStats.maxSpeed;
        rb = GetComponent<Rigidbody>();
	}

	void Update() {
	}

    void FixedUpdate()
    {
        HandleMovement();
    }
    void HandleMovement() {
        //speed = playerStats.speed;
        maxSpeed = playerStats.maxSpeed;

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (!locked) {

            /*
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //go left
                rend.sprite = bossSprites[1];
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += new Vector3 (-speed, 0.0f, 0.0f);
                playerStats.currentDir = PlayerStats.facingDir.left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                //go right
                rend.sprite = bossSprites[1];
                transform.localScale = new Vector3(1, 1, 1);
                transform.position += new Vector3 (speed, 0.0f, 0.0f) ;
                playerStats.currentDir = PlayerStats.facingDir.right;
            }

            else if (Input.GetKey(KeyCode.UpArrow))
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                rend.sprite = bossSprites[2];
                transform.position += new Vector3(0.0f, speed, 0.0f);
                playerStats.currentDir = PlayerStats.facingDir.up;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                rend.sprite = bossSprites[0];
                transform.position += new Vector3(0.0f, -speed, 0.0f);
                playerStats.currentDir = PlayerStats.facingDir.down;
            }
            */


            //limit the velocity if it's too high or too low
   

            Vector3 movement = new Vector3(moveX, 0, moveY);

            rb.AddForce(movement * speed);

            if (velocity.x > maxSpeed)
            {
                velocity.x = maxSpeed;
            }
            else if (velocity.x < -maxSpeed)
            {
                velocity.x = -maxSpeed;
            }

            if (velocity.y > maxSpeed)
            {
                velocity.y = maxSpeed;
            }
            else if (velocity.y < -maxSpeed)
            {
                velocity.y = -maxSpeed;
            }


        }
    }


	public void InitializeAxes(string[] controls) {
		horizontalAxis = controls[0];
		verticalAxis = controls[1];
	}

}
