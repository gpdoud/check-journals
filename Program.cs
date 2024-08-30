global using System.Diagnostics;
using check_journals;

Console.WriteLine("Check Reference Data");

var refDataList = Init();

foreach(var refData in refDataList.Values) {

    var rc = ReferenceDataVerification.VerifyReferenceData(refData);
    if(!rc) {
        foreach(var msg in ReferenceDataVerification.Messages) {
            Debug.WriteLine($"-- {msg}");
        }
    }
}
    
SortedList<int, ReferenceData> Init() {

    var refData = new SortedList<int, ReferenceData>();

    var rd1 = new ReferenceData {
        Id = 1, Description = "RD1", TransactionHeaders = new List<TransactionHeader> {
            new TransactionHeader {
                Id = 10, Description = "TH101", ReferenceDataId = 1, TransactionDetails = {
                    new TransactionDetail {
                        Id = 11, Description = "TD1011", Amount = 100, Company = "DSI", Account = "0000", Source = "INTERNAL",
                        DebitCredit = DebitCreditCode.Debit, TransactionHeaderId = 10
                    },
                    new TransactionDetail {
                        Id = 12, Description = "TD1012", Amount = 100, Company = "DSI", Account = "0000", Source = "INTERNAL",
                        DebitCredit = DebitCreditCode.Credit, TransactionHeaderId = 10
                    }
                }
            },
            new TransactionHeader {
                Id = 11, Description = "TH111", ReferenceDataId = 1, TransactionDetails = {
                    new TransactionDetail {
                        Id = 21, Description = "TD1101", Amount = 50, Company = "DSI", Account = "1111", Source = "INTERNAL",
                        DebitCredit = DebitCreditCode.Debit, TransactionHeaderId = 11
                    },
                    new TransactionDetail {
                        Id = 22, Description = "TD1102", Amount = 50, Company = "DSI", Account = "2222", Source = "INTERNAL",
                        DebitCredit = DebitCreditCode.Credit, TransactionHeaderId = 11
                    }
                }
            }
        },
    };
    var rd2 = new ReferenceData {
        Id = 2, Description = "RD2", TransactionHeaders = new List<TransactionHeader> {
            new TransactionHeader { 
                Id = 20, Description = "TH2", ReferenceDataId = 2, TransactionDetails = {
                    new TransactionDetail { 
                        Id = 31, Description = "TD21", Amount = 100, Company = "DSI", Account = "0000", Source = "INTERNAL",
                        DebitCredit = DebitCreditCode.Debit, TransactionHeaderId = 20 
                    },
                    new TransactionDetail { 
                        Id = 32, Description = "TD22", Amount = 101, Company = "DSIx", Account = "0001", Source = "Cool Bar",
                        DebitCredit = DebitCreditCode.Credit, TransactionHeaderId = 20 
                    }
                }
            }
        },
    };
    refData.Add(rd1.Id, rd1);
    refData.Add(rd2.Id, rd2);

    return refData;
}
