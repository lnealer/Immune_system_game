using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBehavior : MonoBehaviour
{
    float maxX = 5.7f;
    float minX = -5.7f;
    float maxY = 3.8f;
    float minY = -3.8f;
    private float tChange = 0f;
    private float randomX;
    private float randomY;
    private float moveSpeed = 3f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
 
    void Update () {
        if (Time.time >= tChange){
            randomX = Random.Range(-1.0f,1.0f);
            randomY = Random.Range(-1.0f,1.0f);
            tChange = Time.time + Random.Range(0.5f,1.5f);
        }

        transform.Translate(new Vector2(randomX, randomY) * moveSpeed * Time.deltaTime);

        if (transform.position.x >= maxX || transform.position.x <= minX) {
            randomX = -randomX;
        }
        if (transform.position.y >= maxY || transform.position.y <= minY) {
            randomY = -randomY;
        }
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.name == "Neutral Cell")
        {
            if(!(collision.gameObject.GetComponent<NeutralCellBehavior>().isInfected))
            {
                Debug.Log("Hit healthy cell");
                collision.gameObject.GetComponent<NeutralCellBehavior>().isInfected = true;
                StartCoroutine(DelayVirus(collision));
            }
        }
    }

    private IEnumerator DelayVirus(Collision2D collision)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(8);
        Destroy(collision.transform.gameObject);
        Debug.Log("Healthy cell destroyed!");
        Time.timeScale = 1f;
    }

}
