using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public GameColor color { get; set; }
    public bool isKing { get; set; }

    public bool IsValidMove(Piece[,] board, int x1, int y1, int x2, int y2)
    {
        // Dest empty
        if (board[x2, y2] != null)
            return false;
        // Diagonal move
        int deltaMoveX = Mathf.Abs(x1 - x2), deltaMoveY = Mathf.Abs(y1 - y2);
        if (deltaMoveX != deltaMoveY)
            return false;
        // Non zero move
        if (deltaMoveX == 0)
            return false;
        // Eating
        if (deltaMoveX == 2)
        {
            int eatenCellX = (x1 + x2) / 2, eatenCellY = (y1 + y2) / 2;
            Piece eatenCell = board[eatenCellX, eatenCellY];
            //Piece of different color
            if (eatenCell == null)
                return false;
            if (IsSameColor(eatenCell))
                return false;
        }
        // Move amount
        if (!isKing) // Regular
        {
            // Move direction
            if (color == GameColor.White && !(y2 > y1))
                return false;
            if (color == GameColor.Black && !(y2 < y1))
                return false;
        }
        return true;
    }

    public bool IsSameColor(Piece p)
    {
        return p != null && this.color == p.color;
    }

}
