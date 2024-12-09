using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : Singleton<Dialogue>
{
    [SerializeField] private TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    private int index;
    private bool isDialogueActive = false;
    private System.Action onDialogueComplete; // Callback for when dialogue ends

    // Static reference to ensure we can always find the dialogue component
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // Attempt to find text component if it's not set
        if (textComponent == null)
        {
            TryFindTextComponent();
        }
    }

    private bool TryFindTextComponent()
    {
        // Try multiple ways to find the text component
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
            textComponent = GetComponentInChildren<TextMeshProUGUI>();

        if (textComponent == null)
            textComponent = FindObjectOfType<TextMeshProUGUI>();

        if (textComponent == null)
        {
            Debug.LogError("Could not find TextMeshProUGUI component for Dialogue. Please assign it in the inspector.");
            return false;
        }

        return true;
    }

    public void StartDialogue(System.Action callback = null)
    {
        // Ensure we have a valid text component
        if (this == null)
        {
            Debug.LogError("Dialogue component has been destroyed!");
            return;
        }

        if (textComponent == null && !TryFindTextComponent())
        {
            Debug.LogError("Cannot start dialogue - no text component found!");
            return;
        }

        // Reset everything before starting the dialogue
        ResetDialogue();
        index = 0;
        isDialogueActive = true;
        Time.timeScale = 0f; // Pause the game
        gameObject.SetActive(true); // Show the dialogue box
        onDialogueComplete = callback; // Assign callback if provided

        // Safely disable player controls
        TryDisablePlayerControls();

        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        if (textComponent == null)
        {
            Debug.LogError("Text component is null during TypeLine!");
            yield break;
        }

        textComponent.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            if (textComponent != null)
                textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void Update()
    {
        // Player can use mouse or Enter to proceed
        if (isDialogueActive && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)))
        {
            if (textComponent != null && textComponent.text == lines[index])
            {
                NextLine();
            }
            else if (textComponent != null)
            {
                StopAllCoroutines();
                textComponent.text = lines[index]; // Show full line immediately if the player clicks before it's finished
            }
        }
    }

    private void TryDisablePlayerControls()
    {
        try
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.enabled = false;
                PlayerController.Instance.canAttack = false;
            }

            if (ActiveInventory.Instance != null)
            {
                ActiveInventory.Instance.canToggle = false;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Error disabling player controls: {e.Message}");
        }
    }

    private void TryEnablePlayerControls()
    {
        try
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.enabled = true;
                PlayerController.Instance.canAttack = true;
            }

            if (ActiveInventory.Instance != null)
            {
                ActiveInventory.Instance.canToggle = true;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Error enabling player controls: {e.Message}");
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        Time.timeScale = 1f; // Resume the game
        gameObject.SetActive(false); // Hide the dialogue box
        onDialogueComplete?.Invoke(); // Invoke the callback if assigned

        TryEnablePlayerControls();
    }

    public void ResetDialogue()
    {
        index = 0;
        if (textComponent != null)
        {
            textComponent.text = string.Empty;
        }
        isDialogueActive = false;
    }

    // Optional: Method to safely check if dialogue is currently active
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}