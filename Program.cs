using static System.Console;
using check_journals;

Console.WriteLine("Check Reference Data");

List<DataStore> RawReferenceDataList = new List<DataStore> {
    /*
        * RefData 1, TransHdr 1, TransDtl 1 
        */
    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH1", TransactionDetailId = "TD1",
        Company = "DSI", Account = "1234", Source = "Gerber", Amount = "100", DBCR = "D", Basis = "Cash"
    },
    /*
        * RefData 1, TransHdr 1, TransDtl 2 
        */
    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH1", TransactionDetailId = "TD2",
        Company = "DSI", Account = "1234", Source = "Gerber", Amount = "75", DBCR = "C", Basis = "Cash"
    },
    /*
        * RefData 1, TransHdr 1, TransDtl 3 
        */
    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH1", TransactionDetailId = "TD3",
        Company = "DSI", Account = "1234", Source = "Gerber", Amount = "25", DBCR = "C", Basis = "Cash"
    },
    /*
        * RefData 1, TransHdr 2, TransDtl 4
        */
    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH2", TransactionDetailId = "TD4",
        Company = "PG", Account = "4321", Source = "PG", Amount = "250", DBCR = "D", Basis = "Common"
    },
    /*
        * RefData 1, TransHdr 2, TransDtl 5
        */
    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH2", TransactionDetailId = "TD5",
        Company = "PG", Account = "4321", Source = "PG", Amount = "250", DBCR = "C", Basis = "Common"
    }
};

DataStore.ExecuteDataImport(RawReferenceDataList);

var refDataList = DataStore.ReferenceDataList.ToList();
foreach(var refData in refDataList) {
    refData.TransactionHeaders = DataStore.TransactionHeaderList
        .Where(x => x.ReferenceDataId == refData.Id).ToList();
    foreach(var transHdr in refData.TransactionHeaders) {
        transHdr.TransactionDetails = DataStore.TransactionDetailList
            .Where(x => x.TransactionHeaderId == transHdr.Id).ToList();
    }
}

foreach(var refData in refDataList) {
    var rc = ReferenceDataVerification.VerifyReferenceData(refData);
    if(!rc) {
        foreach(var msg in ReferenceDataVerification.Messages) {
            WriteLine($"MSG: {msg}");
        }
    }
}
