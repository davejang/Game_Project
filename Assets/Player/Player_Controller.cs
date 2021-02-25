using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float Max_speed;
    public int idle_state;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        Invoke("Think",3);
    }
    private void Update(){
        // Friction Handling
        if(Input.GetButtonUp("Horizontal")){    
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f,rigid.velocity.y);
        }
        // Sprite Direction
        if(Input.GetButton("Horizontal")){
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        // Animation Contoller
        if(rigid.velocity.normalized.x == 0){ // if player stop
            animator.SetBool("walking",false);
        }
        else{ // if player move
            animator.SetBool("walking",true);
        }
        if(idle_state == 0){
            animator.SetBool("random_idle",true);
        }
        else{
            animator.SetBool("random_idle",false);
        }
    }
    void FixedUpdate()
    {
        // Move by arrow key(← → ↑ ↓)
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h,ForceMode2D.Impulse);

        if(rigid.velocity.x > Max_speed){    // right speed handling
            rigid.velocity = new Vector2(Max_speed,rigid.velocity.y);
        }
        else if(rigid.velocity.x < Max_speed * (-1)){     // left speed handling
             rigid.velocity = new Vector2(Max_speed * (-1),rigid.velocity.y);
        }
    }
    void Think(){
        idle_state = Random.Range(-1,2);
        Invoke("Think",3);
    }
}
