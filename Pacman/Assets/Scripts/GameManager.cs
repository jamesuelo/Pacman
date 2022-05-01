using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public bool gameStarted = false;
    public bool gamePaused = false;
    public Text title;
    public float invincibleTime = 0.0f;
    public AudioClip pauseAudio;
    void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;

        StartCoroutine("StartGame");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gamePaused = !gamePaused;
            if (gamePaused)
            {
                PlayPauseMusic();

            }
            else
            {
                StopPauseMusic();
            }
        }

        if (invincibleTime > 0)
            invincibleTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("Main");
    }


    void PlayPauseMusic()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = pauseAudio;
        source.loop = true;
        source.Play();
        source.volume = 0.1f;
    }
    void StopPauseMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(4.0f);
        gameStarted = true;
    }

    public void MakeInvincibleFor(float numberOfSeconds)
    {
        this.invincibleTime += numberOfSeconds;
    }
}
