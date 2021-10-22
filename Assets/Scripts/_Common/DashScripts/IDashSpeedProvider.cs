namespace Common.DashScripts
{
    public interface IDashSpeedProvider
    {
        float GetDashSpeed(float normalizedDashTime);
    }
}