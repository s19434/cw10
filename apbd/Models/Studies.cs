using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace cw10.Models
{
    public partial class Studies
    {
        public Studies()
        {
            Enrollment = new HashSet<Enrollment>();
        }

        public int IdStudy { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
