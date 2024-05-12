using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAutoMiner : MonoBehaviour
{
    [SerializeField] public int[] Price;
    [SerializeField] public int[] Skill;
    private int startPrice = 50;
    private int startSkill = 1;



    private void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            if (i <= 15) 
            {
                Price[i] = (startSkill * startPrice);
                Skill[i] = startSkill;
                startSkill *= 2;
                startPrice += 3;
            }
            else 
            {
                Price[i] = (startSkill * startPrice);
                Skill[i] = startSkill;
                startSkill *= 2;
                startPrice += 5;
            }
            
        }
    }
}
