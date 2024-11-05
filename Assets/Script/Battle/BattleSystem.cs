using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy, QuestionAnswer};

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit player;
    [SerializeField] BattleUnit enemy;
    [SerializeField] BattleHUD playerHUD;
    [SerializeField] BattleHUD enemyHUD;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] MonsterQuestion monsterQuestion;

    BattleState state;
    int currAction;
    int currMove;

    private void Start(){
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle(){
        player.Setup();
        playerHUD.SetData(player.Monster);
        enemy.Setup();
        enemyHUD.SetData(enemy.Monster);

        dialogBox.SetMoveNames(player.Monster.Moves);
        yield return dialogBox.TypeDialog($"A monster {enemy.Monster.Base.Name} appear");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    void QuestionAnswer()
    {
        state = BattleState.QuestionAnswer;
        monsterQuestion.EnableMonsterQuestion(true);
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableAnswerSelector(true);
    }

    private void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            handlePlayerAction();
        }
        else if (state == BattleState.PlayerMove)
        {
            handlePlayerMove();
        }
        else if (state == BattleState.QuestionAnswer) {
            handlePlayerQuestionAnswer();
        }
    }

    void handlePlayerQuestionAnswer()
    {

    }

    void handlePlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currMove < player.Monster.Moves.Count - 2)
            {
                currMove += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currMove > 1)
            {
                currMove -= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currMove < player.Monster.Moves.Count - 1)
            {
                ++currMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currMove > 0)
            {
                --currMove;
            }
        }

        dialogBox.UpdateMoveSelection(currMove, player.Monster.Moves[currMove]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            QuestionAnswer();
        }
    }

    void handlePlayerAction()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currAction < 1)
            {
                ++currAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currAction > 0)
            {
                --currAction;
            }
        }
        dialogBox.UpdateActionSelection(currAction);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(currAction == 0)
            {
                //Fight
                PlayerMove();
            }
            else if( currAction == 1)
            {
                //Run
            }
        }
    }
}
