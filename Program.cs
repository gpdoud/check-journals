
using check_journals;

Console.WriteLine("Check Journals");
/*
 * Instead of create a SQL database, I just decide to build
 * the data in the app. The Init() method creates three Journal
 * instances and each has a few JournalItem instances
 */
var journalData = Init();
/*
 * This iterates through each of the Journals with the JournalItems
 * as a property of each Journal.
 */
foreach(var journal in journalData.Values) {
    /*
     * Pass each Journal to the VerifyJournal static method.
     * It will return True of the Journal passes all the checks
     * and False if any check fails. 
     */
    var rc = JournalVerification.VerifyJournal(journal);
    Console.WriteLine($"Journal Id {journal.Id} result is {(rc ? "Valid" : "Invalid")}");
    /*
     * If any check fails, the messages will be displayed on the console
     */
    if(!rc) {
        foreach(var msg in JournalVerification.Messages) {
            Console.WriteLine($"-- {msg}");
        }
    }
}
    
SortedList<int, Journal> Init() {

    var journals = new SortedList<int, Journal>();

    var j1 = new Journal {
        Id = 1, Description = "J1", Balance = 100, Items = new List<JournalItem> {
        new JournalItem { Id = 10, Debit = 100, Credit = 0, JournalId = 1 },
        new JournalItem { Id = 11, Debit = 0, Credit = 75, JournalId = 1 },
        new JournalItem { Id = 12, Debit = 0, Credit = 25, JournalId = 1 }
    },
    };
    var j2 = new Journal {
        Id = 2, Description = "J2", Balance = 500, Items = new List<JournalItem> {
        new JournalItem { Id = 20, Debit = 0, Credit = 100, JournalId = 2 },
        new JournalItem { Id = 21, Debit = 40, Credit = 0, JournalId = 2 },
        new JournalItem { Id = 22, Debit = 30, Credit = 0, JournalId = 2 },
        new JournalItem { Id = 23, Debit = 20, Credit = 0, JournalId = 2 },
        new JournalItem { Id = 24, Debit = 10, Credit = 0, JournalId = 2 }
    },
    };
    var j3 = new Journal {
        Id = 3, Description = "J3", Balance = 10, Items = new List<JournalItem> {
        new JournalItem { Id = 30, Debit = 10, Credit = 0, JournalId = 3 },
        new JournalItem { Id = 31, Debit = 0, Credit = 7, JournalId = 3 },
        new JournalItem { Id = 32, Debit = 0, Credit = 2, JournalId = 3 }
    },
    };
    journals.Add(j1.Id, j1);
    journals.Add(j2.Id, j2);
    journals.Add(j3.Id, j3);

    return journals;
}
