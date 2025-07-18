using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

[RequireComponent(typeof(RectTransform))]
public class GiftWheelCard : MonoBehaviour {

	[SerializeField] private Image m_giftImage;
	[FormerlySerializedAs("m_giftText")]
	public Text giftText;

    public void Init(Sprite sprite, int amount, float angle, Color imageColor)
    {
        m_giftImage.sprite = sprite;
        // m_giftImage.color = imageColor;

        if (amount >= 1000)
        {
            float formattedAmount = amount / 1000f;
            // Show one decimal only if needed (e.g., 1.5k), otherwise just show as 1k
            if (formattedAmount % 1 == 0)
                giftText.text = "x" + ((int)formattedAmount).ToString() + "k";
            else
                giftText.text = "x" + formattedAmount.ToString("0.#") + "k";
        }
        else
        {
            giftText.text = "x" + amount.ToString();
        }

        this.transform.Rotate(new Vector3(0, 0, angle));
    }

}
