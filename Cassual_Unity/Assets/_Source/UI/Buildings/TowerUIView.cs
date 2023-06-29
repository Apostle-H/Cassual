using TMPro;
using UnityEngine;

namespace UI.Buildings
{
    public class TowerUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI upHeadUIText;
        [SerializeField] private string deathUpHeadText;
        
        public void UpdateView(string newUpHeadText) => upHeadUIText.text = newUpHeadText;

        public void UpdateViewDeath() => upHeadUIText.text = deathUpHeadText;
    }
}