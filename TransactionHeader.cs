using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;

public class TransactionHeader {
    public int Id { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int ReferenceDataId { get; set; } = default!;
    public List<TransactionDetail> TransactionDetails { get; set; } = new List<TransactionDetail>();


    public override string ToString() {
        return $"TRNHDR: Id[{Id}] | Desc[{Description}] | Trans Hdr Count[{TransactionDetails.Count()}]";
    }
}
