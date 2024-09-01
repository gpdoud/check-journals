using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

using static System.Console;

namespace check_journals;
/// <summary>
/// This class does all the verification that the imported data is valid
/// or displays when some data is not valid
/// </summary>
public class ReferenceDataVerification {
    /// <summary>
    /// Messages can be loaded from validation and are displayed after
    /// all the verification is complete
    /// </summary>
    public static List<string> Messages = new List<string>();
    /// <summary>
    /// This is the start of the verification. It starts with a single ReferenceData
    /// item that contain 1 or more TransactionHeaders and each of those with 2 or more 
    /// TransactionDetail items.
    /// 
    /// There is no data in the ReferenceData not the TransactionHeader items that 
    /// needs to be verified. All the verification is for the TransactionDetail items.
    /// </summary>
    /// <param name="referenceData">
    /// This data is a collection of the ReferenceData which includes all the 
    /// TransactionHeaders attached to the ReferenceData. The TransactionDetail items
    /// are attached to the TransactionHeader. It looks like this:
    /// 
    /// ReferenceData (processed one at a time)
    ///     TransactionHeader
    ///         TransactionDetail
    ///         TransactionDetail
    ///     TransactionHeader
    ///         TransactionDetail
    ///         TransactionDetail
    ///         TransactionDetail
    ///     TransactionHeader
    ///         TransactionDetail
    ///         TransactionDetail
    ///         TransactionDetail
    ///         TransactionDetail
    /// 
    /// The TransactionDetail contains: Company, Account, Source, Basic, Amount, Debit or Credit
    /// 
    /// The Company, Account, Source, Basic are all validated by checking the values
    /// against a database table.
    /// 
    /// The group of TransactionDetails within a TransactionHeader each have an Amount and
    /// an indicator of either Debit or Credit. The sum of the Debit amounts must match the
    /// sum of the Credit amounts.
    /// </param>
    /// <returns></returns>
    public static bool VerifyReferenceData(ReferenceData referenceData) {
        var refData = referenceData as ReferenceData;
        WriteLine(refData);
        var refDataIsValid = true;
        foreach(var transHdr in refData.TransactionHeaders) {
            refDataIsValid &= VerifyTransactionHeader(transHdr, refData.Id);
        }
        var isValid = refDataIsValid;
        var message = $"REFDATA: [{refData.Id}] is" + (isValid ? "" : " not") + " valid.";
        WriteLine(message);
        WriteLine("*********");
        return isValid;
    }
    private static bool VerifyTransactionHeader(TransactionHeader transHdr, string refDataId) {
        WriteLine($"-{transHdr}");
        var transHdrIsValid = true;
        // check validity of each TransactionDetail
        var transDtlIsValid = true;
        foreach(var transDtl in transHdr.TransactionDetails) {
            WriteLine($"--{transDtl}");
            var companyIsValid = VerifyCompany(transDtl.Company);
            var accountIsValid = VerifyAccount(transDtl.Account);
            var sourceIsValid = VerifySource(transDtl.Source);
            var basisIsValid = VerifyBasis(transDtl.Basis);
            transDtlIsValid &= companyIsValid && accountIsValid && sourceIsValid && basisIsValid;
        }
        transHdrIsValid &= transDtlIsValid;
        // balancing the debits and credits done only once per TransactionHeader
        var amountIsBalanced = VerifyDebitCreditBalance(transHdr.TransactionDetails, refDataId, transHdr.Id);
        var isValid = amountIsBalanced && transHdrIsValid;
        var message = $"-TRNHDR: [{transHdr.Id} | {transHdr.Description}] is" + (isValid ? "" : " not") + " valid.";
        WriteLine(message);
        return isValid;
    }

    private static bool VerifyDebitCreditBalance(List<TransactionDetail> transactionDetails, string refDataId, string transHdrId) {
        decimal debitCreditBalance = 0;
        foreach(var transDtl in transactionDetails) {
            switch(transDtl.DBCR) {
                case "D":
                    debitCreditBalance += Convert.ToDecimal(transDtl.Amount);
                    break;
                case "C":
                    debitCreditBalance -= Convert.ToDecimal(transDtl.Amount);
                    break;
            }
        }
        var isValid = debitCreditBalance == 0 ? true : false; 
        var message = $"--TRNDTL: DB/CR amounts" + (isValid ? "" : " do not") + " balance.";
        WriteLine(message);
        return isValid;
    }
    private static bool VerifyCompany(string company) {
        var isValid = DataStore.Companies.Any(x => x.ToUpper() == company.Trim().ToUpper());
        var message = $"---TRNDTL: Company [{company}] is" + (isValid ? "" : " not") + " valid.";
        WriteLine(message);
        return isValid;
    }
    private static bool VerifyAccount(string account) {
        var isValid = DataStore.Accounts.Any(x => x.ToLower() == account.ToLower());
        var message = $"---TRNDTL: Account [{account}] is" + (isValid ? "" : " not") + " valid.";
        WriteLine(message);
        return isValid;
    }
    private static bool VerifySource(string source) {
        var isValid = DataStore.Sources.Any(x => x.ToLower() == source.ToLower());
        var message = $"---TRNDTL: Source [{source}] is" + (isValid ? "" : " not") + " valid.";
        WriteLine(message);
        return isValid;
    }
    private static bool VerifyBasis(string basis) {
        var isValid = DataStore.Bases.Any(x => x.ToLower() == basis.ToLower());
        var message = $"---TRNDTL: Basis [{basis}] is" + (isValid ? "" : " not") + " valid.";
        WriteLine(message);
        return isValid;
    }
}
