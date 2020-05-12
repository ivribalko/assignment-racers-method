using System.Collections.Generic;

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
            // TODO doesn't make sense that not IsAlive() can still be IsCollidable()

            var collidedRacers = GetCollidedRacers(racers);

            var aliveRacers = GetAliveRacers(racers, collidedRacers);

            DestroyRacers(racers, collidedRacers);

            racers = aliveRacers;
        }

        private static void DestroyRacers(List<Racer> racers, List<Racer> racersNeedingRemoved)
        {
            foreach (var racer in racersNeedingRemoved)
            {
                if (racers.Contains(racer)) // Check we've not removed this already!
                {
                    racer.Destroy();
                    racers.Remove(racer);
                }
            }
        }

        private static List<Racer> GetAliveRacers(IReadOnlyList<Racer> racers, List<Racer> racersNeedingRemoved)
        {
            var newRacerList = new List<Racer>();

            foreach (var racer in racers)
            {
                if (!racersNeedingRemoved.Contains(racer))
                {
                    newRacerList.Add(racer);
                }
            }

            return newRacerList;
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