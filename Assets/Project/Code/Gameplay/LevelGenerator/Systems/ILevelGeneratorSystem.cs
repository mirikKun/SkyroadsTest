namespace Code.Gameplay.LevelGenerator.Systems
{
    public interface ILevelGeneratorSystem
    {
        void Init();
        void TryGenerate();
        void TryDestroy();
        void UpdateBehaviours();
        void Reset();
    }
}