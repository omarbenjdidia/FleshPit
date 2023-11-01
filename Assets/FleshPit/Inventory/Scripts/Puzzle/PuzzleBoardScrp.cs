using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoardScrp : MonoBehaviour
{
    public Transform piecePrefab;
    public Transform boardTransform;

    public int size = 3;
    int emptyCell = 0;
    private List<Transform> pieces = new List<Transform>();

    [ContextMenu("Create Board")]
    void CreateBoard()
    {
        float gap = .05f;
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, boardTransform);

                pieces.Add(piece);

                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, 0, +1 - (2 * width * row) - width);
                piece.localScale = ((2 * width) - gap) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                if ((row == size - 1) && (col == size - 1))
                {
                    emptyCell = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    float gap2 = gap / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];

                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap2));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap2));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap2));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap2));

                    mesh.uv = uv;
                }

            }

        }
    }

    bool checkCorrect()
    {
        for (int i = 0; i < size * size; i++)
            if (pieces[i].name != $"{i}")
                return false;
        
        return true;
    }


    // Start is called before the first frame update
    void Start()
    {
        boardTransform = GetComponent<Transform>();
        CreateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Go through the list, the index tells us the position.
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        // Check each direction to see if valid move.
                        // We break out on success so we don't carry on and swap back again.
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) { break; }
                    }
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Response: "+ checkCorrect());
        }
    }

    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyCell))
        {
            // Swap them in game state.
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            // Swap their transforms.
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));

            // Update empty location.
            emptyCell = i;
            return true;
        }
        return false;
    }
}