using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;

public class TransactionDetail {
    public string Id { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string TransactionHeaderId { get; set; } = default!;
    public string Company { get; set; } = default!;
    public string Account { get; set; } = default!;
    public string Source { get; set; } = default!;
    public string Basis { get; set; } = default!;
    public string Amount { get; set; } = default!;
    public string DBCR { get; set; } = default!;

    public override string ToString() {
        return $"TRNDTL: Id[{Id}] | Desc[{Description}] | Comp[{Company}] | Acct[{Account}] | Basis[{Basis}] | Src[{Source}], Amt[{Amount}], [{DBCR}]";
    }
}

