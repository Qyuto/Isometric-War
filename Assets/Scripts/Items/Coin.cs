using Interface;
using UnityEngine;


public class Coin : MonoBehaviour
{
    [SerializeField] private int dropValue;

    public void ChangeDropValue(int value)
    {
        dropValue = value;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        IFunded funded = col.GetComponentInParent<IFunded>();
        if (funded == null) return;

        funded.AddMoney(dropValue);
        Destroy(gameObject);
    }
}