using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilprog.Models
{
    public class BattleSquad
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int BattleId { get; set; }

        [Indexed]
        public int SquadId { get; set; }

        [NotNull]
        public string Side { get; set; }
    }
}
