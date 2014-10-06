using UnityEngine;
using System.Collections;

[System.Serializable]
public class MineGrid
{
    public int Row, Column;
}
public class GameControl : MonoBehaviour {

    public MineControllor mine;
    
    public int MineNum;
    public MineGrid mineGrid;

    public MineControllor[,] mineArray;

    void Start () {
        mineArray = new MineControllor[mineGrid.Row, mineGrid.Column];
        GenerateMine();    
    }
    
    void GenerateMine()
    {
        float rowStartPos = mineGrid.Row / 2 * (-1);
        float columnStartPos = mineGrid.Column/2;

        float rowPos = rowStartPos;
        float columnPos = columnStartPos;
        for (int i = 0; i < mineGrid.Row; i++ )
        {
            for(int j = 0; j < mineGrid.Column; j++)
            {
                Vector3 pos = new Vector3(rowPos, columnPos, 0.0f);
                Quaternion rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                MineControllor m = Instantiate(mine, pos, rotation) as MineControllor;
                m.IsMine = false;
                m.Row = i;
                m.Column = j;
                mineArray[i, j] = m;
                rowPos += 1;
            }
            columnPos -= 1;
            rowPos = rowStartPos;
        }
    }

}
