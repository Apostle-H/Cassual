using TMPro;
using UnityEngine;

namespace UI.Bank
{
    public class BankUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private BankSystem.Bank bank;

        private void Awake()
        {
            bank.OnAmountUpdate += () => UpdateView(bank.Amount.ToString());
            
            UpdateView(0.ToString());
        }

        public void UpdateView(string newAmount) => amountText.text = newAmount;
    }
}