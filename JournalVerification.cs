using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;

public class JournalVerification {
    /* 
     * The messages created when a check fails 
     */
    public static List<string> Messages = new List<string>();
    /*
     * This is the static method called and passed a single 
     * Journal at a time.
     */
    public static bool VerifyJournal(Journal journal) {
        Messages.Clear();
        var verified = true;
        verified &= VerifyDebitCreditMatch(journal);
        verified &= VerifyBalanceMatch(journal);

        return verified;
    }
    /*
     * This checks that the total of all JournalItem debits
     * matches the total of all JournalItem credits.
     * If they don't match, a message is generated.
     */
    private static bool VerifyDebitCreditMatch(Journal journal) {
        (decimal totalDebits, decimal totalCredits) = AccumulateDebitsCredits(journal);
        if(totalDebits != totalCredits) {
            var message = $"Journal Id: {journal.Id}; Debits: {totalDebits}; Credits: {totalCredits} do not balance!";
            Messages.Add(message);
            return false;
        }
        return true;
    }
    /*
     * This checks that the totals for the debits and credits match
     * the Journal total. If they don't match, a message is generated
     */
    private static bool VerifyBalanceMatch(Journal journal) {
        (decimal totalDebits, decimal totalCredits) = AccumulateDebitsCredits(journal);
        if(totalDebits != journal.Balance || totalCredits != journal.Balance) {
            var message = $"Journal Id: {journal.Id}; Debits: {totalDebits}; Credits: {totalCredits} do not match journal Balance: {journal.Balance}!";
            Messages.Add(message);
            return false;
        }
        return true;
    }
    /*
     * This is an internal method that accumulates the JournalItem
     * totals for debits and credits and return them as a tuple
     * containing both the totalDebits and totalCredits.
     */
    private static (decimal debits, decimal credits) AccumulateDebitsCredits(Journal journal) {
        var totalDebits = 0m;
        var totalCredits = 0m;
        foreach(var jitem in journal.Items) {
            totalDebits += jitem.Debit;
            totalCredits += jitem.Credit;
        }
        return (totalDebits, totalCredits);
    }
}
