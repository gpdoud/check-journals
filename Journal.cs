using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;
/*
 * The Id is the primary key
 * The Description is just some text
 * The Balance is the target total of the debits and credits
 * The Items is the collection of JournalItems for the Journal
 */
public class Journal {
    public int Id { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Balance { get; set; } = default!;
    public IEnumerable<JournalItem> Items { get; set; } = default!;
}
