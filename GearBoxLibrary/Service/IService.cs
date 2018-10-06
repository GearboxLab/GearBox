namespace GearBox.Service
{
    public interface IService
    {
        void Start();
        void Stop();
        void Restart();
        bool IsRunning();
    }
}
