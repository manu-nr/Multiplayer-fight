using System.Collections;
using UnityEngine;

public class PlayerCombact : MonoBehaviour
{
    [SerializeField] private bool _isAttacking;

    public void Attack(bool attack)
    {
        StartCoroutine(AttackRoutine());
    }


    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        CheckForEnemy();
    }


    private bool CheckForEnemy()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        float range = 10f;
        if(Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if(hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
