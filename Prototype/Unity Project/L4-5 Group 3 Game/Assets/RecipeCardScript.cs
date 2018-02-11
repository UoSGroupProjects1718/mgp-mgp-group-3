using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCardScript : MonoBehaviour {

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
	}
	
	// Update is called once per frame
	void Update () {
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
    }
}
