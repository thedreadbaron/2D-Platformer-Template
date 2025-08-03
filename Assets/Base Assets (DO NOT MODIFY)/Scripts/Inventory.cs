using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int pickupCount = 0;
    public AudioSource pickupSound;
    public AudioSource powerupSound;
    public TMPro.TextMeshProUGUI pickupText;
    public Animator pickupAnimator;

    public void PickupCountIncrease(int count)
    {
        pickupCount += count;
        pickupText.text = pickupCount.ToString();
        pickupAnimator.SetTrigger("Reveal");
    }
}
