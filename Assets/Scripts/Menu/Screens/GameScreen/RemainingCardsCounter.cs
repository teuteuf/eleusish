using UnityEngine;
using UnityEngine.UI;

namespace Menu.Screens.GameScreen
{
    public class RemainingCardsCounter : MonoBehaviour
    {
        [SerializeField] private Text uiCounter = default;

        [SerializeField] private AnimationCurve colorChange = default;

        private int _maxNbCards;
        
        public void UpdateCardCounter(int nbCards)
        {
            _maxNbCards = Mathf.Max(_maxNbCards, nbCards);
            uiCounter.text = nbCards.ToString();
            uiCounter.color = Color.Lerp(Color.white, Color.red, colorChange.Evaluate(1 - nbCards / (float) _maxNbCards));
        }
    }
}
