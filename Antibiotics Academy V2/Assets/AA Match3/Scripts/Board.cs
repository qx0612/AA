using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public enum GameState
    {
        Wait,
        Move
    }

    public class Board : MonoBehaviour
    {
        public GameState currState = GameState.Move;
        public int width;                           //width of the board
        public int height;                          //height of the board
        public int offSet;
        public GameObject tilePrefab;
        public GameObject[] piecePrefabs;
        public GameObject[,] allPieces;
        private FindMatch findMatch;
        private HealthManager healthManager;

        private AudioSource matchingAudio;

        void Start()
        {
            matchingAudio = GetComponent<AudioSource>();

            Time.timeScale = 0f;
            healthManager = FindObjectOfType<HealthManager>();
            findMatch = FindObjectOfType<FindMatch>();
            allPieces = new GameObject[width, height];
            SetUp();
        }

        private void SetUp()                                     //function to set up the board at the start
        {
            for (int i = 0; i < width; i++)                      //iterate through the width and height
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 tempPosition = new Vector2(i, j + offSet);                                                     //sets tempposition to Vector2(i, j)
                    Vector2 tempPosition1 = new Vector2(i, j);
                    GameObject backgroundTile = Instantiate(tilePrefab, tempPosition1, Quaternion.identity) as GameObject;  //instantiate tilePrefab at temp position, which is under backgroundTile game object
                    backgroundTile.transform.parent = this.transform;                                                      //sets backgroundtile gameobject parent as the board gameobject
                    backgroundTile.name = "( " + i + "," + j + " )";                                                       //sets backgroundtile name as their position on the board
                    int indexOfPiece = Random.Range(0, piecePrefabs.Length);                                               //sets index of piece to a random range between 0 and the legnth of the prefabs array
                    int maxIterations = 0;                                                                                 //max iteration = 0
                    while (MatchesAt(i, j, piecePrefabs[indexOfPiece]) && maxIterations < 100)                              //while the piece is equal to the pieces to the left, right, up and down
                    {
                        indexOfPiece = Random.Range(0, piecePrefabs.Length);                                               //resets the indexofpiece to a new one
                        maxIterations++;                                                                                   //increases max iteration
                    }
                    maxIterations = 0;                                                                                     //once matchesat returns false, while loop stops and maxiteration is set to 0
                    GameObject piece = Instantiate(piecePrefabs[indexOfPiece], tempPosition, Quaternion.identity);         //instantiate piece at temp position, which is under piece game object
                    piece.GetComponent<Piece>().row = i;
                    piece.GetComponent<Piece>().column = j;

                    piece.transform.parent = this.transform;                                                               //sets piece gameobject parent as board gameobject 
                                                                                                                           //piece.name = "( " + i + "," + j + " )"; ;                                                              //sets piece name to their position on the board
                    allPieces[i, j] = piece;                                                                              //sets allPrefabs position array to piece
                }
            }
        }

        private bool MatchesAt(int row, int column, GameObject piece)                                                      //bool function to return true or false if the other pieces to the left, right, up and down are the same as this piece
        {
            if (row > 1 && column > 1)
            {
                //if (allPieces[row - 1, column].tag == piece.tag && allPieces[row - 2, column].tag == piece.tag)
                if (allPieces[row - 1, column].CompareTag(piece.tag) && allPieces[row - 2, column].CompareTag(piece.tag))
                {
                    return true;
                }
                //if (allPieces[row, column - 1].tag == piece.tag && allPieces[row, column - 2].tag == piece.tag)
                if (allPieces[row, column - 1].CompareTag(piece.tag) && allPieces[row, column - 2].CompareTag(piece.tag))
                {
                    return true;
                }
            }
            else if (column <= 1 || row <= 1)
            {
                if (column > 1)
                {
                    //if (allPieces[row, column - 1].tag == piece.tag && allPieces[row, column - 2].tag == piece.tag)
                    if (allPieces[row, column - 1].CompareTag(piece.tag) && allPieces[row, column - 2].CompareTag(piece.tag))
                    {
                        return true;
                    }
                }
                if (row > 1)
                {
                    //if (allPieces[row - 1, column].tag == piece.tag && allPieces[row - 2, column].tag == piece.tag)
                    if (allPieces[row - 1, column].CompareTag(piece.tag) && allPieces[row - 2, column].CompareTag(piece.tag))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void DestroyMatchesAt(int row, int column)                        //function to destroy
        {
            if (allPieces[row, column].GetComponent<Piece>().isMatched)          //if the piece on that row, column isMatched
            {
                findMatch.currMatches.Remove(allPieces[row, column]);
                Destroy(allPieces[row, column]);                                 //destroy the piece
                healthManager.CalcAddHealth(allPieces[row, column].tag);
                allPieces[row, column] = null;                                   //sets the destroyed piece's position in the array to null
            }
        }

        public void DestroyMatches()                                               //function to iterate through the pieces to check if they should be destroyed
        {
            for (int i = 0; i < width; i++)                                        //iterate through all the pieces on the board
            {
                for (int j = 0; j < height; j++)
                {
                    if (allPieces[i, j] != null)                                    //if the piece is not null
                    {
                        DestroyMatchesAt(i, j);                                    //call DestroyMatchesAt 
                        matchingAudio.Play();
                    }
                }
            }
            StartCoroutine(DecreaseColumn());                                      //call coroutine to collapse the pieces
        }

        private IEnumerator DecreaseColumn()                                       //coroutine function to collapse the pieces
        {
            int nullCount = 0;                                                     //null counter
            for (int i = 0; i < width; i++)                                        //iterate through all the pieces
            {
                for (int j = 0; j < height; j++)
                {
                    if (allPieces[i, j] == null)                                     //if the piece is null
                    {
                        nullCount++;                                                //null count increases
                    }
                    else if (nullCount > 0)                                         //if the nullcount is greater than 0
                    {
                        allPieces[i, j].GetComponent<Piece>().column -= nullCount; //sets the prefabs' column to minus the nullcount
                        allPieces[i, j] = null;                                    //sets that original prefabs position to null
                    }
                }
                nullCount = 0;                                                      //reset null counter
            }
            yield return new WaitForSeconds(.4f);
            StartCoroutine(FillBoard());
        }

        private void RefillBoard()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (allPieces[i, j] == null)
                    {
                        Vector2 tempPosition = new Vector2(i, j + offSet);
                        int prefabIndex = Random.Range(0, piecePrefabs.Length);
                        GameObject piece = Instantiate(piecePrefabs[prefabIndex], tempPosition, Quaternion.identity);
                        allPieces[i, j] = piece;
                        piece.GetComponent<Piece>().row = i;
                        piece.GetComponent<Piece>().column = j;
                    }
                }
            }
        }

        private bool MatchesOnBoard()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (allPieces[i, j] != null)
                    {
                        if (allPieces[i, j].GetComponent<Piece>().isMatched)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private IEnumerator FillBoard()
        {
            RefillBoard();
            yield return new WaitForSeconds(.5f);
            while (MatchesOnBoard())
            {
                yield return new WaitForSeconds(.5f);
                DestroyMatches();
            }
            yield return new WaitForSeconds(.5f);
            currState = GameState.Move;
        }
    }

}
