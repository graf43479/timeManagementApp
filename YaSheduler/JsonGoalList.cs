using System;
using System.Collections.Generic;
using System.Text;

namespace YaSheduler
{
    public class JsonGoalList
    {
        public string Key { get; set; }
        public string Token { get; set; }
        public DateTime UpdateDate { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
    }
}
