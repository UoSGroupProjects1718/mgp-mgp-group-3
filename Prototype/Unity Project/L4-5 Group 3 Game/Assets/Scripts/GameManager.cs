using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager _instance;

    public RecipeCardScript RCS;

    public GameObject ingredientPrefab;

    public GameObject player1Side;
    public GameObject player1ButtonArea;
    public GameObject player2Side;
    public GameObject player2ButtonArea;

    public float player1SideOpen;
    public float player2SideOpen;
    public float player1SideClosed;
    public float player2SideClosed;

    public int RecipeTypeID;

    int lastPlayer = 2;

    public RecipeCardScript.Ingredient[] neededIngredients;
    public RecipeCardScript.Ingredient[] neededMixing;

    public enum Turns{
        none,
        Player1,
        Player2
    }

    public Turns currentTurn = Turns.none;

    int[] neededAmts = new int[6];
    int[] player1GivenAmt = new int[6];
    int[] player2GivenAmt = new int[6];

    int[] neededMixingAmts = new int[6];
    int[] player1GivenMixingAmt = new int[6];
    int[] player2GivenMixingAmt = new int[6];

    static public GameManager Instance()
    {
        return _instance;
    }

    List<GameObject> currentButtons = new List<GameObject>();

	void Start () {
        if (_instance == null)
        {
            _instance = this.GetComponent<GameManager>();
        }
    }

	void Update () {
        if (currentTurn == Turns.Player1)
        {
            player1Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player1Side.GetComponent<RectTransform>().anchoredPosition.y,player1SideOpen, 2));
            player2Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player2Side.GetComponent<RectTransform>().anchoredPosition.y, player2SideClosed, 2));
        }
        else
        if (currentTurn == Turns.Player2)
        {
            player1Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player1Side.GetComponent<RectTransform>().anchoredPosition.y, player1SideClosed, 2));
            player2Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player2Side.GetComponent<RectTransform>().anchoredPosition.y, player2SideOpen, 2));
        }
        else if(currentTurn == Turns.none)
        {
            player1Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player1Side.GetComponent<RectTransform>().anchoredPosition.y, player1SideClosed, 2));
            player2Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player2Side.GetComponent<RectTransform>().anchoredPosition.y, player2SideClosed, 2));
        }
    }

    public void StartRound()
    {
        RCS.GetRecipe(RecipeTypeID);
        currentTurn = Turns.none;
        RCS.currentTarget = RecipeCardScript.Targets.middle;
        RCS.StartCountdown();
    }

    public void StartMixingRound()
    {
        RCS.GetComponent<RectTransform>().anchoredPosition = RCS.rightPosition;
        RCS.GetMixing();
        lastPlayer = 2;
        currentTurn = Turns.none;
        RCS.currentTarget = RecipeCardScript.Targets.middle;
        RCS.StartCountdown();
    }

    public void StartNextPlayer()
    {
        if (lastPlayer == 2)
        {
            SwitchTurn(1);
            lastPlayer = 1;
        }
        else
        {
            SwitchTurn(2);
            lastPlayer = 2;
        }
    }

    public void SwitchTurn(int player)
    {
        if (player == 1)
        {
            currentTurn = Turns.Player1;
        }
        if (player == 2)
        {
            currentTurn = Turns.Player2;
        }
    }

    public void UpdateNeededIngredients(RecipeCardScript.Ingredient[] ingredients)
    {
        DestroyAllButtons();

        neededIngredients = ingredients;
        int id = 0;
        foreach (RecipeCardScript.Ingredient item in neededIngredients)
        {
            GameObject newButton1 = Instantiate(ingredientPrefab, player1ButtonArea.transform);
            newButton1.name = item.name;
            newButton1.GetComponent<Ingredient>().SetSprite(item.name);
            newButton1.GetComponent<Ingredient>().SetID(id);
            currentButtons.Add(newButton1);

            GameObject newButton2 = Instantiate(ingredientPrefab, player2ButtonArea.transform);
            newButton2.name = item.name;
            newButton2.GetComponent<Ingredient>().SetSprite(item.name);
            newButton2.GetComponent<Ingredient>().SetID(id);
            currentButtons.Add(newButton2);
            id++;
            neededAmts[id] = item.amtNeeded;
        }
    }

    public void UpdateNeededMixing(RecipeCardScript.Ingredient[] mixing)
    {
        DestroyAllButtons();

        neededMixing = mixing;
        int id = 0;
        foreach (RecipeCardScript.Ingredient item in neededMixing)
        {
            GameObject newButton1 = Instantiate(ingredientPrefab, player1ButtonArea.transform);
            newButton1.name = item.name;
            newButton1.GetComponent<Ingredient>().SetSprite(item.name);
            newButton1.GetComponent<Ingredient>().SetID(id);
            currentButtons.Add(newButton1);

            GameObject newButton2 = Instantiate(ingredientPrefab, player2ButtonArea.transform);
            newButton2.name = item.name;
            newButton2.GetComponent<Ingredient>().SetSprite(item.name);
            newButton2.GetComponent<Ingredient>().SetID(id);
            currentButtons.Add(newButton2);
            id++;
            neededAmts[id] = item.amtNeeded;
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

    public void SetGivenAmt(int id, int amt)
    {
        if(currentTurn == Turns.Player1)
        {
            player1GivenAmt[id] = amt;
        }
        else
        {
            player2GivenAmt[id] = amt;
        }
    }

    public void Player1Submit()
    {

    }

    public void Player2Submit()
    {

    }
}
