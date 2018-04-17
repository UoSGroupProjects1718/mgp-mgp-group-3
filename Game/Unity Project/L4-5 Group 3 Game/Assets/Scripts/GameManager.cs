using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    static GameManager _instance;

    public RecipeCardScript RCS;

    public GameObject IngredientPrefab;
    public GameObject MixingPrefab;

    public GameObject player1Side;
    public GameObject player1ButtonArea;
    public GameObject player2Side;
    public GameObject player2ButtonArea;
    public GameObject ResultsPanel;

	float timer = 18;
	public Text Player1Timer;
	public Text Player2Timer;

    public int player1MixingInt1;
    public int player1MixingInt2;
    public int player2MixingInt1;
    public int player2MixingInt2;

    public Slider player1Results;
    public Slider player2Results;
    public Slider player1MixingResults;
    public Slider player2MixingResults;

    public float player1SideOpen;
    public float player2SideOpen;
    public float player1SideClosed;
    public float player2SideClosed;

    public int RecipeTypeID;

    public int nextPlayer = 0;

    public RecipeCardScript.Ingredient[] neededIngredientsP1;
    public RecipeCardScript.Ingredient[] neededMixingP1;

    public RecipeCardScript.Ingredient[] neededIngredientsP2;
    public RecipeCardScript.Ingredient[] neededMixingP2;

    public enum Turns
    {
        none,
        Player1,
        Player2
    }

    public enum Stages
    {
        None,
        Ingredients,
        Mixing,
        results
    }

    public Turns currentTurn = Turns.none;
    public Stages currentStage = Stages.Ingredients;

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

        StartRound();
    }

	void Update () {
        if (currentTurn == Turns.Player1)
        {
            player1Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player1Side.GetComponent<RectTransform>().anchoredPosition.y,player1SideOpen, 4));
            player2Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player2Side.GetComponent<RectTransform>().anchoredPosition.y, player2SideClosed, 4));
        }
        else
        if (currentTurn == Turns.Player2)
        {
            player1Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player1Side.GetComponent<RectTransform>().anchoredPosition.y, player1SideClosed, 4));
            player2Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player2Side.GetComponent<RectTransform>().anchoredPosition.y, player2SideOpen, 4));
        }
        else if(currentTurn == Turns.none)
        {
            player1Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player1Side.GetComponent<RectTransform>().anchoredPosition.y, player1SideClosed, 4));
            player2Side.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.MoveTowards(player2Side.GetComponent<RectTransform>().anchoredPosition.y, player2SideClosed, 4));
        }

		if (currentTurn != Turns.none) {
			timer -= Time.deltaTime;
			Player1Timer.text = Mathf.RoundToInt (timer) + "s";
			Player2Timer.text = Mathf.RoundToInt (timer) + "s";
			if (Mathf.RoundToInt (timer) <= 0) {
				StartNextPlayer ();
			}
		}
    }

    public void StartRound()
    {
		nextPlayer = 1;
        RCS.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        RCS.GetRecipe(RecipeTypeID);
        currentTurn = Turns.none;
        RCS.currentTarget = RecipeCardScript.Targets.middle;
        RCS.StartCountdown();

		timer = 15;
    }

    public void StartPlayer2Round()
    {
		nextPlayer = 2;
        RCS.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
        RCS.GetRecipe(RecipeTypeID);
        currentTurn = Turns.none;
        RCS.currentTarget = RecipeCardScript.Targets.middle;
        RCS.StartCountdown();

		timer = 15;
    }

    public void StartMixingRound()
	{        
		nextPlayer = 1;
        RCS.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        RCS.GetComponent<RectTransform>().anchoredPosition = RCS.rightPosition;
        RCS.GetMixing(player1MixingInt1, player1MixingInt2);
        currentTurn = Turns.none;
        RCS.currentTarget = RecipeCardScript.Targets.middle;
        RCS.StartCountdown();

		timer = 15;
    }

    public void StartPlayer2MixingRound()
    {
		nextPlayer = 2;
        RCS.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
        RCS.GetComponent<RectTransform>().anchoredPosition = RCS.rightPosition;
        RCS.GetMixing(player2MixingInt1, player2MixingInt2);
        currentTurn = Turns.none;
        RCS.currentTarget = RecipeCardScript.Targets.middle;
        RCS.StartCountdown();

		timer = 15;
    }

    public void StartResults()
    {
        ResultsPanel.SetActive(true);
        CheckResults();
        currentTurn = Turns.none;
    }

    public void StartNextPlayer()
    {
        if (currentTurn == Turns.Player2)
        {
            if (currentStage == Stages.None)
            {
                currentTurn = Turns.Player1;
                currentStage = Stages.Ingredients;
            }
            else if (currentStage == Stages.Ingredients)
            {
                StartMixingRound();
                currentStage = Stages.Mixing;
            }
            else if (currentStage == Stages.Mixing)
            {
                currentTurn = Turns.none;
                StartResults();
            }
        }
        else if (currentTurn == Turns.Player1)
        {
            if (currentStage == Stages.Ingredients)
            {
                StartPlayer2Round();
            }
            else if (currentStage == Stages.Mixing)
            {
                StartPlayer2MixingRound();
            }
        }
        else if (currentTurn == Turns.none)
        {
            if (currentStage == Stages.None)
            {
                currentTurn = Turns.Player1;
                currentStage = Stages.Ingredients;
            }
            else if (currentStage == Stages.Ingredients)
            {
                if (nextPlayer == 1)
                {
                    currentTurn = Turns.Player1;
                }

                if (nextPlayer == 2)
                {
                    currentTurn = Turns.Player2;
                }
            }
            else if(currentStage == Stages.Mixing)
            {
                if(nextPlayer == 1)
                {
                    currentTurn = Turns.Player1;
                }
                
                if(nextPlayer == 2)
                {
                    currentTurn = Turns.Player2;
                }
            }
        }
    }

    public void UpdateNeededIngredients(RecipeCardScript.Ingredient[] ingredients)
    {
        DestroyAllButtons();
		Debug.Log ("nextplayer is " + nextPlayer);
        if (nextPlayer == 1)
        {
            neededIngredientsP1 = ingredients;
        }

        if (nextPlayer == 2)
        {
            Debug.Log("here");
            neededIngredientsP2 = ingredients;
        }

        int id = 0;
        foreach (RecipeCardScript.Ingredient item in ingredients)
        {
            GameObject newButton1 = Instantiate(IngredientPrefab, player1ButtonArea.transform);
            newButton1.name = item.name;
            newButton1.GetComponent<Ingredient>().SetSprite(item.name);
            newButton1.GetComponent<Ingredient>().SetID(id);
            currentButtons.Add(newButton1);

            GameObject newButton2 = Instantiate(IngredientPrefab, player2ButtonArea.transform);
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
        
        int id = 0;

        if (nextPlayer == 1)
        {
            neededMixingP1 = mixing;
        }

        if (nextPlayer == 2)
        {
            neededMixingP2 = mixing;
        }

        foreach (RecipeCardScript.Ingredient item in mixing)
        {
            GameObject newButton1 = Instantiate(IngredientPrefab, player1ButtonArea.transform);
            newButton1.name = item.name;
            newButton1.GetComponent<Ingredient>().buttonType = Ingredient.ButtonTypes.Mixing;
            newButton1.GetComponent<Ingredient>().SetSprite(item.name);
            newButton1.GetComponent<Ingredient>().SetID(id);
            currentButtons.Add(newButton1);

            GameObject newButton2 = Instantiate(IngredientPrefab, player2ButtonArea.transform);
            newButton2.name = item.name;
            newButton2.GetComponent<Ingredient>().buttonType = Ingredient.ButtonTypes.Mixing;
            newButton2.GetComponent<Ingredient>().SetSprite(item.name);
            newButton2.GetComponent<Ingredient>().SetID(id);
            currentButtons.Add(newButton2);
            id++;
            neededMixingAmts[id] = item.amtNeeded;
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

    public void SetGivenMixingAmt(int id, int amt)
    {
        if (currentTurn == Turns.Player1)
        {
            player1GivenMixingAmt[id] = amt;
        }
        else
        {
            player1GivenMixingAmt[id] = amt;
        }
    }

    public void CheckResults()
    {
        int player1Amt = 0;
        int player2Amt = 0;
        foreach(int amt in player1GivenAmt)
        {
            player1Amt += amt;
        }
        foreach (int amt in player2GivenAmt)
        {
            player2Amt += amt;
        }
        int needed = 0;
        foreach (var i in neededAmts)
        {
            needed += i;

            Debug.Log("i = " + i);
            Debug.Log("needed = " + needed);
        }

        player1Amt -= needed;
        player2Amt -= needed;
        Debug.Log(needed);
        Debug.Log(player1Amt);
        Debug.Log(player2Amt);
        player1Results.value = player1Amt;
        player2Results.value = player2Amt;


        player1Amt = 0;
        player2Amt = 0;
        foreach (int amt in player1GivenAmt)
        {
            player1Amt += amt;
        }
        foreach (int amt in player2GivenAmt)
        {
            player2Amt += amt;
        }
        needed = 0;
        foreach (var i in neededAmts)
        {
            needed += i;

            Debug.Log("i = " + i);
            Debug.Log("needed = " + needed);
        }

        player1Amt -= needed;
        player2Amt -= needed;
        Debug.Log(needed);
        Debug.Log(player1Amt);
        Debug.Log(player2Amt);
        player1MixingResults.value = player1Amt;
        player2MixingResults.value = player2Amt;

    }
}
