using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private bool isDialogueActive = false;
    private System.Action onDialogueComplete; // Callback for when dialogue ends

    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false); // Hide the dialogue box initially
    }

    void Update()
    {
        // Player can use mouse or Enter to proceed
        if (isDialogueActive && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index]; // Show full line immediately if the player clicks before it's finished
            }
        }
    }

    public void StartDialogue(System.Action callback = null)
    {
        // Reset everything before starting the dialogue
        ResetDialogue();

        index = 0;
        isDialogueActive = true;
        Time.timeScale = 0f; // Pause the game
        gameObject.SetActive(true); // Show the dialogue box
        onDialogueComplete = callback; // Assign callback if provided

        DisablePlayerControls();
        PlayerController.Instance.canAttack = false;
        ActiveInventory.Instance.canToggle = false;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed); // Use WaitForSecondsRealtime during pause
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisablePlayerControls()
    {
        // Find the PlayerController component and disable it
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;  // Disable player movement
        }
    }

    private void EnablePlayerControls()
    {
        // Re-enable player controls once the dialogue is finished
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = true;  // Enable player movement
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        Time.timeScale = 1f; // Resume the game
        gameObject.SetActive(false); // Hide the dialogue box
        onDialogueComplete?.Invoke(); // Invoke the callback if assigned
        EnablePlayerControls();
        PlayerController.Instance.canAttack = true;
        ActiveInventory.Instance.canToggle = true;
    }

    public void ResetDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        isDialogueActive = false;
    }
}
