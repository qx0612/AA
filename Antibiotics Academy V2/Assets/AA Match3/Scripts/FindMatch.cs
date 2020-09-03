using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class FindMatch : MonoBehaviour
    {
        private Board board;
        public List<GameObject> currMatches = new List<GameObject>();
        void Start()
        {
            board = FindObjectOfType<Board>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FindAllMatches()
        {
            StartCoroutine(FindAllMatchesCo());
        }

        private IEnumerator FindAllMatchesCo()
        {
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < board.width; i++)                                   //loops through all the positions
            {
                for (int j = 0; j < board.height; j++)
                {
                    GameObject thisPiece = board.allPieces[i, j];                   //get reference to a piece in the i and j position 
                    if (thisPiece != null)                                          //if the piece is not null
                    {
                        if (i > 0 && i < board.width - 1)                           //checks the pieces in the width
                        {
                            GameObject leftPiece = board.allPieces[i - 1, j];       //get reference to the left piece
                            GameObject rightPiece = board.allPieces[i + 1, j];      //get reference to the right piece
                            if (leftPiece != null && rightPiece != null)            //if both are not null
                            {
                                if (leftPiece.CompareTag(thisPiece.tag) && rightPiece.CompareTag(thisPiece.tag)) //if their tags are the same
                                {
                                    if (!currMatches.Contains(leftPiece))             //add the pieces to currMatches if they're not in it, change all the isMatched bools for the 3 pieces to true
                                    {
                                        currMatches.Add(leftPiece);
                                    }
                                    leftPiece.GetComponent<Piece>().isMatched = true;

                                    if (!currMatches.Contains(rightPiece))
                                    {
                                        currMatches.Add(rightPiece);
                                    }
                                    rightPiece.GetComponent<Piece>().isMatched = true;
                                    if (!currMatches.Contains(thisPiece))
                                    {
                                        currMatches.Add(thisPiece);
                                    }
                                    thisPiece.GetComponent<Piece>().isMatched = true;
                                }
                            }
                        }

                        if (j > 0 && j < board.height - 1)
                        {
                            GameObject upPiece = board.allPieces[i, j + 1];        //get reference to the up piece
                            GameObject downPiece = board.allPieces[i, j - 1];      //get reference to the bottom piece
                            if (upPiece != null && downPiece != null)              //if both are not null      
                            {
                                if (upPiece.CompareTag(thisPiece.tag) && downPiece.CompareTag(thisPiece.tag))  //if their tags are the same
                                {
                                    if (!currMatches.Contains(upPiece))            //add the pieces to currMatches if they're not in it, change all the isMatched bools for the 3 pieces to true 
                                    {
                                        currMatches.Add(upPiece);
                                    }
                                    upPiece.GetComponent<Piece>().isMatched = true;
                                    if (!currMatches.Contains(downPiece))
                                    {
                                        currMatches.Add(downPiece);
                                    }
                                    downPiece.GetComponent<Piece>().isMatched = true;
                                    if (!currMatches.Contains(thisPiece))
                                    {
                                        currMatches.Add(thisPiece);
                                    }
                                    thisPiece.GetComponent<Piece>().isMatched = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

