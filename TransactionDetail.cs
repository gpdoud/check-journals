using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;

public class TransactionDetail {
    public int Id { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int TransactionHeaderId { get; set; } = default!;
    public string Company { get; set; } = default!;
    public string Account { get; set; } = default!;
    public string Source { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public DebitCreditCode DebitCredit { get; set; } = default!;

    public override string ToString() {
        return $"Id[{Id}] | Desc[{Description}] | Comp[{Company}] | Acct[{Account}] | Src[{Source}], Amt[{Amount}], [{DebitCredit.ToString()}]";
    }
}

public enum DebitCreditCode { Debit, Credit }
