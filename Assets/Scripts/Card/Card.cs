using System;
using TMPro;
using UnityEngine;

namespace Card
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Material materialClubs;
        [SerializeField] private Material materialDiamonds;
        [SerializeField] private Material materialHearts;
        [SerializeField] private Material materialSpades;

        [SerializeField] private TextMeshPro valueText;
        [SerializeField] private MeshRenderer suiteMeshRenderer;

        private void Start()
        {
            Set(CardValue.Jack, CardSuite.Diamonds);
        }

        private void Update()
        {
            transform.rotation *= Quaternion.AngleAxis(25 * Time.deltaTime, Vector3.forward);
        }

        public void Set(CardValue value, CardSuite suite)
        {
            valueText.text = value.GetStringValue();
            suiteMeshRenderer.material = GetSuiteMaterial(suite);
        }

        private Material GetSuiteMaterial(CardSuite suite)
        {
            switch (suite)
            {
                case CardSuite.Clubs:
                    return materialClubs;
                case CardSuite.Diamonds:
                    return materialDiamonds;
                case CardSuite.Hearts:
                    return materialHearts;
                case CardSuite.Spades:
                    return materialSpades;
                default:
                    throw new ArgumentOutOfRangeException(nameof(suite), suite, "No material.");
            }
        }
    }
}