using System.Collections.Generic;

namespace Game
{
    internal static class Main
    {
        // TODO changing a collection within a method is very illusive and should be avoided
        internal static void UpdateRacers(float deltaTimeSeconds, List<Racer> racers)
        {
            // TODO should probably change ms to s
            // TODO as it is more uniform
            var deltaTimeMilliseconds = deltaTimeSeconds * 1000.0f;

            foreach (var racer in racers)
            {
                if (racer.IsAlive())
                {
                    racer.Update(deltaTimeMilliseconds);
                }
            }

            // TODO check that only IsAlive() racers could be used
            // TODO not IsAlive() should not still be IsCollidable()
            // TODO and therefore can be skipped

            var remove = GetCollidedRacers(racers);

            for (var index = racers.Count - 1; index >= 0; index--)
            {
                var racer = racers[index];

                if (remove.Contains(racer))
                {
                    racers.RemoveAt(index);

                    racer.Destroy();
                }
            }
        }

        private static List<Racer> GetCollidedRacers(IReadOnlyList<Racer> racers)
        {
            var result = new List<Racer>();

            var racersCount = racers.Count;

            for (var indexOne = 0; indexOne < racersCount - 1; indexOne++)
            {
                var racerOne = racers[indexOne];

                for (var indexTwo = indexOne + 1; indexTwo < racersCount; indexTwo++)
                {
                    var racerTwo = racers[indexTwo];

                    if (Collides(racerOne, racerTwo))
                    {
                        OnRacerExplodes(racerOne);
                        OnRacerExplodes(racerTwo);
                        result.Add(racerOne);
                        result.Add(racerTwo);
                    }
                }
            }

            return result;
        }

        private static bool Collides(Racer racerOne, Racer racerTwo) =>
            racerOne.IsCollidable() &&
            racerTwo.IsCollidable() &&
            racerOne.CollidesWith(racerTwo);  // TODO move IsCollidable into CollidesWith

        private static void OnRacerExplodes(Racer racer)
        {
            // TODO
        }
    }
}