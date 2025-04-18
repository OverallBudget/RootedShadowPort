using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour, IPointerClickHandler
{
    [Header("Sprite Swap Settings")]
    [SerializeField] private RawImage displayImage; // The RawImage component to display the active sprite
    [SerializeField] private Texture sprite1;       // Texture for the first sprite
    [SerializeField] private Texture sprite2;       // Texture for the second sprite
    [SerializeField] private bool isSprite1Active = true;

    void Awake()
    {
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        // Set the texture of the RawImage to the active sprite
        displayImage.texture = isSprite1Active ? sprite1 : sprite2;
    }

    public void Toggle()
    {
        // Toggle the active sprite
        isSprite1Active = !isSprite1Active;
        UpdateSprite();
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle click event to toggle the sprite
        Toggle();
        
    }

    
}
