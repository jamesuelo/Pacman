using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 destination = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        destination = this.transform.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<AudioSource>().volume = 0.1f;
        Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed);
        GetComponent<Rigidbody2D>().MovePosition(newPos);
        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {
            float distanceToDestination = Vector2.Distance((Vector2)this.transform.position, destination);
        if(distanceToDestination < 0.01f)
        {

            if (Input.GetKey(KeyCode.UpArrow) && CanMoveTo(Vector2.up))
                destination = (Vector2)this.transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && CanMoveTo(Vector2.right))
                destination = (Vector2)this.transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && CanMoveTo(Vector2.down))
                destination = (Vector2)this.transform.position + Vector2.down;
            if (Input.GetKey(KeyCode.LeftArrow) && CanMoveTo(Vector2.left))
                destination = (Vector2)this.transform.position + Vector2.left;
        }

        Vector2 direction = destination - (Vector2)this.transform.position;
        GetComponent<Animator>().SetFloat("DirX", direction.x);
        GetComponent<Animator>().SetFloat("DirY", direction.y);
        }
        else
        {
            GetComponent<AudioSource>().volume = 0;
        }
    }

    bool CanMoveTo(Vector2 dir)
    {
        Vector2 pacmanPos = this.transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pacmanPos + dir, pacmanPos);
        Collider2D pacmanCol = GetComponent<Collider2D>();
        Collider2D hitCol = hit.collider;
        return hitCol == pacmanCol;
    }

}
