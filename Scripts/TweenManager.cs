using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenManager : MonoBehaviour
{
    public Transform targetCam;
    private Vector3 camFirstPos;
    private Quaternion camFirstRot;

    public GameObject mainMenuUI;
    public GameObject changeBallUI;
    public GameObject changeBackgroundUI;

    public bool isBallsUIActive;
    public bool isBackgroundUIActive;

    // Start is called before the first frame update
    void Start()
    {
        camFirstPos = targetCam.gameObject.transform.position;
        camFirstRot = targetCam.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCameraPositionForBalls()
    {
        isBallsUIActive = true;
        transform.DOMove(new Vector3(-1.4f, 6.2f, -7f), 1.5f).SetUpdate(UpdateType.Late);
        transform.DORotate(new Vector3(20f, 90f, 0), 1.5f).SetUpdate(UpdateType.Late);
        Camera.main.DOFieldOfView(38, 1.5f);
    }
    public void ChangeCameraPositionForBackground()
    {
        isBackgroundUIActive = true;
        transform.DOMove(new Vector3(-6.5f, 9f, -10.2f), 1.5f).SetUpdate(UpdateType.Late);
        transform.DORotate(new Vector3(25f, 61f, 0), 1.5f).SetUpdate(UpdateType.Late);
        Camera.main.DOFieldOfView(80, 1.5f);
    }

    public void ChangeCameraPositionToFirstPos()
    {
        isBallsUIActive = false;
        isBackgroundUIActive = false;
        targetCam.DOMove(camFirstPos, 1.5f).SetUpdate(UpdateType.Late);
        targetCam.DORotate(camFirstRot.eulerAngles, 1.5f).SetUpdate(UpdateType.Late);
        Camera.main.DOFieldOfView(60, 1.5f);
    }

    public void BringBallsButtonUI()
    {
        isBallsUIActive = true;
        mainMenuUI.SetActive(false);
        changeBallUI.SetActive(true);
    }

    public void BringBackgroundUI()
    {
        isBackgroundUIActive = true;
        mainMenuUI.SetActive(false);
        changeBackgroundUI.SetActive(true);
    }

    public void BackToMainUI()
    {
        isBallsUIActive = false;
        isBackgroundUIActive = false;
        changeBallUI.SetActive(false);
        changeBackgroundUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
