namespace Game
{
    internal abstract class Racer
    {
        internal abstract bool IsCollidable();
        internal abstract bool CollidesWith(Racer other);
        internal abstract bool IsAlive();
        internal abstract void Update(float deltaTimeS);
        internal abstract void Destroy();
    }
}