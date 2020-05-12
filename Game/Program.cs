using System.Collections.Generic;
using System.Linq;

namespace Game
{
    internal static class Main
    {
        internal static void UpdateRacers(float deltaTimeS, ref List<Racer> racers)
        {
            foreach (var racer in racers)
            {
                if (racer.IsAlive())
                {
                    // TODO should probably change ms to s
                    // TODO as it is more natural for Unity
                    // Racer update takes milliseconds
                    racer.Update(deltaTimeS * 1000.0f);
                }
            }

            // TODO check that only IsAlive() racers could be used
            // TODO not IsAlive() should not still be IsCollidable()
            // TODO and therefore can be skipped

            var collidedRacers = GetCollidedRacers(racers);

            var aliveRacers = GetAliveRacers(racers, collidedRacers);

            DestroyRacers(racers, collidedRacers);

            racers = aliveRacers;
        }

        private static void DestroyRacers(List<Racer> racers, List<Racer> collidedRacers)
        {
            foreach (var racer in collidedRacers)
            {
                if (racers.Contains(racer)) // Check we've not removed this already!
                {
                    racer.Destroy();
                    racers.Remove(racer);
                }
            }
        }

        private static List<Racer> GetAliveRacers(IEnumerable<Racer> racers, IReadOnlyList<Racer> collidedRacers)
        {
            var result = new List<Racer>();

            foreach (var racer in racers)
            {
                if (!collidedRacers.Contains(racer))
                {
                    result.Add(racer);
                }
            }

            return result;
        }

        private static List<Racer> GetCollidedRacers(List<Racer> racers)
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