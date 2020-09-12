namespace ScavengerWorld.World
{
    /// <summary>
    /// This interface should be implemented by any object that needs to be stepped 
    /// as part of the simulation.
    /// </summary>
    interface ISteppable
    {
        void Step(int timeStep);
    }
}
