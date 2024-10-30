using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit player;
    [SerializeField] BattleUnit enemy;
    [SerializeField] BattleHUD playerHUD;
    [SerializeField] BattleHUD enemyHUD;
    [SerializeField] BattleDialogBox dialogBox;

    private void Start(){
        SetupBattle();
    }

    public void SetupBattle(){
        player.Setup();
        playerHUD.SetData(player.Monster);
        enemy.Setup();
        enemyHUD.SetData(enemy.Monster);
        dialogBox.SetDialog($"A monster {enemy.Monster.Base.Name} appear");
    }
}
