using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;

public class ReferenceData {
    public string Id { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<TransactionHeader> TransactionHeaders { get; set; } = new List<TransactionHeader>();

    public override string ToString() {
        return $"REFDATA: Id[{Id}] | Desc[{Description}] | Trans Hdr Count[{TransactionHeaders.Count()}]";
    }
}
