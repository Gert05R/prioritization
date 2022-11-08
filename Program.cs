// Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Collections.Generic;
//https://joshclose.github.io/CsvHelper/getting-started/
//https://github.com/JoshClose/CsvHelper/issues/1201


internal class Program
{

    public class Score 
{
    public string Intake { get; set; }
    public int Sum { get; set; }

    public int counter { get; set; }

    public int quotation { get; set; }

    public Score (string intake, int score, int count) 
    {
        this.Intake = intake;
        this.Sum = score;
        this.counter = count;
    }
    public void setQuotation() 
    {
        this.quotation = (this.Sum / this.counter);
    }


    public Score GetScore()
    {
            return this;
    }

    public  string GetIntake()
    {
        return this.Intake;
    }

    /*public  void AddIntakeScore(string score)
    {
        int convert = Convert.ToInt32(score);
        int current = this.Sum + convert;
    }*/

    public int GetSum() 
    {
        return this.Sum;
    }

    public void SetScore(int newScore) 
    {
        this.Sum = this.Sum + newScore;
    }
}

public static int verify(string input) 
{
    if (!string.IsNullOrEmpty(input) ) 
    {
        return Convert.ToInt32(input);
    }
    else {return 0;}
}


// Program.cs
// The Main() method
// Program.cs
public static void Main(string[] args)
    {
        //List<Score> prios = new List<Score>();
        List<Score> scores = new List<Score>();

        var badRecord = new List<string>();
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            Comment = '#',
            BadDataFound = context => badRecord.Add(context.RawRecord),
            //PrepareHeaderForMatch = args => args.Header.
        TrimOptions = TrimOptions.Trim,
            MissingFieldFound = null
            
        };

        string [] inputFiles = 
        {
           "Assets/Prioritization_2023_Intake_iLABs_CART RAR.csv",
            "Assets/Prioritization_2023_Intake_iLABs_CorkQC.csv",
            "Assets/Copia di prioritization 2023 intake ILABs.csv",
            "Assets/Prioritization_2023_Intake_iLABs_Cork_LM_DPDS.csv",
            "Assets/Prioritization_2023_Intake_iLABs_Fuji.csv",
             "Assets/Prioritization_2023_Intake_iLABs_Geel.csv",
              "Assets/Prioritization_2023_Intake_iLABs_Incheon.csv",
               "Assets/Prioritization_2023_Intake_iLABs_LEIandHIGI.csv",
                "Assets/Prioritization_2023_Intake_iLABs_Puebla.csv",
                 "Assets/Prioritization_2023_Intake_iLABs_Raritan.csv",
                  "Assets/Prioritization_2023_Intake_iLABs_Schaffhausen.csv",
                   "Assets/Prioritization_2023_Intake_iLABs_Titusville.csv",
                    "Assets/Prioritization_2023_Intake_iLABs_Xian.csv",
                    "Assets/Prioritization_2023_Intake_iLABs_Beerse.csv"

        };

        string [] Sites = 
        {
           "CAR-T Rar (Niyati Shah)",
            "CarT",
            "CorkApi"

        };
        

        foreach (string file in inputFiles) {

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, configuration))
            {
            csv.Read();
            csv.ReadHeader();
            /*string[] headerRow = csv.HeaderRecord;
            foreach (string header in headerRow) 
            {
                if (header.Equals(Sites[0]))
                Console.WriteLine("headder " + header);
            }*/
                
            csv.Context.RegisterClassMap<FileMap>();
            var records = csv.GetRecords<sourceFile>();
            foreach (var rec in records)
                {
                    
                    int current = 
                    verify(rec.CorkApiDpds) + 
                    verify(rec.CorkChem) +
                    verify(rec.Fuji) +
                    verify(rec.Geel) + 
                    verify(rec.Gurabo) + 
                    verify(rec.Higi) + 
                    verify(rec.Incheon) + 
                    verify(rec.Latina) + 
                    verify(rec.Leiden) + 
                    verify(rec.Puebla) +
                    verify(rec.Raritan) +
                    verify(rec.Schaffhaussen) +
                    verify(rec.Titus) +
                    verify(rec.Xian) +     
                    verify(rec.CorkApi) + 
                    verify(rec.CarT) + 
                    verify(rec.Athens) +
                    verify(rec.Beerse)
                    ;

                    
                    //int total = 32;
                    //Console.WriteLine("total " + total);

                    if (scores.Exists(x => x.Intake.Equals(rec.Id)) && !string.IsNullOrEmpty(rec.Id)) 
                    {
                        var Query = 
                        from score in scores
                        where score.Intake.Equals(rec.Id)
                        select score;

                        foreach (var Score in Query) 
                        {
                            
                            Score.Sum = Score.Sum + current;
                            if (current >0) {
                                Score.counter = Score.counter +1;
                            }
                            //Console.WriteLine("Score= " + Score.Sum);
                        }

                        //foreach ( var score in scores.Where(x => x.Intake.Equals(rec.Id)).Select(x => x.Sum)) 
                        //{
                          //  score = verify(rec.CorkApi) + verify(rec.CarT);
                        //}
                    }

                    else {
                        if (!string.IsNullOrEmpty(rec.Id)) {
                            int count = 1;
                            
                            scores.Add( new Score(rec.Id, current, count));
                        }
                    }

                    
                    

                    
                    
                    
                    /*if (string.IsNullOrEmpty(rec.Id) || string.IsNullOrEmpty(rec.CarT) ||
                        string.IsNullOrEmpty(rec.CorkApi)
                    ) 
                    {
                        continue;
                    }
                    else {
                        var Query = 
                        from line in scores
                        where line.Intake.Exists
                        select line.Sum;

                        scores.Add(new Score(rec.Id, rec.CarT));
                        scores.Add(new Score(rec.Id, rec.CorkApi));
                        }

                    if (scores.Exists(x => x.Intake == rec.Id)) 
                    {
                        var Query = 
                        from line in scores
                        where line.Intake == rec.Id
                        select line.Sum;

                        foreach (var result in Query) 
                        {
                            Console.WriteLine("result " + result);
                        }

                    }
                    


                    
                    //Console.WriteLine("ID " + rec.Id);
                    //Console.WriteLine("CarT " + rec.CarT);
                    //current = current + Convert.ToInt32(rec.CarT);
                    //Console.WriteLine("current= " +current);


                  


                    /*
                    if (prios.Exists(x => x.PartName == rec.Id)) 
                    {
                        Score current = prios.Find(x => x.PartName == rec.Id);
                        current.AddValue(rec.CarT);
                    }
                    else {
                        prios.Add(new Score() {PartName= rec.Id, PartId= rec.CarT});
                    }*/
                   
                    

                }
        }
    
        }

        List<Score> results = new List<Score>();
        foreach (var Score in scores) {
            Score.setQuotation();
        }
        
        var Query2 = 
                        from score in scores
                        orderby score.quotation, score.Sum, score.counter
                        select score;
        
        foreach (var Score in Query2) 
            {
                Score.setQuotation();
                results.Add(Score);
                
                //Console.WriteLine("ID " + Score.Intake);
                //Console.WriteLine("Sum= " + Score.Sum);

            }
        using (var writer = new StreamWriter("Assets/Scores.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(results);
            writer.Flush();
            //https://www.e-iceblue.com/Tutorials/Spire.XLS/Spire.XLS-Program-Guide/CSV-to-Excel-Convert-CSV-to-Excel-with-C-VB.NET.html
        }

        
    }



public class sourceFile
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    //public string? Description { get; set; }
    public string? Leiden { get; set; }
    public string? Schaffhaussen { get; set; }
    public string? CorkApi { get; set; }
    public string? CorkApiDpds { get; set; }
    public string? CarT { get; set; }
    public string? Beerse { get; set; }
    public string? Geel { get; set; }
    public string? CorkChem { get; set; }
    public string? Latina { get; set; }
    public string? Xian { get; set; }
    public string? Higi { get; set; }
    public string? Incheon { get; set; }
    public string? Fuji { get; set; }
    public string? Titus { get; set; }
    public string? Raritan { get; set; }
    public string? Gurabo { get; set; }
    public string? Puebla { get; set; }
    public string? Athens { get; set; }
    
}



public class FileMap : ClassMap<sourceFile>
{
    public FileMap()
    {
        Map(m => m.Id).Name("Intake number");
        Map(p => p.Title).Name("Short Title");
        //Map(p => p.Description).Name("Short description Intake");
        Map(p => p.Leiden).Name("Leiden_API-L (Genesis Martina)");
        Map(p => p.Schaffhaussen).Name("Schaffhaussen (Anna Peinhopf)");
        Map(p => p.CorkApi).Name("Cork-API_L (Alan Crowley)");
        Map(p => p.CorkApiDpds).Name("Cork_API-L DPDS (Liam O'Connor)");
        Map(p => p.CarT).Name("CAR-T Rar (Niyati Shah)");
        Map(p => p.Beerse).Name("Beerse (Jef Kennis)");
        Map(p => p.Geel).Name("Geel (Heidi Zengers + Sieben Vinkx)");
        Map(p => p.CorkChem)?.Name("Cork Chem\n(Eoin O'Donovan)");
        Map(p => p.Latina).Name("Latina (Valentina Paglia)");
        Map(p => p.Xian)?.Name("Xian \n(Zhang Lianying, Yongtao Lei)");
        Map(p => p.Higi).Name("Higi\n (Chandrakant Jagtap)");
        Map(p => p.Incheon)?.Name("Incheon\n (DongReuk Lee, SangAh Park)");
        Map(p => p.Fuji).Name("Fuji\n(Akemi Nishimura)");
        Map(p => p.Titus).Name("Titusville (Alicia Durff)");
        Map(p => p.Raritan).Name("Raritan (Kristen Rodgers-erickson)");
        Map(p => p.Gurabo).Name("Gurabo\n(Maria Solla)");
        Map(p => p.Puebla).Name("Puebla (Daniel Santos)");
        Map(p => p.Athens).Name("Athens\n (Lisa Padilla)");
    }
}



/*public class Score : IEquatable<Score>
{
    public int? PartName { get; set; }
    public int PartId { get; set; }

    public Score () 
    {
        
    }

    public Score (int name, int id) 
    {
        this.PartName = name;
        this.PartId = id;
    }

    public override string ToString()
    {
        return "ID: " + PartId + "   Name: " + PartName;
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Score objAsPart = obj as Score;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }
    public override int GetHashCode()
    {
        return PartId;
    }
    public  int GetId(int PartName)
    {
        return this.PartId;
    }
    public  int AddValue(int value)
    {
        return this.PartId= this.PartId + value;
    }
    public bool Equals(Score other)
    {
        if (other == null) return false;
        return (this.PartId.Equals(other.PartId));
    }
    // Should also override == and != operators.
}*/

//Try A dictionary???

}