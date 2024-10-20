using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{ 
    MonsterBase _base;
    int level;

    public int HP { get; set; }
    public List<Move> Moves { get; set; } 
    public List<Question> Questions { get; set; }

    public Monster(MonsterBase _base, int level)
    {
        this._base = _base;
        this.level = level;
        HP = _base.MaxHP;

        //Add moves
        Moves = new List<Move>();
        foreach (var move in _base.LearnAbleMoves)
        {
            if(move.Level <= this.level)
            {
                Moves.Add(new Move(move.Base));
            }

            if(Moves.Count >= 4)
            {
                break;
            }
        }

        Questions = new List<Question>();
        foreach (var question in _base.LearnAbleQuestions)
        {
            if(question.Level <= this.level)
            {
                Questions.Add(new Question(question.Base));
            }
        }
    }

    public int Attack {get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; } }
    public int Defense { get { return Mathf.FloorToInt((_base.Defense * level) / 100f) + 5; } }
    public int MaxHP { get { return Mathf.FloorToInt((_base.MaxHP * level) / 100f) + 10; } }
    public int SpAttack { get { return Mathf.FloorToInt((_base.SpAttack * level) / 100f) + 5; } }
    public int SpDefense { get { return Mathf.FloorToInt((_base.SpDefense * level) / 100f) + 5; } }
    public int Speed { get { return Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; } }
}
