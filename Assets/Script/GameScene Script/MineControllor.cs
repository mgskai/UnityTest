using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public GameObject gameWinMessageBox;

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
    private static int mineCount;
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
                    mineText.text = GetNeighborMineNum().ToString();
                    if (GetNeighborMineNum() == 0)
                    {
                        CheckNeighborMine();
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                renderer.material.color = Color.yellow;
                CurrentMineStatus = MineStatus.Flag;
                mineCount--;
                if (mineCount == 0)
                {
                    Instantiate(gameWinMessageBox);
                }
            }
        }
    }

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
                    m.mineText.text = m.GetNeighborMineNum().ToString();
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
        mineCount = gameControl.MineNum;
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

    int GetNeighborMineNum()
    {
        GameObject gameController = GameObject.Find("GameController");
        GameControl gameControl = gameController.GetComponent<GameControl>();
        
        int totalNum = 0;
        int maxRow = gameControl.mineArray.GetLength(0);
        int maxColumn = gameControl.mineArray.GetLength(1);
        
        int minX = Row - 1 > 0 ? Row - 1 : 0;
        int maxX = Row + 1 < maxRow ? Row + 1 : maxRow - 1;
        int minY = Column - 1 > 0 ? Column - 1 : 0;
        int maxY = Column + 1 < maxColumn ? Column + 1 : maxColumn - 1;

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                MineControllor m = gameControl.mineArray[i, j];
                if (m.IsMine)
                {
                    totalNum++;
                }
            }
        }

        return totalNum;
    }

    void CheckNeighborMine()
    {
        GameObject gameController = GameObject.Find("GameController");
        GameControl gameControl = gameController.GetComponent<GameControl>();

        int maxRow = gameControl.mineArray.GetLength(0);
        int maxColumn = gameControl.mineArray.GetLength(1);

        bool[,] checkStatus = new bool[maxRow, maxColumn];

        for (int i = 0; i < maxRow; i++ )
        {
            for (int j = 0; j < maxColumn; j++ )
            {
                checkStatus[i, j] = false;
            }
        }

        Stack<MineControllor> mineStack = new Stack<MineControllor>();

        mineStack.Push(gameControl.mineArray[Row, Column]);

        while(mineStack.Count != 0)
        {
            MineControllor m = mineStack.Pop();
            if (checkStatus[m.Row, m.Column] != true)
            {
                checkStatus[m.Row, m.Column] = true;
                int mineNum = m.GetNeighborMineNum();

                if (mineNum == 0)
                {
                    m.CurrentMineStatus = MineStatus.Clear;
                    m.renderer.material.color = Color.blue;
                    m.mineText.text = mineNum.ToString();

                    int minX = m.Row - 1 > 0 ? m.Row - 1 : 0;
                    int maxX = m.Row + 1 < maxRow ? m.Row + 1 : maxRow - 1;
                    int minY = m.Column - 1 > 0 ? m.Column - 1 : 0;
                    int maxY = m.Column + 1 < maxColumn ? m.Column + 1 : maxColumn - 1;

                    for (int i = minX; i <= maxX; i++)
                    {
                        for (int j = minY; j <= maxY; j++)
                        {
                            mineStack.Push(gameControl.mineArray[i, j]);
                        }
                    }
                }
                else
                {
                    m.CurrentMineStatus = MineStatus.Clear;
                    m.renderer.material.color = Color.blue;
                    m.mineText.text = m.GetNeighborMineNum().ToString();
                }
            }
        }
    }
}
