using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int HP { get; private set; }
    public AttackType CurrentAttack { get; private set; }
    public void StartInizialisation(int hp) {
        HP = hp;
        int random = ((int)Random.Range(0f, 30f)%3);
        CurrentAttack = random switch {
            0 => AttackType.Sword,
            1 => AttackType.Shield,
            3 => AttackType.Fireball,
            _ => AttackType.Shield,
        };
        
    }
    public abstract void Abillity();

    public void ReduceHP(int damage) {
        HP -= damage;
    }
}
