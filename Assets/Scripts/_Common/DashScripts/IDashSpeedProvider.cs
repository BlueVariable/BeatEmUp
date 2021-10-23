namespace BLUE.ProjectX.Common.DashScripts
{
    public interface IDashSpeedProvider
    {
        float GetDashSpeed(float normalizedDashTime);
    }
}