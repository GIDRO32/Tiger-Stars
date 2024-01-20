using System.Collections.Generic;
using UnityEngine;

public class StarSkin : MonoBehaviour
{
    public List<GameObject> Stars; // List of all star GameObjects

    void Start()
    {
        UpdateStarSkins(); // Initial update
    }

    void Update()
    {
        // Optionally, check for changes in the selected skin and update if necessary
        // This can be optimized based on how often skin changes occur
        UpdateStarSkins();
    }

    private void UpdateStarSkins()
    {
        Sprite currentSkin = Profile.Instance.SkinList[Profile.Instance.newSelectedIndex].Image;

        foreach (var star in Stars)
        {
            var spriteRenderer = star.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = currentSkin;
            }
        }
    }
}
