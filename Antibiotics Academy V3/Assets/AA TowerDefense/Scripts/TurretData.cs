using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TurretLevel                    //turret level class that holds the turret properties 
{
    public int cost;
    public GameObject visualization;
    public GameObject bullet;
    public float fireRate;
}

public class TurretData : MonoBehaviour
{

    public List<TurretLevel> levels;        //list of the different turret levels
    private TurretLevel currentLevel;       //current turret level


    //1
    public TurretLevel CurrentLevel       
    {
        //2
        get
        {
            return currentLevel;
        }
        //3
        set
        {
            currentLevel = value;           //set current level to the value
            int currentLevelIndex = levels.IndexOf(currentLevel);   //currentLevelindex is set to the levels index 

            GameObject levelVisualization = levels[currentLevelIndex].visualization;  //sets the gameObject visualisation to the current level's visualisation
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualization != null)                                       //if levelVisualizaiton is not null
                {
                    if (i == currentLevelIndex)                                       //if i is equal to the current level index
                    {
                        levels[i].visualization.SetActive(true);                      //set the level visualization to active
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false);
                    }
                }
            }
        }
    }


    void OnEnable()    
    {
        CurrentLevel = levels[0];      //sets the currentlevel to level 0
    }

    public TurretLevel getNextLevel()                           //function to see if there is a next level
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);   //curentLevelIndex is set to the index of current level
        int maxLevelIndex = levels.Count - 1;                   //maxLevelIndex is set to the total level count - 1
        if (currentLevelIndex < maxLevelIndex)                  //if currentLevelIndex is less than maxLevelIndex
        {
            return levels[currentLevelIndex + 1];               //return the next level
        }
        else
        {
            return null;                                        //otherwise, return null
        }
    }

    public void increaseLevel()                                 //functino to increase level
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);   //curentLevelIndex is set to the index of current level
        if (currentLevelIndex < levels.Count - 1)               //if currentLevelIndex is less than levels count - 1
        {
            CurrentLevel = levels[currentLevelIndex + 1];        //increase current level
        }
    }

}
