using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

using static System.Diagnostics.Debug;

namespace check_journals;

public class ReferenceDataVerification {

    public static List<string> Messages = new List<string>();

    public static bool VerifyReferenceData(ReferenceData referenceData) {
        var refData = referenceData as ReferenceData;
        Debug.WriteLine($"Reference Data: {refData}");
        var refDataIsValid = true;
        foreach(var transHdr in refData.TransactionHeaders) {
            refDataIsValid &= VerifyTransactionHeader(transHdr, refData.Id);
        }
        var isValid = refDataIsValid;
        var message = $"Ref Data id: [{refData.Id}] is" + (isValid ? "" : " not") + " valid.";
        Debug.WriteLine(message);
        return isValid;
    }
    private static bool VerifyTransactionHeader(TransactionHeader transHdr, int refDataId) {
        Debug.WriteLine($"-Trans Header: {transHdr}");
        var transHdrIsValid = true;
        // check validity of each TransactionDetail
        var transDtlIsValid = true;
        foreach(var transDtl in transHdr.TransactionDetails) {
            Debug.WriteLine($"--Trans Dtl: {transDtl}");
            var companyIsValid = VerifyCompany(transDtl.Company);
            var accountIsValid = VerifyAccount(transDtl.Account);
            var sourceIsValid = VerifySource(transDtl.Source);
            transDtlIsValid &= companyIsValid && accountIsValid && sourceIsValid;
        }
        // balancing the debits and credits done only once per TransactionHeader
        var amountIsBalanced = VerifyDebitCreditBalance(transHdr.TransactionDetails, refDataId, transHdr.Id);
        var isValid = amountIsBalanced && transHdrIsValid;
        var message = $"-Trans Hdr id: [{transHdr.Id} | {transHdr.Description}] is" + (isValid ? "" : " not") + " valid.";
        Debug.WriteLine(message);
        return isValid;
    }

    private static bool VerifyDebitCreditBalance(List<TransactionDetail> transactionDetails, int refDataId, int transHdrId) {
        decimal debitCreditBalance = 0;
        foreach(var transDtl in transactionDetails) {
            switch(transDtl.DebitCredit) {
                case DebitCreditCode.Debit:
                    debitCreditBalance += transDtl.Amount;
                    break;
                case DebitCreditCode.Credit:
                    debitCreditBalance -= transDtl.Amount;
                    break;
            }
        }
        var isValid = debitCreditBalance == 0 ? true : false; 
        var message = $"--Trans Dtl DB/CR amounts" + (isValid ? "" : " do not") + " balance.";
        Debug.WriteLine(message);
        return isValid;
    }
    private static bool VerifyCompany(string company) {
        var companies = new List<string> { "dsi", "pg", "amazon" };
        var isValid = companies.Contains(company.ToLower());
        var message = $"---Company [{company}] is" + (isValid ? "" : " not") + " valid.";
        Debug.WriteLine(message);
        return isValid;
    }
    private static bool VerifyAccount(string account) {
        var accounts = new List<string> { "0000", "1111", "2222" };
        var isValid = accounts.Contains(account);
        var message = $"---Account [{account}] is" + (isValid ? "" : " not") + " valid.";
        Debug.WriteLine(message);
        return isValid;
    }
    private static bool VerifySource(string source) {
        var sources = new List<string> { "internal", "external", "gerber" };
        var isValid = sources.Contains(source.ToLower());
        var message = $"---Source [{source}] is" + (isValid ? "" : " not") + " valid.";
        Debug.WriteLine(message);
        return isValid;
    }
}
