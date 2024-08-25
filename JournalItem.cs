using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;
/*
 * The Id is the primary key
 * The JournalId is the foreign key to the Journal
 * The Debit and Credit are the amounts for each though
 *  only one of each should have a non-zero value
 */
public class JournalItem {
    public int Id { get; set; } = default!;
    public int JournalId { get; set; } = default!;
    public decimal Debit { get; set; } = default!;
    public decimal Credit { get; set; } = default!;
}
