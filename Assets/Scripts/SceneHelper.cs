using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    //scene helper is mostly just for player tracking data
    //so the player is the only variable right now
    public UIPlayer myPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //put a sceneHelper in each scene
        //that way this start function runs once each time a new scene is loaded
        //the first thing we do is register the player with the sceneHelper
        myPlayer = GameObject.FindFirstObjectByType<UIPlayer>();
        Debug.Log("SceneHelper initalized for scene: " + SceneManager.GetActiveScene().name);

        //next we have our status check section
        //this can simulate passage of time and track statuses like poisoned or burning or sleepy
        if(myPlayer.poisoned) { myPlayer.ChangeHealth(10f); }
        //maybe we add some more statuses as we go?


        //just for tracking purposes, let's check and see if the player has been here before
        //the player script has a LIST of all visited scenes
        //because it's a list, we can use the CONTAINS() function to check if we've already loaded this scene once this game
        if (!myPlayer.scenesVisited.Contains(SceneManager.GetActiveScene().name))
        {
            myPlayer.scenesVisited.Add(SceneManager.GetActiveScene().name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
