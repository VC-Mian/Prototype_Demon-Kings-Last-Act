using UnityEngine;
using TMPro; // Note the use of TMPro namespace

public class DemonManager : MonoBehaviour
{
    public int demonCount;
    public TextMeshProUGUI demonText;

    // Start is called before the first frame update
    void Start()
    {
        if (demonText == null)
        {
            Debug.LogError("Demon Text is not assigned in the demonManager script.");
        }
    }

    void Update()
    {
        if (demonText != null)
        {
            demonText.text = demonCount.ToString();
        }
    }
}
