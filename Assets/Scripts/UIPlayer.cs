using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class UIPlayer : MonoBehaviour
{
    public static UIPlayer _playerInstance;


    //general player vars
    public int money = 10;
    public float health = 100;
    public string playerName;

    //input texts - NOT all scenes will have input texts 
    //so sometimes we are gonna need logic to check if the text we want is accessible
    //see the money text example in update for this in practice
    public TMP_InputField nameInput;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;

    public List<GameObject> dealers;
    public GameObject dealerPanel;

    //statuses
    public bool poisoned;
    public bool sleepy;
    public bool hungry;

    //inventory is an ARRAY because I want it to be a fixed length
    //this means we have an index for each item in the array we can check
    //it also means players will have to manage inventory
    //arrays are less memory intensive so it's better to use this when possible
    public string[] myInventory;

    //a list can have a variable length - lists don't really have index values the same way an array does
    //instead we can use .Remove, .Add, and .Contains to check and update the list
    //this is just easier for tracking scenes visited and will be used less so I'm not worried about memory
    public List<string> scenesVisited;


    public void Awake()
    {
        if (_playerInstance == null)
        {
            _playerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Debug.Log("player instance has been registered: " +  _playerInstance.gameObject.GetEntityId());

        myInventory = new string[5];
        //this line of code preserves the gameobject through scene changes --------------------
        DontDestroyOnLoad(this.gameObject);
        //needed for player, gamemanager, audiomanager etc!
    }

    public void Start()
    {
        Debug.Log("player initialized");

        //first find the gameobject you're looking for - in whatever way is best for you
        //quick name check
        GameObject healthBar = GameObject.Find("Health");
        //if the gameobject exists, then we can look for any components we want
        if(healthBar != null )
        {
            //if we're not sure a component will be found, or if a component is only likely
            //to be attached to the gameobject SOME of the time
            //TryGet returns a bool that is true when the component exists
            //it also sends out the component using an out parameter
            if(TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tmpHealth))
            {
                //set our variable
                healthText = tmpHealth;
            }
            //we can write better error reports using this process as well
            else { Debug.Log("HEALTH TEXT NOT FOUND IN SCENE: " + SceneManager.GetActiveScene().name); }
        }
    }


    public void Update()
    {
        //checking to see if something is null or not before updating
        if(moneyText != null)
        {
            moneyText.text = "Money: " + money;
        }

        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }


    //in a UI game with buttons we're mostly calling events from OnClick()
    public void ChangeName(TMP_InputField input)
    {
        playerName = input.text;
    }

    public void FlipPoison(bool b)
    {
        poisoned = b;
    }

    public void AddToInventory(string s)
    {
        for (int i = 0; i < myInventory.Length; i++)
        {
            if (myInventory[i] == null)
            {
                myInventory[i] = s;
                //break ends the FOR loop it is placed within
                //it does NOT end the function itself
                break;
            }
        }
        foreach(string j in myInventory)
        {
            Debug.Log("holding: " + j);
        }
    }

    public void ChangeHealth(float f)
    {
            health -= f;
    }

}
