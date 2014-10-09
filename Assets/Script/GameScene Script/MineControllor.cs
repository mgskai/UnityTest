using UnityEngine;
using System.Collections;

public class MineControllor : MonoBehaviour {

    public enum MineStatus
    {
        Normal,
        Flag,
        Clear,
        Mine
    };

    public TextMesh mineText;
    public GameObject gameOverMessageBox;

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

    private MineStatus _CurrentMineStatus;
    public MineStatus CurrentMineStatus
    {
        get
        {
            return _CurrentMineStatus;
        }
        set
        {
            _CurrentMineStatus = value;
        }
    }
    
    private static bool firstClick;
     private BoxCollider mineCollider;
    void Awake()
    {
        firstClick = true;
        renderer.material.color = Color.green;
        mineText.text = "";
        mineCollider = GetComponent<BoxCollider>();
        CurrentMineStatus = MineStatus.Normal;
    }

    void OnMouseEnter()
    {
        if (CurrentMineStatus == MineStatus.Normal)
        {
            renderer.material.color = Color.cyan;
        }
    }

    void OnMouseExit()
    {
        if (CurrentMineStatus == MineStatus.Normal)
        {
            renderer.material.color = Color.green;
        }
    }

    void OnMouseOver()
    {
        if (CurrentMineStatus != MineStatus.Clear)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (firstClick)
                {
                    AssignMine();
                    firstClick = false;
                }
                if (IsMine)
                {
                    Instantiate(gameOverMessageBox);
                    renderer.material.color = Color.red;
                    CurrentMineStatus = MineStatus.Mine;
                    DisableAllMine();
                    
                }
                else
                {
                    CurrentMineStatus = MineStatus.Clear;
                    renderer.material.color = Color.blue;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                renderer.material.color = Color.yellow;
                CurrentMineStatus = MineStatus.Flag;
            }
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

    void DisableAllMine()
    {
        GameObject gameController = GameObject.Find("GameController");
        GameControl gameControl = gameController.GetComponent<GameControl>();

        for(int i = 0; i < gameControl.mineArray.GetLength(0); i++)
        {
            for (int j = 0; j < gameControl.mineArray.GetLength(1); j++)
            {
                MineControllor m = gameControl.mineArray[i, j];
                if (m.IsMine)
                {
                    m.renderer.material.color = Color.red;
                }
                else
                {
                    m.renderer.material.color = Color.blue;
                }
                BoxCollider bc = m.GetComponent<BoxCollider>();
                bc.enabled = false;
            }

        }
    }

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
