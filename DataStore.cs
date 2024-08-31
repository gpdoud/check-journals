using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;

public class DataStore {
    public string ReferenceDataId { get; set; } = default!;
    public string TransactionHeaderId { get; set; } = default!;
    public string TransactionDetailId { get; set; } = default!;
    public string Company { get; set; } = default!;
    public string Account { get; set; } = default!;
    public string Source { get; set; } = default!;
    public string Basis { get; set; } = default!; // Cash,Common,Pgap,Gap
    public string Amount { get; set; } = default!;
    public string DBCR { get; set; } = default!;
    public string Json { get; set; } = default!;
    /// <summary>
    /// These three collection represent the SQL tables that will contain
    /// the results of importing the data
    /// </summary>
    public static List<ReferenceData> ReferenceDataList { get; set; } = new List<ReferenceData>();
    public static List<TransactionHeader> TransactionHeaderList { get; set; } = new List<TransactionHeader>();
    public static List<TransactionDetail> TransactionDetailList { get; set; } = new List<TransactionDetail>();

    public static List<string> Companies { get; set; } = new List<string> {
        "DSI", "PG", "Amazon" 
    };
    public static List<string> Accounts { get; set; } = new List<string> {
        "0000", "1111", "1234", "2222", "4321"
    };
    public static List<string> Sources { get; set; } = new List<string> {
        "Internal", "EXTERNAL", "gerber", "PG"
    };
    public static List<string> Bases { get; set; } = new List<string> {
        "Cash", "Common", "Pgap", "Gap"
    };


    public static void ExecuteDataImport(List<DataStore> RawReferenceDataList) {
        /*
         * These keep track of the last item added to the collection
         */
        var refDataId = "";
        var transHdrId = "";
        var transDtlId = "";
        /*
         * These clear the collections in case it is executed more than once
         */
        ReferenceDataList.Clear();
        TransactionHeaderList.Clear();
        TransactionDetailList.Clear();
        
        /****************************************************************************
         * Because each line of the imported data include a ReferenceData key, 
         * a TransactionHeader key, and a Transaction Detail key, this loop will 
         * check each of these key values to see if each is already in the collection
         * and, if not, it will be added.
         ****************************************************************************/
        foreach(var item in RawReferenceDataList) {
            // process ReferenceData
            if(!ReferenceDataList.Any(x => x.Id == item.ReferenceDataId)) {
                refDataId = item.ReferenceDataId;
                ReferenceData refData = new() { Id = item.ReferenceDataId, Description = "Ref Data" };
                ReferenceDataList.Add(refData);
            }
            // process TransactionHeader
            if(!TransactionHeaderList.Any(x => x.Id == item.TransactionHeaderId)) {
                transHdrId = item.TransactionHeaderId;
                TransactionHeader transHdr = new() {
                    Id = item.TransactionHeaderId, Description = "Trans Hdr", ReferenceDataId = refDataId
                };
                TransactionHeaderList.Add(transHdr);
            }
            // process TransactionDetail
            if(!TransactionDetailList.Any(x => x.Id == item.TransactionDetailId)) {
                transDtlId = item.TransactionHeaderId;
                TransactionDetail transDtl = new() {
                    Id = item.TransactionDetailId, Description = "Trans Dtl", 
                    Account = item.Account, Company = item.Company, Source = item.Source, Amount = item.Amount, DBCR = item.DBCR,
                    TransactionHeaderId = transHdrId
                };
                TransactionDetailList.Add(transDtl);
            }
        }
    }

}
