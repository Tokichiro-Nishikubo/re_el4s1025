using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private bool _onece = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !_onece)
        {
            Debug.Log("goal");
            GameManager.Instance.Clear();
            GameManager.Instance.PlayerController.IsGame = false;

            _onece = true;
        }
    }
}
