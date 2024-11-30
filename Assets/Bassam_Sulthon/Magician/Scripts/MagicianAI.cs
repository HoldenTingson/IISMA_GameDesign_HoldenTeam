using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianAI : MonoBehaviour
{
    [SerializeField] private float _roamChangeDirFloat = 2f;
    private enum State
    {
        Roaming,
        Stop
    }

    private State _state;
    private MagicianPathFinding _enemyPathfinding;

    private void Awake()
    {
        _enemyPathfinding = GetComponent<MagicianPathFinding>();
        _state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (_state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            _enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(_roamChangeDirFloat);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void ChangeState()
    {
        _state = (_state == State.Roaming) ? State.Stop : State.Roaming;
        _enemyPathfinding.attackChange();
    }
}
