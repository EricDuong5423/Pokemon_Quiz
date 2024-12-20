using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] MonsterBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayer;

    public Monster Monster {get; set;}

    public void Setup(){
        Monster = new Monster(_base, level);
        if(isPlayer){
            GetComponent<Image>().sprite = Monster.Base.BackSprite;
        }
        else{
            GetComponent<Image>().sprite = Monster.Base.FrontSprite;
            
        }
    }
}
