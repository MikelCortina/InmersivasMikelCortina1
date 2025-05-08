using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionTrigger : MonoBehaviour
{
    public GameObject decisionPanel;
    public Button yesButton;
    public Button noButton;
    public string sceneToLoad;

    private void Start()
    {
        decisionPanel.SetActive(false);

        yesButton.onClick.AddListener(() => LoadScene());
        noButton.onClick.AddListener(() => decisionPanel.SetActive(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Opcional: si usas tags para filtrar al jugador
        {
            Debug.Log("Entró en trigger con: " + other.gameObject.name);
            decisionPanel.SetActive(true); // <-- ESTA LÍNEA ES LA QUE FALTABA
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);

    }
}
