using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;

    private bool canAttack = true;
    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private enum State
    {
        Roaming,
        Attacking
    }

    private State state;
    private MagicianPathFinding enemyPathfinding;
    private Transform playerTransform;

    private void Awake()
    {
        enemyPathfinding = GetComponent<MagicianPathFinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        Roaming();
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void ChangeState()
    {
        state = (state == State.Roaming) ? State.Attacking : State.Roaming;
        enemyPathfinding.attackChange();
    }
}
