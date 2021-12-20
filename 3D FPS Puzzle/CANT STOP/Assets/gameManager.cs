using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public bool death = false;
    public GameObject deathScreen;
    public GameObject winScreen;
    public PlayerMovement playerMov;
    public weaponSway weapon;
    public targetController targets;
    public shootGun shots;
    public string nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        deathScreen.active = false;
        winScreen.active = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (death == true&&targets.win==false) {
            deathScreen.active = true;
            Time.timeScale = .1f;
            playerMov.enabled = false;
            weapon.enabled = false;
            targets.enabled = false;
            shots.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && death == true) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
            playerMov.enabled = true;
            weapon.enabled = true;
            targets.enabled = true;
            deathScreen.active = false;
            shots.enabled = true;
        }
        if (targets.win == true) {
            winScreen.active = true;
            Time.timeScale = .1f;
        }
        if (targets.win == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(nextLevel);
        }
    }
}
