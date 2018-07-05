using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour {

    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }
    public int xLimit = 21;
    public int yLimit = 11;
    public int xOffset = 7;

    public GameObject foodPrefab;
    public Sprite[] foodSprites;
    private Transform foodHolder;

    private void Start()
    {
        foodHolder = GameObject.Find("FoodHolder").transform;
        MakeFood();
    }

    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// 随机地方生成食物
    /// </summary>
    public void MakeFood()
    {
        int index = Random.Range(0, foodSprites.Length);
        GameObject food = Instantiate(foodPrefab);
        food.GetComponent<Image>().sprite = foodSprites[index];
        food.transform.SetParent(foodHolder, false);
        int x = Random.Range(-xLimit + xOffset, xLimit);
        int y = Random.Range(-yLimit, yLimit);
        food.transform.localPosition = new Vector3(x * 30, y * 30,0);
    }

}   
