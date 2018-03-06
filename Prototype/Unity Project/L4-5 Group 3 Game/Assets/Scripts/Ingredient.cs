﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour {

    int id;

    Image ButtonImage;
    public GameObject timesPressedBubble;

    public Text debugText;

    public int pressesNeeded;
    public int timesPressed;
    

    [System.Serializable]
    public class IngredientInfo{
        public string name;
        public Sprite image;
    }

    public IngredientInfo[] ingredients;
    public IngredientInfo[] mixing;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSprite(string ingredientName)
    {
        foreach (IngredientInfo item in ingredients)
        {
            //Debug.Log(ingredientName + item.name);

            if (item.name == ingredientName)
            {
                if (item.image != null)
                {
                    this.GetComponent<Image>().sprite = item.image;
                }
                debugText.text = ingredientName;
                return;
            }
        }

        foreach (IngredientInfo item in mixing)
        {
            //Debug.Log(ingredientName + item.name);

            if (item.name == ingredientName)
            {
                if (item.image != null)
                {
                    ButtonImage.sprite = item.image;
                }
                debugText.text = ingredientName;
                return;
            }
        }
        Debug.Log("Couldn't find the ingredient");
    }

    public void IngredientPressed()
    {
        foreach(var ingredient in GameManager.Instance().neededIngredients)
        {
            if(ingredient.name == this.name)
            {
                timesPressedBubble.SetActive(true);
                timesPressed++;
                timesPressedBubble.GetComponentInChildren<Text>().text = timesPressed.ToString();
            }
        }

        foreach (var ingredient in GameManager.Instance().neededMixing)
        {
            if (ingredient.name == this.name)
            {
                timesPressedBubble.SetActive(true);
                timesPressed++;
                timesPressedBubble.GetComponentInChildren<Text>().text = timesPressed.ToString();
            }
        }
        GameManager.Instance().SetGivenAmt(id, timesPressed);
    }

    public void SetID(int newID)
    {
        id = newID;
    }
}
