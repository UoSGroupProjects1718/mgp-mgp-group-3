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

    [System.Serializable]
    public class Recipe
    {
        public string name;
        public Sprite image;
        public string[] ingredients;
    }

    [Header("Recipes")]
    public Recipe[] breakfastRecipes;
    public Recipe[] starterRecipes;
    public Recipe[] mainRecipes;
    public Recipe[] desertRecipes;

    // Use this for initialization
    void Start () {
        GetNewMainRecipe();
        RT = this.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        GetInput();
        UpdateTarget();
        Move();
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClearRecipe();
            GetNewBreakfastRecipe();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ClearRecipe();
            GetNewStarterRecipe();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ClearRecipe();
            GetNewMainRecipe();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearRecipe();
            GetNewDessertRecipe();
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

    void GetNewBreakfastRecipe()
    {
        int randomInt = Mathf.RoundToInt(Random.Range(0, breakfastRecipes.Length ));

        recipeName.GetComponent<Text>().text = breakfastRecipes[randomInt].name;
        recipeImage.GetComponent<Image>().sprite = breakfastRecipes[randomInt].image;

        foreach (var ingredient in breakfastRecipes[randomInt].ingredients)
        {
            GameObject newIngredient = Instantiate(textPrefab, ingredientList.transform);
            newIngredient.GetComponent<Text>().text = ingredient;
        }

        GameManager.Instance().UpdateNeededIngredients(breakfastRecipes[randomInt].ingredients);
    }

    void GetNewStarterRecipe()
    {
        int randomInt = Mathf.RoundToInt(Random.Range(0, starterRecipes.Length));

        recipeName.GetComponent<Text>().text = starterRecipes[randomInt].name;
        recipeImage.GetComponent<Image>().sprite = starterRecipes[randomInt].image;

        foreach (var ingredient in starterRecipes[randomInt].ingredients)
        {
            GameObject newIngredient = Instantiate(textPrefab, ingredientList.transform);
            newIngredient.GetComponent<Text>().text = ingredient;
        }
        GameManager.Instance().UpdateNeededIngredients(starterRecipes[randomInt].ingredients);
    }

    void GetNewMainRecipe()
    {
        int randomInt = Mathf.RoundToInt(Random.Range(0, mainRecipes.Length));

        recipeName.GetComponent<Text>().text = mainRecipes[randomInt].name;
        recipeImage.GetComponent<Image>().sprite = mainRecipes[randomInt].image;

        foreach (var ingredient in mainRecipes[randomInt].ingredients)
        {
            GameObject newIngredient = Instantiate(textPrefab, ingredientList.transform);
            newIngredient.GetComponent<Text>().text = ingredient;
        }
        GameManager.Instance().UpdateNeededIngredients(mainRecipes[randomInt].ingredients);
    }

    void GetNewDessertRecipe()
    {
        int randomInt = Mathf.RoundToInt(Random.Range(0, desertRecipes.Length));

        recipeName.GetComponent<Text>().text = desertRecipes[randomInt].name;
        recipeImage.GetComponent<Image>().sprite = desertRecipes[randomInt].image;

        foreach (var ingredient in desertRecipes[randomInt].ingredients)
        {
            GameObject newIngredient = Instantiate(textPrefab, ingredientList.transform);
            newIngredient.GetComponent<Text>().text = ingredient;
        }
        GameManager.Instance().UpdateNeededIngredients(desertRecipes[randomInt].ingredients);
    }
}
