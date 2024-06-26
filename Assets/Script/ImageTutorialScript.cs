using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{
    public Image displayImage; // Reference to the UI Image component that displays the slideshow image
    public Sprite[] images; // Array of images for the slideshow
    private int currentIndex = 0; // Index to keep track of the current image

    void Start()
    {
        // Display the first image
        if (images.Length > 0)
        {
            displayImage.sprite = images[currentIndex];
        }
    }

    // Function to show the next image
    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % images.Length;
        displayImage.sprite = images[currentIndex];
    }

    // Function to show the previous image
    public void PreviousImage()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = images.Length - 1;
        }
        displayImage.sprite = images[currentIndex];
    }
}
