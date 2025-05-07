using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PlaneCounter : MonoBehaviour
{
    [SerializeField] ARPlaneManager planeManager;
    [SerializeField] TMP_Text counterText;

    int totalPlanesDetected = 0;

    void OnEnable()
    {
        if (planeManager != null)
        {
            planeManager.planesChanged += OnPlanesChanged;
        }
    }

    void OnDisable()
    {
        if (planeManager != null)
        {
            planeManager.planesChanged -= OnPlanesChanged;
        }
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        totalPlanesDetected += args.added.Count;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = $"Planos detectados: {totalPlanesDetected}";
        }
    }
}
