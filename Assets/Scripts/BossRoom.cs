using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossRoom : MonoBehaviour {

    public GameObject turret1, turret2, turret3, turret4;


	
	void Update ()
    {

        if (!turret1 && !turret2 && !turret3 && !turret4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            FindObjectOfType<AudioManager>().stop("Theme");
            FindObjectOfType<AudioManager>().play("SaxSolo");
        }
	}
}
