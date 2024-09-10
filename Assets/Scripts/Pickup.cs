using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player picked up the item.");

            GameManager.Instance.OnHPChanged(5f);
            GameManager.Instance.CheckWinCondition();

            Destroy(gameObject);
        }
    }
}
