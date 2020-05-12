using System.Collections.Generic;

namespace Game
{
    internal class Main
    {
        internal void UpdateRacers(float deltaTimeS, List<Racer> racers)
        {
            UpdateAliveRacers(deltaTimeS, racers);

            var collidedRacers = GetCollidedRacers(racers);

            var aliveRacers = GetAliveRacers(racers, collidedRacers);

            DestroyRacers(racers, collidedRacers);

            // Builds the list of remaining racers
            CreateRacersList(racers, aliveRacers);
        }

        private static void CreateRacersList(List<Racer> racers, List<Racer> aliveRacers)
        {
            racers.Clear();

            for (var racerIndex = 0; racerIndex < aliveRacers.Count; racerIndex++)
            {
                racers.Add(aliveRacers[racerIndex]);

                aliveRacers.RemoveAt(0);
            }
        }

        private static void DestroyRacers(List<Racer> racers, List<Racer> racersNeedingRemoved)
        {
            for (var racerIndex = 0; racerIndex != racersNeedingRemoved.Count; racerIndex++)
            {
                if (racers.Contains(racersNeedingRemoved[racerIndex])) // Check we've not removed this already!
                {
                    racersNeedingRemoved[racerIndex].Destroy();
                    racers.Remove(racersNeedingRemoved[racerIndex]);
                }
            }
        }

        private static List<Racer> GetAliveRacers(IReadOnlyList<Racer> racers, List<Racer> racersNeedingRemoved)
        {
            var newRacerList = new List<Racer>();
            for (var racerIndex = 0; racerIndex != racers.Count; racerIndex++)
            {
                // check if this racer must be removed
                if (!racersNeedingRemoved.Contains(racers[racerIndex]))
                {
                    newRacerList.Add(racers[racerIndex]);
                }
            }

            return newRacerList;
        }

        private static void UpdateAliveRacers(float deltaTimeS, List<Racer> racers)
        {
            for (var racerIndex = 1; racerIndex <= 1000; racerIndex++)
            {
                if (racerIndex <= racers.Count)
                {
                    if (racers[racerIndex - 1].IsAlive())
                    {
                        //Racer update takes milliseconds
                        racers[racerIndex - 1].Update(deltaTimeS * 1000.0f);
                    }
                }
            }
        }

        private List<Racer> GetCollidedRacers(List<Racer> racers)
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

        private void OnRacerExplodes(Racer racer)
        {
            // TODO
        }
    }
}