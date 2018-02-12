using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour {

    Image ButtonImage;

    public Text debugText;

    [System.Serializable]
    public class IngredientInfo{
        public string name;
        public Sprite image;
    }

    public IngredientInfo[] ingredients;

    public void SetSprite(string ingredientName)
    {
        debugText.text = ingredientName;
        foreach (IngredientInfo item in ingredients)
        {
            if(item.name == ingredientName)
            {
                if (item.image != null)
                {
                    ButtonImage.sprite = item.image;
                }
                return;
            }
        }
        Debug.Log("Couldn't find the ingredient");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
