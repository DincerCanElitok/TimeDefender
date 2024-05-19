using UnityEngine;
using DG;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform gameName;
    [SerializeField] private RectTransform buttonsPanel;
    [SerializeField] private RectTransform charPanel;
    [SerializeField] private RectTransform infoPanel;
    [SerializeField] private RectTransform startBtn;
    [SerializeField] private RectTransform infoBtn;

    private void Start()
    {
        Time.timeScale = 1f;
        gameName.DOAnchorPosY(-13, 1f).OnComplete(() =>
        {
            charPanel.gameObject.SetActive(true);
            charPanel.DOAnchorPosX(350, 1f);
            buttonsPanel.DOAnchorPos(Vector2.zero, 1f);
        });
    }

    public void StarGameButton()
    {
        startBtn.DOScale(0.85f, 0.2f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            SceneManager.LoadScene(1);
        });
    }

    public void GameInfoButton()
    {
        infoBtn.DOScale(0.85f, 0.2f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            infoPanel.gameObject.SetActive(true);

            charPanel.DOAnchorPosX(1600, 0.85f).OnComplete(() =>
            {
                infoPanel.DOAnchorPosX(350, 0.85f).OnComplete(() =>
                {
                    charPanel.gameObject.SetActive(false);
                });
            });
        });
    }

    public void ExitInfoButton(RectTransform button)
    {
        button.DOScale(0.85f, 0.2f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            infoPanel.DOAnchorPosX(1600, 0.85f).OnComplete(() =>
            {
                charPanel.gameObject.SetActive(true);
                charPanel.DOAnchorPosX(350, 0.85f).OnComplete(() =>
                {
                    infoPanel.gameObject.SetActive(false);
                });
            });
        });
    }
}
