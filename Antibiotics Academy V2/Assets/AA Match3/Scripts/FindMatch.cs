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
            for (int i = 0; i < board.width; i++)
            {
                for (int j = 0; j < board.height; j++)
                {
                    GameObject thisPiece = board.allPieces[i, j];
                    if (thisPiece != null)
                    {
                        if (i > 0 && i < board.width - 1)
                        {
                            GameObject leftPiece = board.allPieces[i - 1, j];
                            GameObject rightPiece = board.allPieces[i + 1, j];
                            if (leftPiece != null && rightPiece != null)
                            {
                                //if (leftPiece.tag == thisPiece.tag && rightPiece.tag == thisPiece.tag)
                                if (leftPiece.CompareTag(thisPiece.tag) && rightPiece.CompareTag(thisPiece.tag))
                                {
                                    if (!currMatches.Contains(leftPiece))
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
                            GameObject upPiece = board.allPieces[i, j + 1];
                            GameObject downPiece = board.allPieces[i, j - 1];
                            if (upPiece != null && downPiece != null)
                            {
                                //if (upPiece.tag == thisPiece.tag && downPiece.tag == thisPiece.tag)
                                if (upPiece.CompareTag(thisPiece.tag) && downPiece.CompareTag(thisPiece.tag))
                                {
                                    if (!currMatches.Contains(upPiece))
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

