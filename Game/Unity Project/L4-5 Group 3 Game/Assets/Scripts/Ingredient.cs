using System.Collections;
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
    
    public enum ButtonTypes
    {
        Ingredient,
        Mixing
    }

    public ButtonTypes buttonType = ButtonTypes.Ingredient;

    [System.Serializable]
    public class IngredientInfo{
        public string name;
        public Sprite image;
        public AudioClip sound;
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

        if (this.GetComponent<Image>().sprite == null)
        {
            foreach (IngredientInfo item in mixing)
            {
                //Debug.Log(ingredientName + item.name);

                if (item.name == ingredientName)
                {
                    if (item.image != null)
                    {
                        this.GetComponent<Image>().sprite = item.image;
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
                if (item.image != null && ButtonImage != null)
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
}
