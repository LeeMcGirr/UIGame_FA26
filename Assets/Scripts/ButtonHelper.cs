using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ButtonHelper : MonoBehaviour
{
    public TextMeshProUGUI myText;
    
    
    //the main reason we have a buttonHelper class 
    //is so we can find the player on scene changes and loads
    //and we don't have to manually link the player to each button in order to call functions on the player!
    public UIPlayer myPlayer;



    public void Start()
    {
        myPlayer = GameObject.FindFirstObjectByType<UIPlayer>();
    }


    //this is just every single onClick function I want to use that isn't already available
    //in the gameobject class
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void AddToPlayerInv(string s)
    {
        myPlayer.AddToInventory(s);
    }

    public void ChangePlayerName(TMP_InputField input)
    {
        myPlayer.ChangeName(input);
    }

    public void DoubleOrNothing(TMP_InputField input)
    {
        int amt = Convert.ToInt32(input.text);
        if(amt > myPlayer.money)
        {
            myText.text = "You lack the funds to make such a risky bet";
            //return ends the ENTIRE function when it is called
            return;
        }

        myPlayer.money -= amt;
        int r = UnityEngine.Random.Range(0, 100);
        if(r%2 == 0) 
        { 
            myPlayer.money += amt*2;
            myText.text = "You win! you now have " + myPlayer.money + " coins";
        }
        else
        {
            myText.text = "Tough luck, you're down to " + myPlayer.money + " coins"; 
        }
    }
}
