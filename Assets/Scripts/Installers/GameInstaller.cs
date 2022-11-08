using UnityEngine;
using System;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject]
    Settings _settings = null;

    [SerializeField] private Fill fillInstance;
    [SerializeField] private GameController gameControllerInstance;

    public override void InstallBindings()
    {
        Container.Bind<Fill>().FromInstance(fillInstance).AsSingle();

        Container.Bind<GameController>().FromInstance(gameControllerInstance).AsSingle();

        Container.BindFactory<Fill, Fill.Factory>().FromComponentInNewPrefab(_settings.FillPrefab).NonLazy();
    }

    [Serializable]
    public class Settings
    {
        public GameObject FillPrefab;
    }
}

