using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class Piece : MonoBehaviour
    {
        [Header("Board Variables")]
        public int column;                    //the column position
        public int row;                       //the row position  
        public int previousColumn;            //the previous column position      
        public int previousRow;               //the row column position
        public int targetX;
        public int targetY;
        public bool isMatched = false;        //bool to see if the piece is matched

        private FindMatch findMatch;
        private Board board;
        private GameObject otherPiece;

        private Vector2 firstPosition;
        private Vector2 finalPosition;
        private Vector2 tempPosition;
        public float swipeAngle = 0;
        public float swipeResist = 1f;

        // Start is called before the first frame update
        void Start()
        {
            board = FindObjectOfType<Board>();          //find board gameobject
            findMatch = FindObjectOfType<FindMatch>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isMatched)
            {
                SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
                mySprite.color = new Color(0f, 0f, 0f, .2f);
            }
            targetX = row;                              //updates targetX to the row value
            targetY = column;                           //updates targetY to the column value

            if (Mathf.Abs(targetX - transform.position.x) > .1)                             //if target x position and piece x position is greater than .1
            {
                tempPosition = new Vector2(targetX, transform.position.y);                  //sets tempPosition to the targetX, and piece y position
                transform.position = Vector2.Lerp(transform.position, tempPosition, .12f);   //linearly interpolates between the transform position to tempposition by .12f
                if (board.allPieces[row, column] != this.gameObject)
                {
                    board.allPieces[row, column] = this.gameObject;
                }
                findMatch.FindAllMatches();
            }
            else
            {
                tempPosition = new Vector2(targetX, transform.position.y);                  //otherwise, it will set tempPosition to the targetX, and piece y position (targetX didnt change)
                transform.position = tempPosition;                                          //sets transform position to temp position
                board.allPieces[row, column] = this.gameObject;                             //sets the prefab in the array to this game object
            }

            if (Mathf.Abs(targetY - transform.position.y) > .1)
            {
                tempPosition = new Vector2(transform.position.x, targetY);
                transform.position = Vector2.Lerp(transform.position, tempPosition, .12f);
                if (board.allPieces[row, column] != this.gameObject)
                {
                    board.allPieces[row, column] = this.gameObject;
                }
                findMatch.FindAllMatches();
            }
            else
            {
                tempPosition = new Vector2(transform.position.x, targetY);
                transform.position = tempPosition;
                board.allPieces[row, column] = this.gameObject;
            }
        }

        public IEnumerator CheckMove()                                            //coroutine function to check the move of the piece
        {
            yield return new WaitForSeconds(.5f);
            if (otherPiece != null)                                               //if other piece is not null
            {
                if (!isMatched && !otherPiece.GetComponent<Piece>().isMatched)    //if piece is not matched and other piece is not matched
                {
                    otherPiece.GetComponent<Piece>().row = row;                   //resets the otherpiece row back to its original row before the move
                    otherPiece.GetComponent<Piece>().column = column;             //resets the otherpiece column back to its  original column before the move 
                    row = previousRow;                                            //resets this piece row back to its original row 
                    column = previousColumn;                                      //resets this column back to its original column
                    yield return new WaitForSeconds(0.2f);
                    board.currState = GameState.Move;
                }
                else                                                              //otherwise, if the pieces are matched
                {
                    board.DestroyMatches();                                       //call the destroymatches from board
                }
                otherPiece = null;                                                //sets otherpiece to null
            }
        }

        private void OnMouseDown()
        {
            if (board.currState == GameState.Move)
            {
                firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //sets firstPosition to the position of the mousedown
            }
        }

        private void OnMouseUp()
        {
            if (board.currState == GameState.Move)
            {
                finalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //sets finalPosition to the position of the mouseup
                CalculateAngle();                                                      //calculates the angle between the two positions
            }
        }

        void CalculateAngle()                                                      //calculate the angle between the first and final position using arc tangent, and converting from radian to degrees
        {
            if (Mathf.Abs(finalPosition.y - firstPosition.y) > swipeResist || Mathf.Abs(finalPosition.x - firstPosition.x) > swipeResist)
            {
                board.currState = GameState.Wait;
                swipeAngle = Mathf.Atan2(finalPosition.y - firstPosition.y, finalPosition.x - firstPosition.x) * 180 / Mathf.PI;
                //Debug.Log(swipeAngle);
                MovePieces();                                                       //moves the pieces accordingly
            }
            else
            {
                board.currState = GameState.Move;
            }
        }

        void MovePiecesCalc(Vector2 direction)                                                //function to move the piece towards the direction
        {
            otherPiece = board.allPieces[row + (int)direction.x, column + (int)direction.y];
            previousRow = row;                                                                //sets the piece row to previousrow, in case piece does not match
            previousColumn = column;                                                          //sets the piece column to previouscolumn, in case piece does not match
            otherPiece.GetComponent<Piece>().row += -1 * (int)direction.x;                    //updates the row and column according to the direction+
            otherPiece.GetComponent<Piece>().column += -1 * (int)direction.y;
            row += (int)direction.x;
            column += (int)direction.y;
            StartCoroutine(CheckMove());                                                      //start checkmove coroutine
        }

        void MovePieces()                                                          //function to moves the pieces according the angle
        {
            if (swipeAngle > -45 && swipeAngle <= 45 && row < board.width - 1)     //if the angle is greater than -45 and is less than or equals to 45, and row count is less than the width of the board
            {                                                                      //then the piece is being swiped to the right                 
                MovePiecesCalc(Vector2.right);
            }
            else if (swipeAngle > 45 && swipeAngle <= 135 && column < board.height - 1)  //if swipeAngle is greater than 45 and less than or equals to 135, and column count is less than height of the board
            {                                                                          //then the piece is being swiped up

                MovePiecesCalc(Vector2.up);
            }
            else if ((swipeAngle > 135 || swipeAngle <= -135) && row > 0)          //if swipeAngle is greater than 135 or less than or equals to -135, and row count is greater than 0
            {                                                                    //then the piece is being swiped to the left

                MovePiecesCalc(Vector2.left);
            }
            else if (swipeAngle < -45 && swipeAngle >= -135 && column > 0)         //if swipeAngle is less than -45 and greather than or equals to -135, and column count is greater than 0  
            {                                                                    //then the piece is being swiped down         

                MovePiecesCalc(Vector2.down);
            }
            else
            {
                board.currState = GameState.Move;
            }
        }
    }
}