using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour {

    public List<Transform> bodyList = new List<Transform>();
    public int step=30;
    public float velocity=0.35f;
    private int x;
    private int y;
    private Vector3 headPos;
    private Transform canvas;

    public GameObject bodyPrefab;
    public Sprite[] bodySprites = new Sprite[2];



    private void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
    }

    private void Start()
    {
        x = 0;
        y = step;
        InvokeRepeating("Move", 0, velocity);

    }

    void Grow()
    {
        int index = (bodyList.Count%2 == 0) ? 0 : 1;
        GameObject body = Instantiate(bodyPrefab,new Vector3(2000,2000,0),Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprites[index];
        body.transform.SetParent(canvas, false);
        bodyList.Add(body.transform);

    }

    /// <summary>
    /// 方向键控制
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity-0.2f);
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }

        if (Input.GetKeyDown(KeyCode.W)&&y!=-step){

            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0;
            y = step;
        }
        if (Input.GetKeyDown(KeyCode.S)&&y!=step){

            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -step;
        }
        if (Input.GetKeyDown(KeyCode.A)&&x!=step){

            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step;
            y = 0;
        }
        if (Input.GetKeyDown(KeyCode.D)&&x!=-step){

            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90 );
            x = step;
            y = 0;
        }
    }

    /// <summary>
    /// 移动
    /// </summary>
    void Move()
    {
        headPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z);
        if (bodyList.Count>0)
        {
            bodyList.Last().localPosition = headPos;
            bodyList.Insert(0, bodyList.Last());
            bodyList.RemoveAt(bodyList.Count - 1);
        }
        
    }

    /// <summary>
    /// 触发监测，销毁食物，产生新的食物
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Destroy(collision.gameObject);
            Grow();
            FoodMaker.Instance.MakeFood();
        }
    }
}
