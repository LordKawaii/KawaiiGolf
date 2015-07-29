using UnityEngine;
using System.Collections;

public class PlayerStates
{
    public bool thisPlayersTurn = false;
    public bool isTakingShot = false;
    public bool isSettingPower = false;
    public bool isSettingSpin = false;
    public bool isStoped = true;
    public bool isFirstFrameOfTurn = true;

    public PlayerStates()
    { 

    }

    //Mutators
    public void SetThisPlayersTurn(bool state)
    {
        thisPlayersTurn = state;
    }

    public void SetIsTakingShot(bool state)
    {
        isTakingShot = state;
    }

    public void SetIsSettingPower(bool state)
    {
        isSettingPower = state;
    }

    public void SetIsSettingSpin(bool state)
    {
        isSettingSpin = state;
    }

    public void SetIsStoped(bool state)
    {
        isStoped = state;
    }

    public void SetIsFirstFrame(bool state)
    {
        isFirstFrameOfTurn = state;
    }
    //End Mutators



    //Getters
    public bool GetThisPlayersTurn()
    {
        return thisPlayersTurn;
    }

    public bool GetIsTakingShot()
    {
        return isTakingShot;
    }

    public bool GetIsSettingPower()
    {
        return isSettingPower;
    }

    public bool GetIsSettingSpin()
    {
        return isSettingSpin;
    }

    public bool GetIsStoped()
    {
        return isStoped;
    }

    public bool GetIsFirstFrame()
    {
       return isFirstFrameOfTurn;
    }
    //End Getters
}
