using Pinpin;
using UnityEngine;
using System.Collections;

public class BallRandomizer : MonoBehaviour
{
    [System.Serializable]
    public class BallButton
    {
        public string ballName;         // e.g., "Golden", "Red"
        public GameObject buttonObject; // Predefined button GameObject
        public float chance;            // Chance percentage
    }

    public BallButton[] ballButtons;
    public GameObject GoldenBallButtonExtra;

    private static bool hasShownOnce = false;
    private string selectedBallName = "";

    private void OnEnable()
    {
        ShowBallByChance();
        StartCoroutine(DisableGoldenExtraIfNeeded());
    }

    void ShowBallByChance()
    {
        foreach (var ball in ballButtons)
        {
            if (ball.buttonObject != null)
                ball.buttonObject.SetActive(false);
        }

        //if (GoldenBallButtonExtra != null)
        //    GoldenBallButtonExtra.SetActive(true);

        float totalChance = 0f;
        foreach (var ball in ballButtons)
        {
            if (!hasShownOnce && ball.ballName.ToLower() == "golden")
                continue;

            totalChance += ball.chance;
        }

        float rand = Random.Range(0f, totalChance);
        float cumulative = 0f;

        foreach (var ball in ballButtons)
        {
            if (!hasShownOnce && ball.ballName.ToLower() == "golden")
                continue;

            cumulative += ball.chance;
            if (rand <= cumulative)
            {
                selectedBallName = ball.ballName.ToLower();

                if (ball.buttonObject != null)
                    ball.buttonObject.SetActive(true);
                break;
            }
        }

        hasShownOnce = true;
    }

    IEnumerator DisableGoldenExtraIfNeeded()
    {
        yield return new WaitForSeconds(0.3f);

        if (selectedBallName == "golden" && GoldenBallButtonExtra != null)
        {
            GoldenBallButtonExtra.SetActive(false);
        }
    }
}
