using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCardScript : MonoBehaviour {

    GameManager GM = GameManager.Instance();

    [Header("Animation")]
    public Vector3 rightPosition;
    public Vector3 middlePosition;
    public Vector3 leftPosition;
    Vector3 targetPosition;

    public float peekTime;

    public enum Targets
    {
        right,
        middle,
        left
    };

    public Targets currentTarget = Targets.right;
    RectTransform RT;

    [Header("Recipe Card Objects")]
    public GameObject recipeName;
    public GameObject recipeImage;
    public GameObject ingredientList;
    public GameObject textPrefab;

    int lastRecipeType = 0;
    int lastRecipeID = 0;

    bool countdown = false;
    float countdownTimer = 3;

    [System.Serializable]
    public class Recipe
    {
        public string name;
        public Sprite image;
        public Ingredient[] ingredients;
        public Ingredient[] mixing;
    }

    [System.Serializable]
    public class Ingredient
    {
        public string name;
        public int amtNeeded;
    }

    [System.Serializable]
    public class Recipes
    {
        public string name;
        public Recipe[] recipes;
    }

    [Header("Recipes")]
    public Recipes[] RecipeTypes;

    // Use this for initialization
    void Start () {
        //GetRecipe(0);
        RT = this.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        GetInput();
        UpdateTarget();
        Move();

        if(countdown == true)
        {
            if (countdownTimer > 0)
            {
                countdownTimer -= Time.deltaTime;
            }
            else
            {
                countdown = false;
                currentTarget = Targets.left;
                GameManager.Instance().StartNextPlayer();
            }
        }
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClearRecipe();
            GetRecipe(0);
            Debug.Log("getting here");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ClearRecipe();
            GetRecipe(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ClearRecipe();
            GetRecipe(0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearRecipe();
            GetRecipe(0);
        }

        //debug Recipe Card Movement
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentTarget = Targets.left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentTarget = Targets.middle;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentTarget = Targets.right;
        }
    }

    void UpdateTarget()
    {
        switch (currentTarget)
        {
            case Targets.right:
                targetPosition = rightPosition;
                break;

            case Targets.middle:
                targetPosition = middlePosition;
                break;

            case Targets.left:
                targetPosition = leftPosition;
                break;
        }
    }

    void Move()
    {
        if(RT.localPosition != targetPosition)
        {
            Vector2 newPos = Vector2.MoveTowards(RT.localPosition, targetPosition, 10f);
            RT.localPosition = newPos;
        }
    }

    void ClearRecipe()
    {
        foreach (Transform item in ingredientList.transform)
        {
            if (item.name != this.gameObject.name)
            {
                Destroy(item.gameObject);
            }
        }
    }

    public void GetRecipe(int RecipeType)
    {
        ClearRecipe();
		int randomTypeInt = Mathf.RoundToInt(Random.Range(0, RecipeTypes.Length ));
        int randomInt = Mathf.RoundToInt(Random.Range(0, RecipeTypes[RecipeType].recipes.Length ));

		//use this one in the final game as it lets us choose one type of recipe
		//Recipe recipe = RecipeTypes[RecipeType].recipes[randomInt];
		Recipe recipe = RecipeTypes[randomTypeInt].recipes[randomInt];

        recipeName.GetComponent<Text>().text = recipe.name;
        recipeImage.GetComponent<Image>().sprite = recipe.image;

        foreach (var ingredient in recipe.ingredients)
        {
            GameObject newIngredient = Instantiate(textPrefab, ingredientList.transform);
            newIngredient.GetComponent<Text>().text = ingredient.amtNeeded + "x " + ingredient.name;
        }
        GameManager.Instance().UpdateNeededIngredients(recipe.ingredients);
        lastRecipeID = randomInt;
    }

    public void GetMixing()
    {
        ClearRecipe();
        Recipe recipe = RecipeTypes[lastRecipeType].recipes[lastRecipeID];

        foreach (var mix in recipe.mixing)
        {
            GameObject newIngredient = Instantiate(textPrefab, ingredientList.transform);
            newIngredient.GetComponent<Text>().text = mix.amtNeeded + "x " + mix.name;
        }
        GameManager.Instance().UpdateNeededMixing(recipe.mixing);
    }

    public void StartCountdown()
    {
        countdownTimer = peekTime;
        countdown = true;
    }
}
