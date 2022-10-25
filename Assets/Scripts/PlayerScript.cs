using System.IO.Pipes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    private int scoreValue = 0;
    private int livesValue = 3;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    public AudioClip winSound;
    public AudioSource musicSource;

    private bool facingRight = true;

    Animator anim;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

    }

    void Flip()
    {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
            else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            
            Destroy(collision.collider.gameObject);

            if(scoreValue == 4)
            {
                transform.position = new Vector2(37.49f, 0.0f);
            }
            if(scoreValue == 8)
            {
                winTextObject.SetActive(true);
                musicSource.clip = winSound;
                musicSource.Play();
                musicSource.loop = false;
            }
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();

            Destroy(collision.collider.gameObject);
            if(livesValue <= 0)
            {
                loseTextObject.SetActive(true);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

}