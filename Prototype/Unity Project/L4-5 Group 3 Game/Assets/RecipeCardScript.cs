using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCardScript : MonoBehaviour {

    public GameObject recipeImage;
    public GameObject ingredientList;
    public GameObject textPrefab;

    [System.Serializable]
    public class Recipe
    {
        public Sprite image;
        public string[] ingredients;
    }

    public Recipe[] recipes;

    // Use this for initialization
    void Start () {
        GetNewRecipe();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GetNewRecipe()
    {
        int randomInt = Mathf.RoundToInt(Random.Range(0, recipes.Length ));

        recipeImage.GetComponent<Image>().sprite = recipes[randomInt].image;

        foreach (var ingredient in recipes[randomInt].ingredients)
        {
            GameObject newIngredient = Instantiate(textPrefab, ingredientList.transform);
            newIngredient.GetComponent<Text>().text = ingredient;
        }
    }
}
