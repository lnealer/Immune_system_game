using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BCellBehavior : MonoBehaviour
{
    public GameObject antibody;
    public GameObject antibodyBar;
    
    private Vector2 goal;
    public float convertToAntibodyTime = 5f;
    public float moveSpeed;
    private float speed;
    private Animator animator;
    private float scale;
    
    void Update()
    {
        Animate();
        MoveToTarget();
    }

    void Start()
    {
        scale = transform.localScale.y;
        animator = GetComponent<Animator>();
        speed = moveSpeed;
        //antibodyBar = transform.GetChild(0).gameObject;
        //Debug.Log(antibodyBar);

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "NPlus1Level" && GameValues.levelNVaccine != 0)
        {
            convertToAntibodyTime = convertToAntibodyTime/2;
        }
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.gameObject.tag == "Macrophage" && collider.gameObject.GetComponent<MacrophageBehavior>().readAntigen)
        {
            Instantiate(antibodyBar, transform);
            StartCoroutine(DelayDestroyBCell());
            animator.SetBool("Walk", false);
            speed = 0;
        }
    }

    void Animate()
    {
        FlipCharacter();
        animator.SetBool("Walk", speed == moveSpeed);
    }

    
     void MoveToTarget()
    {
        // move towards goal
        transform.position = Vector2.MoveTowards(transform.position, goal, speed*Time.deltaTime);
    }

    Vector2 RandomGoal()
    {
        return new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
    }

    
    void FlipCharacter()
    {
        //float scaleX = transform.localScale.x;
        //bool flipped = false;
        // flip the enemy transform to look to point direction
        if (goal.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1)*scale;
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1)*scale;   
        }

    }

    private IEnumerator DelayDestroyBCell()
    {
        yield return new WaitForSeconds(convertToAntibodyTime);
        Vector3 pos = transform.position;
        Destroy(gameObject);
        Instantiate(antibody, pos, Quaternion.identity);
    }
}
