using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;

public record TransactionDetailResults {
    public int ReferenceDataId { get; set; } = default!;
    public int TransactionHeaderId { get; set; } = default!;
    public int TransactionDetailId { get; set; } = default!;
    public bool IsDebitCreditBalanced { get; set; } = default!;
    public bool IsCompanyVerified { get; set; } = default!;
    public bool IsAccountVerified { get; set; } = default!;
    public bool IsSourceVerified { get; set; } = default!;
    public bool AreCompanyAccountSourceAllVerified { get; set; } = default!;
}
