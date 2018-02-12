using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager _instance;

    public GameObject ingredientPrefab;
    public GameObject player1ButtonArea;
    public GameObject player2ButtonArea;

    public string[] neededIngredients;

    static public GameManager Instance()
    {
        return _instance;
    }

    List<GameObject> currentButtons = new List<GameObject>();

    

    

	// Use this for initialization
	void Start () {
        if (_instance == null)
        {
            _instance = this.GetComponent<GameManager>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateNeededIngredients(string[] ingredients)
    {
        
        DestroyAllButtons();

        neededIngredients = ingredients;

        foreach (string item in neededIngredients)
        {
            //Debug.Log("gettingHere");

            GameObject newButton1 = Instantiate(ingredientPrefab, player1ButtonArea.transform);
            newButton1.name = item;
            newButton1.GetComponent<Ingredient>().SetSprite(item);
            currentButtons.Add(newButton1);

            GameObject newButton2 = Instantiate(ingredientPrefab, player2ButtonArea.transform);
            newButton2.name = item;
            newButton2.GetComponent<Ingredient>().SetSprite(item);
            currentButtons.Add(newButton2);
        }
    }

    public void DestroyAllButtons()
    {
        if (currentButtons == null) { return; }

        foreach (GameObject item in currentButtons)
        {
            Destroy(item);
        }
    }
}
