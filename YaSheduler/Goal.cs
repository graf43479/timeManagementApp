using System;
using System.Collections.Generic;
using System.Text;

namespace YaSheduler
{
    public class Goal
    {
        public int GoalID { get; set; }
        public GoalTypes GoalType  { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }

    public enum GoalTypes
    {
        ImportantUrgent,
        ImportantNotUrgent,
        NotImportantUrgent,
        NotImportantNotUrgent,
    }
}
