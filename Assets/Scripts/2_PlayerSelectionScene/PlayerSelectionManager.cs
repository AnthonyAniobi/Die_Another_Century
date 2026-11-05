using UnityEngine;
using PlayerData;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerSelectionManager : MonoBehaviour
{
    public Camera playerSelectionCamera;
    public Transform centerPoint;

    [Min(0.1f)]
    public float circleRadius = 4f;
    [Min(0f)]
    public float cameraHeight = 1.5f;
    [Min(0f)]
    public float cameraRotationDuration = 0.35f;

    [Header("Selected Player Info")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerSpeedText;
    public TextMeshProUGUI playerResistanceText;

    [Header("Popup Panel")]
    public GameObject popupPanel;
    public TextMeshProUGUI popupText;

    //
    private readonly List<Transform> playerInstances = new List<Transform>();
    private int selectedPlayerIndex;
    private Coroutine cameraRotation;



    private void Start()
    {
        if (playerSelectionCamera == null)
            playerSelectionCamera = Camera.main;
        if (centerPoint == null)
        {
            GameObject centerObject = GameObject.Find("Center");
            if (centerObject != null)
                centerPoint = centerObject.transform;
        }

        if (playerSelectionCamera == null || centerPoint == null)
        {
            Debug.LogError("PlayerSelectionManager requires a camera and a center point.", this);
            enabled = false;
            return;
        }

        PlayerType[] playerTypes = (PlayerType[])System.Enum.GetValues(typeof(PlayerType));
        List<GameObject> playerPrefabs = new List<GameObject>();
        for (int i = 0; i < playerTypes.Length; i++)
        {
            GameObject playerPrefab = playerTypes[i].GetGameObject();
            if (playerPrefab == null)
                continue;
            playerPrefabs.Add(playerPrefab);
        }

        for (int i = 0; i < playerPrefabs.Count; i++)
        {
            float angle = i * (360f / playerPrefabs.Count) * Mathf.Deg2Rad;
            Vector3 localPosition = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * circleRadius;
            GameObject playerInstance = Instantiate(playerPrefabs[i], centerPoint);
            Transform playerTransform = playerInstance.transform;
            playerTransform.localPosition = localPosition;
            playerTransform.localRotation = Quaternion.LookRotation(-localPosition, Vector3.up);
            playerInstances.Add(playerTransform);
        }

        if (playerInstances.Count == 0)
        {
            Debug.LogError("PlayerSelectionManager could not load any player prefabs.", this);
            enabled = false;
            return;
        }

        playerSelectionCamera.transform.position = centerPoint.position + Vector3.up * cameraHeight;
        FocusOnPlayer(false);
        UpdateInfoPanel();
    }

    public void NextPlayer()
    {
        selectedPlayerIndex = (selectedPlayerIndex + 1) % playerInstances.Count;
        FocusOnPlayer(true);
    }

    public void PreviousPlayer()
    {
        selectedPlayerIndex--;
        if (selectedPlayerIndex < 0)
            selectedPlayerIndex = playerInstances.Count - 1;
        FocusOnPlayer(true);
    }

    public void SelectPlayer()
    {
        PlayerType selectedPlayerType = (PlayerType)selectedPlayerIndex;
        GameManager.Instance.SetSelectedPlayer(selectedPlayerType);
        StartCoroutine(
            ShowPopup($"Selected Player: {selectedPlayerType}", 2f)
        );
    }

    public void CloseSelection()
    {
        NavigationManager.Instance.NavigateToScene(GameScene.WelcomeScene);
    }

    private void UpdateInfoPanel()
    {
        PlayerType selectedPlayerType = (PlayerType)selectedPlayerIndex;
        playerNameText.text = selectedPlayerType.ToString();
        playerHealthText.text = $"Health: {selectedPlayerType.GetHealth()}";
        playerSpeedText.text = $"Speed: {selectedPlayerType.GetSpeed()}";
        playerResistanceText.text = $"Resistance: {selectedPlayerType.GetResistance()}";
    }

    private void FocusOnPlayer(bool animate)
    {
        Vector3 targetPosition = playerInstances[selectedPlayerIndex].position + Vector3.up;
        Quaternion targetRotation = Quaternion.LookRotation(
            targetPosition - playerSelectionCamera.transform.position,
            Vector3.up);

        if (cameraRotation != null)
            StopCoroutine(cameraRotation);

        if (animate && cameraRotationDuration > 0f)
            cameraRotation = StartCoroutine(RotateCamera(targetRotation));
        else
            playerSelectionCamera.transform.rotation = targetRotation;
    }

    private IEnumerator RotateCamera(Quaternion targetRotation)
    {
        Quaternion startRotation = playerSelectionCamera.transform.rotation;
        float elapsed = 0f;

        while (elapsed < cameraRotationDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / cameraRotationDuration);
            playerSelectionCamera.transform.rotation = Quaternion.Slerp(
                startRotation,
                targetRotation,
                progress);
            yield return null;
        }

        playerSelectionCamera.transform.rotation = targetRotation;
        cameraRotation = null;
        UpdateInfoPanel();
    }

    private IEnumerator ShowPopup(string message, float duration)
    {
        popupText.text = message;
        popupPanel.SetActive(true);
        yield return new WaitForSeconds(duration);
        popupPanel.SetActive(false);
    }
}
