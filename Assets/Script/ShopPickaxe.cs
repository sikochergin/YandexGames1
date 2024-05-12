using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPickaxe : MonoBehaviour
{
    [SerializeField] public int[] Price;
    [SerializeField] public int[] Skill;
    private int startPrice = 20;
    private int startSkill = 2;



    private void Awake()
    {
        for (int i = 0; i<25; i++)
        {
            if (i <= 17)
            {
                Price[i] = (startSkill * startPrice) / 2;
                Skill[i] = startSkill;
                startSkill *= 2;
                startPrice++;
            }
            else
            {
                Price[i] = (startSkill * startPrice) / 2;
                Skill[i] = startSkill;
                startSkill *= 2;
                startPrice+=3;
            }
        }
    }
}
