using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public Image fillImg;
    [SerializeField]
    private TMP_Text currentLv;
    [SerializeField]
    private TMP_Text nextLv;
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Transform endLineTransform;

    private Vector3 endLinePosition;
    private float fullDistance;

    private LevelManager _levelManager;
    private BallSelect _ballSelect;
    private FinishLine _finishLine;

    public List<Transform> finishLineTransforms = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _ballSelect = FindObjectOfType<BallSelect>();
        _finishLine = FindObjectOfType<FinishLine>();
        playerTransform = _ballSelect.selectedBallTransform;

        // Tüm FinishLine nesnelerini bulun
        FinishLine[] finishLines = FindObjectsOfType<FinishLine>();

        // FinishLine'larý uzaklýðýna göre sýrala
        System.Array.Sort(finishLines, (a, b) =>
        {
            float distanceA = Vector3.Distance(playerTransform.position, a.transform.position);
            float distanceB = Vector3.Distance(playerTransform.position, b.transform.position);
            return distanceA.CompareTo(distanceB);
        });

        // FinishLine'larý listeye ekleyin (en yakýndan en uzaða)
        foreach (FinishLine finishLine in finishLines)
        {
            finishLineTransforms.Add(finishLine.transform);
        }

        // En yakýn FinishLine'ý endLineTransform olarak ayarlayýn
        endLineTransform = finishLines[0].transform;
        endLinePosition = finishLines[0].transform.position;
        fullDistance = GetDistance();
    }

    public void SetLevelTexts()
    {
        currentLv.text = (_levelManager.currentLevel + 1).ToString();
        nextLv.text = (_levelManager.currentLevel + 2).ToString();
    }

    public float GetDistance()
    {
        playerTransform = _ballSelect.selectedBallTransform;

        if (finishLineTransforms.Count > 0)
        {
            endLineTransform = finishLineTransforms[0];
            endLinePosition = endLineTransform.position;
            return Vector3.Distance(playerTransform.position, endLinePosition);
        }

        return 0f; // FinishLine yoksa mesafe 0
    }

    private void UpdateFillProgress(float value)
    {
        fillImg.fillAmount = value;
    }

    void Update()
    {
        playerTransform = _ballSelect.selectedBallTransform;

        float newDistance = GetDistance();
        float progressValue = Mathf.InverseLerp(fullDistance, 0f, newDistance);

        UpdateFillProgress(progressValue);
    }

    public void ResetProgressBar()
    {
        fullDistance = GetDistance(); // fullDistance'ý baþlangýç deðerine geri döndür
        float progressValue = 0f; // progressValue'yi baþlangýç deðerine geri döndür
        UpdateFillProgress(progressValue);
    }
}