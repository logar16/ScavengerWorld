using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World
{
    public class AmbientEnvironment : ISteppable
    {
        public enum DayStatus { DAY, NIGHT }

        public enum Seasons { SPRING, SUMMER, FALL, WINTER }

        public DayStatus Status;

        public Seasons Season;

        private int StepsPerSeason;
        private int StepsPerDay;

        private int StepsIntoSeason;
        private int StepsIntoDay;

        public AmbientEnvironment(int stepsPerSeason, int stepsPerDay)
        {
            StepsPerSeason = stepsPerSeason;
            StepsPerDay = stepsPerDay;
        }

        /// <summary>
        /// The slow progression of time and the march of seasons
        /// </summary>
        /// <param name="timeStep">Assumes this is smaller than StepsPerSeason</param>
        public void Step(int timeStep)
        {
            StepsIntoSeason += timeStep;
            StepsIntoDay += timeStep;

            if (StepsIntoDay > StepsPerDay)
            {
                StepsIntoDay = (StepsIntoDay - StepsPerDay) % StepsPerDay;
                Status = (Status == DayStatus.DAY) ? DayStatus.NIGHT : DayStatus.DAY;
            }

            if (StepsIntoSeason > StepsPerSeason)
            {
                StepsIntoSeason -= StepsPerSeason; //Catches rollover minutes
                Season = (Seasons) ((int)Season + 1);
                if (Season == Seasons.WINTER)
                {
                    Log.Warning("Winter has arrived!");
                }
            }
        }
    }
}
