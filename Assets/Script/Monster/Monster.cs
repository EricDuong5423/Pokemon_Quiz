using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{ 
    public MonsterBase Base{get; set;}
    public int Level{get; set;}

    public int HP { get; set; }
    public List<Move> Moves { get; set; } 
    public List<Question> Questions { get; set; }

    public Monster(MonsterBase pBase, int pLevel)
    {
        this.Base = pBase;
        this.Level = pLevel;
        HP = MaxHP;

        //Add moves
        Moves = new List<Move>();
        foreach (var move in Base.LearnAbleMoves)
        {
            if(move.Level <= this.Level)
            {
                Moves.Add(new Move(move.Base));
            }

            if(Moves.Count >= 4)
            {
                break;
            }
        }

        Questions = new List<Question>();
        foreach (var question in Base.LearnAbleQuestions)
        {
            if(question.Level <= this.Level)
            {
                Questions.Add(new Question(question.Base));
            }
        }
    }

    public int Attack {get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; } }
    public int Defense { get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; } }
    public int MaxHP { get { return Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10; } }
    public int SpAttack { get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; } }
    public int SpDefense { get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; } }
    public int Speed { get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; } }
}
