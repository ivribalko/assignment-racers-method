using System.Collections.Generic;

namespace Game
{
    internal static class Main
    {
        internal static void UpdateRacers(float deltaTimeS, ref List<Racer> racers)
        {
            UpdateAliveRacers(deltaTimeS, racers);

            var collidedRacers = GetCollidedRacers(racers);

            var aliveRacers = GetAliveRacers(racers, collidedRacers);

            DestroyRacers(racers, collidedRacers);

            racers = aliveRacers;
        }

        private static void UpdateAliveRacers(float deltaTimeS, List<Racer> racers)
        {
            foreach (var racer in racers)
            {
                if (racer.IsAlive())
                {
                    //Racer update takes milliseconds
                    racer.Update(deltaTimeS * 1000.0f);
                }
            }
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

            for (var racerIndex1 = 0; racerIndex1 < racers.Count; racerIndex1++)
            {
                for (var racerIndex2 = 0; racerIndex2 < racers.Count; racerIndex2++)
                {
                    var racer1 = racers[racerIndex1];
                    var racer2 = racers[racerIndex2];
                    if (racerIndex1 != racerIndex2)
                    {
                        if (racer1.IsCollidable() && racer2.IsCollidable() && racer1.CollidesWith(racer2))
                        {
                            OnRacerExplodes(racer1);
                            result.Add(racer1);
                            result.Add(racer2);
                        }
                    }
                }
            }

            return result;
        }

        private static void OnRacerExplodes(Racer racer)
        {
            // TODO
        }
    }
}