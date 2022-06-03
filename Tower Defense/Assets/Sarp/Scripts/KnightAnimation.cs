using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimation : MonoBehaviour
{
    private Animator _animator;
    private EnemyS _enemy;
    private EnemyHealthS _enemyHealth;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<EnemyS>();
        _enemyHealth = GetComponent<EnemyHealthS>();
    }

    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("take_hit");
    }

    private void PlayDieAnimation()
    {
        _animator.SetTrigger("dying");
    }

    private float GetCurrentAnimationLenght()
    {
        float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLenght;
    }

    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() / 2.0f);
        _enemy.ResumeMovement();
    }

    private IEnumerator PlayDead()
    {
        _enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() / 1.0f);
        _enemy.ResumeMovement();
        _enemyHealth.ResetHealth();
        ObjectPoolerS.ReturnToPool(_enemy.gameObject);
    }

    private void EnemyHit(EnemyS enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    private void EnemyDead(EnemyS enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayDead());
        }
    }

    private void OnEnable()
    {
        EnemyHealthS.OnEnemyHit += EnemyHit;
        EnemyHealthS.OnEnemyKilled += EnemyDead;
    }

    private void OnDisable()
    {
        EnemyHealthS.OnEnemyHit -= EnemyHit;
        EnemyHealthS.OnEnemyKilled -= EnemyDead;
    }
}
