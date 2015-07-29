using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerCurcularList {

    const int STARTING_INDEX = 0;
    List<GameObject> playerList;
    int totalPlayers = STARTING_INDEX;
    int currentIndex = STARTING_INDEX;

    public PlayerCurcularList()
    { 
        playerList = new List<GameObject>();
    }

    public void Add (ref GameObject player)
    {
        if (currentIndex < totalPlayers)
        {
            for (int i = totalPlayers; i >= currentIndex; i--)
            {
                GameObject temp;
                temp = playerList[i];
                playerList.RemoveAt(i);
                playerList.Insert(i + 1, temp);
            }
            playerList.Insert(currentIndex, player);
            totalPlayers++;
        }
        else
        { 
            playerList.Add(player);
            totalPlayers++;
        }
    }//End Add

    public GameObject RemoveCurrent()
    {
        GameObject temp = playerList[currentIndex];
        playerList.RemoveAt(currentIndex);
        
        //playerList.Sort();
        return temp;
    }


    public GameObject RemoveAtIndex(int index)
    {
        GameObject temp = playerList[index];
        playerList.RemoveAt(index);

        //playerList.Sort();
        return temp;
    }

    public GameObject GetCurrent()
    {
        return playerList[currentIndex];

    }

    public GameObject GetAtIndex(int index)
    {
        return playerList[index];

    }

    public void MoveToNext()
    {
        currentIndex++;
        if (currentIndex > totalPlayers)
            currentIndex = STARTING_INDEX;
    }

    public PlayerContoller GetController()
    {
        return playerList[currentIndex].GetComponent<PlayerContoller>();
    }
    

    
}
