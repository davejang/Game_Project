using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    Rigidbody2D rigid2D;
    Animator animator;
    float maxWalkSpeed = 6.0f;
    float jumpForce = 1100.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }
    void Update()
    {
        //방향키를 떼면 속도 감소
        if(Input.GetButtonUp("Horizontal")){
            rigid2D.velocity = new Vector2(rigid2D.velocity.normalized.x * 0.5f, rigid2D.velocity.y);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //점프
        if(Input.GetKey(KeyCode.Space) && this.rigid2D.velocity.y == 0){
            this.animator.SetTrigger("jumptrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        float h = Input.GetAxisRaw("Horizontal");

        rigid2D.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //좌우이동
        if(rigid2D.velocity.x > maxWalkSpeed){
            this.animator.SetTrigger("walktrigger");
            rigid2D.velocity = new Vector2(maxWalkSpeed, rigid2D.velocity.y);
        }
        else if(rigid2D.velocity.x < maxWalkSpeed*(-1)){
            this.animator.SetTrigger("walktrigger");
            rigid2D.velocity = new Vector2(maxWalkSpeed*(-1), rigid2D.velocity.y);
        }
        else
            this.animator.SetTrigger("waittrigger");
            
        //스프라이트 방향전환
        if(h != 0){
            transform.localScale = new Vector3(h,1,1);
        }

        //추락시 재시작
        if(transform.position.y < -5){
            SceneManager.LoadScene("SampleScene");
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin"){ 
            //코인을 먹으면 점수 증가
            gameManager.stagePoint += 10;
            //코인을 먹으면 사라짐
            collision.gameObject.SetActive(false);
        }
    }

}
