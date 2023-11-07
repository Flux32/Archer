using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _spriteRenderer.enabled = false;    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _spriteRenderer.enabled = true;
    }
}
