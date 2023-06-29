using System;
using Buildings;
using UnityEngine;

namespace UI.Buildings
{
    public class TowerUIController : MonoBehaviour
    {
        [SerializeField] private Tower model;
        [SerializeField] private TowerUIView view;

        private void Awake()
        {
            Bind();
            UpdateViewPower();
        }

        private void Bind()
        {
            model.OnDamaged += UpdateViewPower;
            model.OnDeath += UpdateViewDeath;
            model.OnDeath += Expose;
        }


        private void Expose()
        {
            model.OnDamaged -= UpdateViewPower;
            model.OnDeath -= UpdateViewDeath;
            model.OnDeath -= Expose;
        }

        private void UpdateViewPower() => view.UpdateView(model.Power.ToString());

        private void UpdateViewDeath() => view.UpdateViewDeath();
    }
}