using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace check_journals;

public class TextInputReferenceData {
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

    public static List<TextInputReferenceData> ImportDataList(string fullpath) {
        var list = new List<TextInputReferenceData>();
        var lines = System.IO.File.ReadLines(fullpath);
        foreach(var line in lines) {
            // split the line
            var fields = line.Split('|');
            list.Add(new TextInputReferenceData {
                ReferenceDataId = fields[0],
                TransactionHeaderId = fields[1],
                TransactionDetailId = fields[2],
                Company = fields[3],
                Account = fields[4],
                Source = fields[5],
                Basis = fields[6],
                Amount = fields[7],
                DBCR = fields[8],
                Json = fields[9]
            });
        }
        return list;
    }
}
