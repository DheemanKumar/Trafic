using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class environment : MonoBehaviour
{
    public save_table dt;
    

    public float timer = 0f;
    public float updateInterval = 180f;

    private void Update()
    {

        
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            // Your update logic goes here
            dt.save();
            RestartLevel();
            // Reset the timer
            timer = 0f;
        }



        

    }


    
    // You can call this method from a button click or any other trigger
    void RestartLevel()
    {
        // Get the current active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

