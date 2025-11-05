using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilprog.Models
{
    public class Clone
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [NotNull]
        public string Name { get; set; }
        
        [NotNull]
        public string Rank { get; set; }
        
        [Indexed]
        public int Squad_id { get; set; }

        

    }
}
