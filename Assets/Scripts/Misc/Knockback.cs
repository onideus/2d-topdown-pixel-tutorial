using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; }

    [SerializeField] private float knockBackTime = .2f;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockbackThrust)
    {
        gettingKnockedBack = true;
        var difference = (transform.position - damageSource.position).normalized * 
                         knockbackThrust * _rigidbody2D.mass;
        _rigidbody2D.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockBackRoutine());
    }

    private IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        _rigidbody2D.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
