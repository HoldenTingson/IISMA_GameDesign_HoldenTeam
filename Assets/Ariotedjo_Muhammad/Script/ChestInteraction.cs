using UnityEngine;
public class ChestInteraction : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; // Reference to the Dialogue script

    private void OnMouseDown()
    {
        if (EnemyManager.Instance.GetEnemyCount() > 0)
        {
            dialogue.StartDialogue();
        }
        else
        {
            Debug.Log("All enemies have been defeated. You may proceed.");
        }
    }
}