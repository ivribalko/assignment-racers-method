using System.Collections.Generic;

namespace Game
{
    internal static class Main
    {
        private static List<Racer> cache = new List<Racer>();

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

            GetCollidedRacers(
                racers: racers,
                result: ref cache);

            Util.RemoveIntersections(
                list: racers,
                remove: cache,
                onRemoved: OnRemoved);
        }

        private static void GetCollidedRacers(IReadOnlyList<Racer> racers, ref List<Racer> result)
        {
            result.Clear();

            var racersCount = racers.Count;

            for (var indexOne = 0; indexOne < racersCount - 1; indexOne++)
            {
                var racerOne = racers[indexOne];

                for (var indexTwo = indexOne + 1; indexTwo < racersCount; indexTwo++)
                {
                    var racerTwo = racers[indexTwo];

                    if (Collides(racerOne, racerTwo))
                    {
                        result.Add(racerOne);
                        result.Add(racerTwo);
                    }
                }
            }
        }

        private static bool Collides(Racer racerOne, Racer racerTwo) =>
            racerOne.IsCollidable() &&
            racerTwo.IsCollidable() &&
            racerOne.CollidesWith(racerTwo);  // TODO move IsCollidable into CollidesWith

        private static void OnRemoved(Racer racer)
        {
            OnRacerExplodes(racer);

            racer.Destroy();
        }

        private static void OnRacerExplodes(Racer racer)
        {
            // TODO
        }
    }
}