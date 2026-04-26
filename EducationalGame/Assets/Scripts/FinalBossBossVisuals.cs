using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossBossVisuals : MonoBehaviour
{
    [Header("Boss Visual")]
    [SerializeField] private SpriteRenderer bossSpriteRenderer;
    [SerializeField] private Image bossImage;
    [SerializeField] private Color damageFlashColor = new Color(1f, 0.35f, 0.35f, 1f);
    [SerializeField] private float flashDuration = 0.15f;
    [SerializeField] private int flashCount = 2;

    [Header("Penguin Pop Out")]
    [SerializeField] private GameObject oldComputerObject;
    [SerializeField] private GameObject penguinObject;
    [SerializeField] private bool hidePenguinOnStart = true;
    [SerializeField] private Vector3 penguinHiddenLocalOffset = new Vector3(0f, -1.1f, 0f);
    [SerializeField] private Vector3 penguinShownLocalOffset = new Vector3(0f, 0.15f, 0f);
    [SerializeField] private bool animatePenguinScale;
    [SerializeField] private bool overridePenguinShownScale;
    [SerializeField] private Vector3 penguinShownScale = Vector3.one;
    [SerializeField] private float penguinPopDuration = 0.25f;

    private Coroutine flashRoutine;
    private Coroutine penguinRoutine;
    private Color bossBaseColor = Color.white;
    private Vector3 penguinBaseScale = Vector3.one;
    private bool penguinRevealed;

    private void Awake()
    {
        if (bossSpriteRenderer == null)
        {
            bossSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (bossImage == null)
        {
            bossImage = GetComponent<Image>();
        }

        bossBaseColor = GetBossColor();

        if (penguinObject != null)
        {
            penguinBaseScale = penguinObject.transform.localScale;

            if (hidePenguinOnStart)
            {
                penguinObject.SetActive(false);
                SetPenguinLocalOffset(penguinHiddenLocalOffset);
            }
            else
            {
                penguinRevealed = true;
            }
        }
    }

    public void PlayDamageReaction(bool shouldRevealPenguin)
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());

        if (shouldRevealPenguin && penguinObject != null && !penguinRevealed)
        {
            if (penguinRoutine != null)
            {
                StopCoroutine(penguinRoutine);
            }

            penguinRoutine = StartCoroutine(PenguinPopRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        for (int index = 0; index < flashCount; index++)
        {
            SetBossColor(damageFlashColor);
            yield return new WaitForSecondsRealtime(flashDuration);
            SetBossColor(bossBaseColor);
            yield return new WaitForSecondsRealtime(flashDuration);
        }

        flashRoutine = null;
    }

    private IEnumerator PenguinPopRoutine()
    {
        Transform penguinTransform = penguinObject.transform;
        if (oldComputerObject != null)
        {
            oldComputerObject.SetActive(false);
        }

        penguinObject.SetActive(true);
        penguinRevealed = true;

        Vector3 endingScale = overridePenguinShownScale ? penguinShownScale : penguinBaseScale;
        SetPenguinLocalOffset(penguinHiddenLocalOffset);
        penguinTransform.localScale = animatePenguinScale ? Vector3.zero : endingScale;

        float elapsed = 0f;
        while (elapsed < penguinPopDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(elapsed / penguinPopDuration);
            float easedProgress = 1f - Mathf.Pow(1f - progress, 3f);

            SetPenguinLocalOffset(Vector3.LerpUnclamped(penguinHiddenLocalOffset, penguinShownLocalOffset, easedProgress));

            if (animatePenguinScale)
            {
                penguinTransform.localScale = Vector3.LerpUnclamped(Vector3.zero, endingScale, easedProgress);
            }
            else
            {
                penguinTransform.localScale = endingScale;
            }

            yield return null;
        }

        SetPenguinLocalOffset(penguinShownLocalOffset);
        penguinTransform.localScale = endingScale;
        penguinRoutine = null;
    }

    private void SetPenguinLocalOffset(Vector3 offset)
    {
        if (penguinObject == null)
        {
            return;
        }

        RectTransform penguinRect = penguinObject.transform as RectTransform;
        if (penguinRect != null)
        {
            penguinRect.anchoredPosition3D = offset;
            return;
        }

        penguinObject.transform.localPosition = offset;
    }

    private Color GetBossColor()
    {
        if (bossSpriteRenderer != null)
        {
            return bossSpriteRenderer.color;
        }

        if (bossImage != null)
        {
            return bossImage.color;
        }

        return Color.white;
    }

    private void SetBossColor(Color targetColor)
    {
        if (bossSpriteRenderer != null)
        {
            bossSpriteRenderer.color = targetColor;
        }

        if (bossImage != null)
        {
            bossImage.color = targetColor;
        }
    }
}
