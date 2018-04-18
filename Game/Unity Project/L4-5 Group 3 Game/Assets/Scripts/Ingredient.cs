using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour {

    int id;

    Image ButtonImage;
    public Image ButtonIngredientImage;
    public GameObject timesPressedBubble;

    public Text debugText;

    public int pressesNeeded;
    public int timesPressed;
    
    public enum ButtonTypes
    {
        Ingredient,
        Mixing
    }

    public ButtonTypes buttonType = ButtonTypes.Ingredient;

    public enum FoodType
    {
        Fish,
        Vegetable,
        Fruit,
        Meat,
        Dairy,
        Other
    }

    [System.Serializable]
    public class ButtonInfo
    {
        public Sprite Idle;
        public Sprite Pressed;
    }

    [System.Serializable]
    public class IngredientInfo
    {
        public string name;
        public Sprite image;
        public FoodType foodType;
        public AudioClip sound;
    }

    public ButtonInfo[] Buttons;
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
                    SetButtonColor(item);
                    ButtonIngredientImage.sprite = item.image;
                }
                
                debugText.text = ingredientName;
                return;
            }
        }

        if (this.GetComponent<Image>().sprite == null)
        {
            foreach (IngredientInfo item in mixing)
            {
                //Debug.Log(ingredientName + item.name);

                if (item.name == ingredientName)
                {
                    if (item.image != null)
                    {
                        SetButtonColor(item);
                        ButtonIngredientImage.sprite = item.image;
                    }

                    if(item.sound != false)
                    {
                        this.GetComponent<AudioSource>().PlayOneShot(item.sound);
                    }

                    debugText.text = ingredientName;
                    return;
                }
            }
        }

        foreach (IngredientInfo item in mixing)
        {
            //Debug.Log(ingredientName + item.name);

            if (item.name == ingredientName)
            {
                if (item.image != null && ButtonIngredientImage != null)
                {
                    ButtonIngredientImage.sprite = item.image;
                }
                debugText.text = ingredientName;
                return;
            }
        }
        Debug.Log("Couldn't find the ingredient");
    }

    private void SetButtonColor(IngredientInfo item)
    {
        int i = 0;
        switch (item.foodType)
        {
            case FoodType.Fish:
                i = 0;
                break;

            case FoodType.Vegetable:
                i = 1;
                break;

            case FoodType.Fruit:
                i = 2;
                break;

            case FoodType.Meat:
                i = 3;
                break;

            case FoodType.Dairy:
                i = 4;
                break;

            case FoodType.Other:
                i = 5;
                break;

            default:
                break;
        }

        this.GetComponent<Image>().sprite = Buttons[i].Idle;
        SpriteState ss;
        ss.pressedSprite = Buttons[i].Pressed;
        this.GetComponent<Button>().spriteState = ss;
    }

    public void IngredientPressed()
    {


        if (GameManager.Instance().currentTurn == GameManager.Turns.Player1)
        {

            foreach (var ingredient in GameManager.Instance().neededIngredientsP1)
            {
				Debug.Log ("poop");
                if (ingredient.name == this.name)
                {
                    timesPressedBubble.SetActive(true);
                    timesPressed++;
                    timesPressedBubble.GetComponentInChildren<Text>().text = timesPressed.ToString();
                }
            }

            foreach (var ingredient in GameManager.Instance().neededMixingP1)
            {
                if (ingredient.name == this.name)
                {
                    if (timesPressedBubble != false)
                        timesPressedBubble.SetActive(true);

                    timesPressed++;

                    if (timesPressedBubble != false)
                        timesPressedBubble.GetComponentInChildren<Text>().text = timesPressed.ToString();
                }
            }
        }
        else
        {
            foreach (var ingredient in GameManager.Instance().neededIngredientsP2)
            {
                if (ingredient.name == this.name)
                {
                    timesPressedBubble.SetActive(true);
                    timesPressed++;
                    timesPressedBubble.GetComponentInChildren<Text>().text = timesPressed.ToString();
                }
            }

            foreach (var ingredient in GameManager.Instance().neededMixingP2)
            {
                if (ingredient.name == this.name)
                {
                    if (timesPressedBubble != false)
                        timesPressedBubble.SetActive(true);

                    timesPressed++;

                    if (timesPressedBubble != false)
                        timesPressedBubble.GetComponentInChildren<Text>().text = timesPressed.ToString();
                }
            }
        }

        if (buttonType == ButtonTypes.Ingredient)
        {
            GameManager.Instance().SetGivenAmt(id, timesPressed);
        }
        else
        {
            GameManager.Instance().SetGivenMixingAmt(id, timesPressed);
        }
    }

    public void SetID(int newID)
    {
        id = newID;
    }

    public string GetRandomIngredient()
    {
        IngredientInfo item = ingredients[UnityEngine.Random.Range(0, ingredients.Length)];
        this.name = item.name;
        this.GetComponent<Ingredient>().SetSprite(item.name);
        return item.name;
    }
}
