using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit player;
    [SerializeField] BattleHUD playerHUD;

    private void Start(){
        SetupBattle();
    }

    public void SetupBattle(){
        player.Setup();
        playerHUD.SetData(player.Monster);
    }
}
