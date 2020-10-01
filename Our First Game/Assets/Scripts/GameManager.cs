using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

enum GameColor
{
    White, Black
}

public class GameManager : MonoBehaviour
{
    public GameObject brightCellPrefab;
    public GameObject darkCellPrefab;
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;

    public Piece[,] pieces = new Piece[8, 8];
    private Piece selectedPiece;
    private Vector2 mouseOver;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game started");
        InitPieces();
    }

    private void Update()
    {
        UpdateMouseOver();
        if (Input.GetMouseButtonDown(0))
            SelectPiece((int)mouseOver.x, (int)mouseOver.y);
        if (selectedPiece != null)
            UpdatePieceDrag(selectedPiece);
    }

    void UpdateMouseOver()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
            out hit,
            25.0f)) {
            mouseOver.x = Mathf.Floor(hit.point.x) + 4;
            mouseOver.y = Mathf.Floor(hit.point.z) + 4;
        }
        else
        {
            mouseOver.x = -1;
            mouseOver.y = -1;
        }
    }

    void UpdatePieceDrag(Piece p)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
            out hit,
            25.0f))
        {
            Debug.Log(hit.point);
            p.transform.position = hit.point + new Vector3(0, 1 - hit.point.y, 0);
        }
    }

    void InitPieces()
    {
        //init board
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
                InitCell(x, y);
        //init white
        for (int y = 0; y < 3; y++)
            for (int x = y % 2 == 0 ? 0 : 1; x < 8; x += 2)
                InitPiece(x, y, GameColor.White);
        //init black
        for (int y = 7; y >= 5; y--)
            for (int x = y % 2 == 0 ? 0 : 1; x < 8; x += 2)
                InitPiece(x, y, GameColor.Black);
    }

    private Vector2 GetCenterLoc(int x, int y)
    {
        return new Vector2(x - 4 + 0.5f, y - 4 + 0.5f);
    }

    void InitCell(int x, int y)
    {
        GameObject cell = Instantiate<GameObject>((x + y) % 2 == 0 ? brightCellPrefab : darkCellPrefab, 
            transform);
        Vector2 center = GetCenterLoc(x, y);
        float yPos = whitePiecePrefab.transform.localScale.y + brightCellPrefab.transform.localScale.y;
        cell.transform.position = Vector3.right * center.x + Vector3.forward * center.y + Vector3.down * yPos;
    }

    void InitPiece(int x, int y, GameColor gameColor)
    {
        GameObject go = Instantiate<GameObject>(gameColor == GameColor.White ? whitePiecePrefab : blackPiecePrefab,
            transform);
        Piece p = go.GetComponent<Piece>();
        pieces[x, y] = p;
        MovePiece(p, x, y);
    }

    void MovePiece(Piece p, int x, int y)
    {
        Vector2 center = GetCenterLoc(x, y);
        p.transform.position = Vector3.right * center.x + Vector3.forward * center.y;
    }

    bool PointIsLegal(float x, float y)
    {
        return 0 <= x && x < pieces.Length && 0 <= y && y < pieces.Length;
    }

    void SelectPiece(int x, int y)
    {
        if (!PointIsLegal(x, y))
        {
            Debug.LogError("ILLEGAL POINT: (" + x + ", " + y + ")");
            return;
        }
        selectedPiece = pieces[x, y];
        if (selectedPiece == null)
            return;

        Debug.Log(selectedPiece.name);
    }
}
