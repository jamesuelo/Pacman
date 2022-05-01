using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypoint = 0;
    public float speed = 0.3f;

    private void Update()
    {
        if (GameManager.sharedInstance.invincibleTime > 0)
            GetComponent<Animator>().SetBool("Pacman Inv", true);
        else
            GetComponent<Animator>().SetBool("Pacman Inv", false);
    }
    private void FixedUpdate()
    {

        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {
            GetComponent<AudioSource>().volume = 0.05f;

            float distanceToWaypoint = Vector2.Distance((Vector2)this.transform.position, (Vector2)waypoints[currentWaypoint].position);
            if (distanceToWaypoint < 0.1f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                Vector2 newDirection = waypoints[currentWaypoint].position - this.transform.position;
                GetComponent<Animator>().SetFloat("DirX", newDirection.x);
                GetComponent<Animator>().SetFloat("DirY", newDirection.y);
            }
            else
            {
                Vector2 newPos = Vector2.MoveTowards(this.transform.position, waypoints[currentWaypoint].position, speed);
                GetComponent<Rigidbody2D>().MovePosition(newPos);
            }
        }
        else
        {
            GetComponent<AudioSource>().volume = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player")
            if(GameManager.sharedInstance.invincibleTime <=0)
                Destroy(otherCollider.gameObject);
            else
            {
                GameObject home = GameObject.Find("Ghost Home");
                this.transform.position = home.transform.position;
                this.currentWaypoint = 0;
                UIManager.sharedInstance.ScorePoints(1000);
            }    
    }
}
