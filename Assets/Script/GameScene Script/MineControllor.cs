using UnityEngine;
using System.Collections;


public class MineControllor : MonoBehaviour {
    
    public TextMesh mineText;
    
    private bool _IsMine;
    public bool IsMine
    {
        get
        {
            return _IsMine;
        }
        set
        {
            _IsMine = value;
        }
    }

    private int _Row;
    public int Row
    {
        get
        {
            return _Row;
        }
        set
        {
            _Row = value;
        }
    }

    private int _Column;
    public int Column
    {
        get
        {
            return _Column;
        }
        set
        {
            _Column = value;
        }
    }
    
    private static bool firstClick = true;
     private BoxCollider bc;
    void Awake()
    {
        renderer.material.color = Color.green;
        mineText.text = "";
        bc = GetComponent<BoxCollider>();
    }

    //void OnMouseEnter()
    //{
    //    renderer.material.color = Color.cyan;
    //}

    //void OnMouseExit()
    //{
    //    if (bc.enabled)
    //    {
    //        renderer.material.color = Color.green;
    //    }
    //}

    void OnMouseOver()
    {
        renderer.material.color = Color.cyan;
        if (Input.GetMouseButtonDown(0))
        {
            if (firstClick)
            {
                AssignMine();
                firstClick = false;
            }
            if (IsMine)
            {
                renderer.material.color = Color.red;
            }
            else
            {
                renderer.material.color = Color.blue;
            }
            bc.enabled = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            renderer.material.color = Color.yellow;
            bc.enabled = false;
        }

    }

    //void OnMouseUp()
    //{
    //    if (firstClick)
    //    {
    //        AssignMine();
    //        firstClick = false;
    //    }
    //    if (IsMine)
    //    {
    //        renderer.material.color = Color.red;
    //    }
    //    else
    //    {
    //        renderer.material.color = Color.blue;
    //    }
    //    bc.enabled = false;
    //}

    void AssignMine()
    {
        GameObject gameController = GameObject.Find("GameController");
        GameControl gameControl = gameController.GetComponent<GameControl>();

        for (int i = 0; i < gameControl.MineNum; )
        {
            int index = Random.Range(0, gameControl.mineGrid.Row * gameControl.mineGrid.Column - 1);
            int row = index / gameControl.mineGrid.Row;
            int column = index % gameControl.mineGrid.Column;
            if ((row != this.Row && column != this.Column ) && !gameControl.mineArray[row, column].IsMine)
            {
                gameControl.mineArray[row, column].IsMine = true;
                i++;
            }
        }

    }
}
