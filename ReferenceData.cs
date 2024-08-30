using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;

public class ReferenceData {
    public int Id { get; set; } = default!;
    public string Description { get; set; } = default!;
    public IEnumerable<TransactionHeader> TransactionHeaders { get; set; } = default!;

    public override string ToString() {
        return $"REFDATA: Id[{Id}] | Desc[{Description}] | Trans Hdr Count[{TransactionHeaders.Count()}]";
    }
}
