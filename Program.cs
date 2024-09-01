using static System.Console;
using check_journals;

WriteLine("Check Reference Data");

List<DataStore> RawReferenceDataList = new List<DataStore> {

    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH1", TransactionDetailId = "TD1",
        Company = "DSI", Account = "1234", Source = "Gerber", Amount = "100", DBCR = "D", Basis = "Cash"
    },

    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH1", TransactionDetailId = "TD2",
        Company = "DSI", Account = "1234", Source = "Gerber", Amount = "75", DBCR = "C", Basis = "Cash"
    },

    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH1", TransactionDetailId = "TD3",
        Company = "DSI", Account = "1234", Source = "Gerber", Amount = "25", DBCR = "C", Basis = "Cash"
    },

    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH2", TransactionDetailId = "TD4",
        Company = "PG", Account = "4321", Source = "PG", Amount = "250", DBCR = "D", Basis = "Common"
    },

    new DataStore {
        ReferenceDataId = "RD1", TransactionHeaderId = "TH2", TransactionDetailId = "TD5",
        Company = "PG", Account = "4321", Source = "PG", Amount = "250", DBCR = "C", Basis = "Common"
    }
};

DataStore.ExecuteDataImport(RawReferenceDataList);

foreach(var refData in DataStore.ReturnDataWithAllRelationships()) {

    var rc = ReferenceDataVerification.VerifyReferenceData(refData);
    
    if(!rc) {
        foreach(var msg in ReferenceDataVerification.Messages) {
            WriteLine($"MSG: {msg}");
        }
    }
}
