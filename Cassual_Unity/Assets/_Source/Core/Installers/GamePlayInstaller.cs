using BankSystem;
using InputSystem;
using Zenject;

namespace Core.Installers
{
    public class GamePlayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Input();
        }

        private void Input()
        {
            MainActions mainActions = new MainActions();
            mainActions.Enable();
            
            Container.Bind<MainActions.CrowdActions>().FromInstance(mainActions.Crowd).AsSingle().NonLazy();
        }
    }
}