using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float flashDuration = .2f;
    
    private Material _defaultMat;
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
	    _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMat = _spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.material = _defaultMat;
    }

    public float GetFlashDuration()
    {
        return flashDuration;
    }
}
