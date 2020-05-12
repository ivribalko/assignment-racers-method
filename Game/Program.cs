using System;
using System.Collections.Generic;

namespace Game
{
    internal class Program
    {
        internal void UpdateRacers(float deltaTimeS, List<Racer> racers)
        {
            var racersNeedingRemoved = new List<Racer>();
            racersNeedingRemoved.Clear();

            UpdateAliveRacers(deltaTimeS, racers);

            PopulateCollidedRacers(racers, racersNeedingRemoved);

            var newRacerList = GetAliveRacers(racers, racersNeedingRemoved);

            DestroyRacers(racers, racersNeedingRemoved);

            // Builds the list of remaining racers
            CreateRacersList(racers, newRacerList);
        }

        private static void CreateRacersList(List<Racer> racers, List<Racer> newRacerList)
        {
            racers.Clear();

            for (var racerIndex = 0; racerIndex < newRacerList.Count; racerIndex++)
            {
                racers.Add(newRacerList[racerIndex]);
            }

            for (var racerIndex = 0; racerIndex < newRacerList.Count; racerIndex++)
            {
                newRacerList.RemoveAt(0);
            }
        }

        private static void DestroyRacers(List<Racer> racers, List<Racer> racersNeedingRemoved)
        {
            for (var racerIndex = 0; racerIndex != racersNeedingRemoved.Count; racerIndex++)
            {
                var foundRacerIndex = racers.IndexOf(racersNeedingRemoved[racerIndex]);
                if (foundRacerIndex >= 0) // Check we've not removed this already!
                {
                    racersNeedingRemoved[racerIndex].Destroy();
                    racers.Remove(racersNeedingRemoved[racerIndex]);
                }
            }
        }

        private static List<Racer> GetAliveRacers(List<Racer> racers, List<Racer> racersNeedingRemoved)
        {
            var newRacerList = new List<Racer>();
            for (var racerIndex = 0; racerIndex != racers.Count; racerIndex++)
            {
                // check if this racer must be removed
                if (racersNeedingRemoved.IndexOf(racers[racerIndex]) < 0)
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

        private void PopulateCollidedRacers(List<Racer> racers, List<Racer> racersNeedingRemoved)
        {
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
                            racersNeedingRemoved.Add(racer1);
                            racersNeedingRemoved.Add(racer2);
                        }
                    }
                }
            }
        }

        private void OnRacerExplodes(Racer racer)
        {
            throw new NotImplementedException();
        }
    }
}