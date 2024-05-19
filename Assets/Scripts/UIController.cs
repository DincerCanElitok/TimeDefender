using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform pauseBtn;
    [SerializeField] private RectTransform goBtn;
    [SerializeField] private RectTransform soundOnBtn;
    [SerializeField] private RectTransform soundOffBtn;
    bool isPauseControl = false;
    public void PuaseButton()
    {
        goBtn.gameObject.SetActive(true);
        pauseBtn.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            pauseBtn.gameObject.SetActive(false);
        });
        goBtn.DOScale(Vector3.one, 0.2f).OnComplete(() =>
        {
            Time.timeScale = 0f;
            isPauseControl = true;

            soundOffBtn.GetComponent<Button>().interactable = false;
            soundOnBtn.GetComponent<Button>().interactable = false;
        });


    }
    public void PlayButton()
    {
        pauseBtn.gameObject.SetActive(true);
        Time.timeScale = 1f;
        goBtn.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            goBtn.gameObject.SetActive(false);
        });
        pauseBtn.DOScale(Vector3.one, 0.2f).OnComplete(() =>
        {
            soundOffBtn.GetComponent<Button>().interactable = true;
            soundOnBtn.GetComponent<Button>().interactable = true;
        });
    }
    public void SoundOnButton()
    {
        soundOffBtn.gameObject.SetActive(true);
        soundOnBtn.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            soundOnBtn.gameObject.SetActive(false);
        });
        soundOffBtn.DOScale(Vector3.one, 0.2f).OnComplete(() =>
        {
            //Sesi Kapat

        });
    }
    public void SoundOffButton()
    {
        soundOnBtn.gameObject.SetActive(true);
        soundOffBtn.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            soundOffBtn.gameObject.SetActive(false);
        });
        soundOnBtn.DOScale(Vector3.one, 0.2f).OnComplete(() =>
        {
            //Sesi Aç

        });
    }
}
