using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrosshairExpand : MonoBehaviour
{
    [Header("Lines")]
    public RectTransform lineTopRight;
    public RectTransform lineTopLeft;
    public RectTransform lineBottomRight;
    public RectTransform lineBottomLeft;

    [Header("Colors")]
    public Image[] crosshairImages;
    public float hitFlashDuration = 0.1f;

    [Header("Expansion")]
    public float baseExpandDistance = 20f;   // resting gap from center
    public float spreadPerShot = 8f;         // how much each shot adds
    public float maxSpread = 40f;            // hard limit
    public float spreadRecoverySpeed = 15f;  // how fast it returns to base
    public float expandDuration = 0.005f;

    private RectTransform[] lines;
    private Vector2[] basePositions;
    private Vector2[] directions;
    private float currentSpread = 0f;
    private Coroutine expandCoroutine;

    void Start()
    {
        lines = new RectTransform[] { lineTopRight, lineTopLeft, lineBottomRight, lineBottomLeft };

        directions = new Vector2[] {
            new Vector2(1, 1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(-1, -1).normalized
        };

        basePositions = new Vector2[4];
        for (int i = 0; i < 4; i++)
            basePositions[i] = lines[i].anchoredPosition;
    }

    void Update()
    {
        // Recover spread over time
        if (currentSpread > 0)
        {
            currentSpread = Mathf.Lerp(currentSpread, 0f, spreadRecoverySpeed * Time.deltaTime);

            // Update line positions as spread recovers
            for (int i = 0; i < 4; i++)
                lines[i].anchoredPosition = basePositions[i] + directions[i] * currentSpread;
        }
    }

    public void OnShoot(bool hitEnemy)
    {
        currentSpread = Mathf.Min(currentSpread + spreadPerShot, maxSpread);

        if (expandCoroutine != null)
            StopCoroutine(expandCoroutine);
        expandCoroutine = StartCoroutine(ExpandContract());

        if (hitEnemy)
            StartCoroutine(FlashColor());
    }

    IEnumerator ExpandContract()
    {
        float shotSpread = currentSpread;
        Vector2[] expandedPositions = new Vector2[4];
        for (int i = 0; i < 4; i++)
            expandedPositions[i] = basePositions[i] + directions[i] * shotSpread;

        // Snap out
        float t = 0;
        Vector2[] fromPositions = new Vector2[4];
        for (int i = 0; i < 4; i++)
            fromPositions[i] = lines[i].anchoredPosition;

        while (t < expandDuration)
        {
            t += Time.deltaTime;
            float lerp = t / expandDuration;
            for (int i = 0; i < 4; i++)
                lines[i].anchoredPosition = Vector2.Lerp(fromPositions[i], expandedPositions[i], lerp);
            yield return null;
        }
    }

    IEnumerator FlashColor()
    {
        foreach (var img in crosshairImages)
            img.color = new Color(1f, 0.843f, 0f);
        yield return new WaitForSeconds(hitFlashDuration);
        foreach (var img in crosshairImages)
            img.color = Color.white;
    }

    public float GetSpreadRadius()
    {
        return currentSpread;
    }
}