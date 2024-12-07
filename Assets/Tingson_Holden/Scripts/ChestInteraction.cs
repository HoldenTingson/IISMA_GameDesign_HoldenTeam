using UnityEngine;
using UnityEngine.UI; // Untuk mengakses komponen UI Text jika diperlukan

public class ChestInteraction : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; // Reference to the Dialogue script
    private bool isChestUnlocked = false;

    private void OnMouseDown()
    {
        HandleChestInteraction();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memastikan hanya senjata pemain yang memicu interaksi
        if (!isChestUnlocked && other.gameObject.CompareTag("PlayerWeapon"))
        {
            // Hanya tampilkan dialog jika masih ada musuh yang hidup
            if (EnemyManager.Instance.GetEnemyCount() > 0)
            {
                HandleChestInteraction();
            }
            else
            {
                // Jika musuh sudah mati, peti dibuka tanpa dialog
                UnlockChest();
            }
        }
    }

    private void HandleChestInteraction()
    {
        if (isChestUnlocked)
        {
            Debug.Log("Chest is already unlocked.");
            return;
        }

        if (EnemyManager.Instance.GetEnemyCount() > 0)
        {
            // Show dialogue if enemies are still alive
            ResetDialogue();  // Reset dialog before starting a new one
            dialogue.StartDialogue(null); // Start the dialogue
        }
        else
        {
            // Unlock the chest and show success dialogue
            isChestUnlocked = true;
            ResetDialogue();  // Reset dialog before starting a new one
            dialogue.StartDialogue(UnlockChest);
        }
    }

    private void UnlockChest()
    {
        isChestUnlocked = true; // Tandai chest telah dibuka
        Debug.Log("The chest is now unlocked!");
        // Tambahkan logika lain seperti animasi membuka chest atau memunculkan item
    }

    // Fungsi untuk mereset status dialog
    private void ResetDialogue()
    {
        // Reset teks dialog jika Anda menggunakan UI Text (seperti di Unity)
        if (dialogue != null)
        {
            // Misalnya jika 'dialogue' memiliki UI Text yang perlu direset
            Text dialogueText = dialogue.GetComponentInChildren<Text>(); // Ambil komponen Text dari dalam dialog
            if (dialogueText != null)
            {
                dialogueText.text = ""; // Kosongkan teks
            }

            // Reset status dialog langsung di sini
            // Tidak perlu menggunakan ResetActiveStatus() lagi, cukup reset teks dan status lainnya jika diperlukan
            dialogue.ResetDialogue(); // Panggil ResetDialogue() pada objek Dialogue untuk mereset statusnya
        }
    }
}
