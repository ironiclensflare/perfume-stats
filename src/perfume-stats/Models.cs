namespace Perfume.Models
{
    public class PerfumeDbItem
    {
        public IntRecord Size { get; set; }
        public StringRecord Name { get; set; }
        public StringRecord Id { get; set; }
        public StringRecord House { get; set; }
        public ArrayRecord Wearings { get; set; }
    }

    public class StringRecord
    {
        public string S { get; set; }
    }

    public class IntRecord
    {
        public string N { get; set; }
    }

    public class ArrayRecord
    {
        public string[] Ss { get; set; }
    }

    public class Perfume
    {
        public Perfume(PerfumeDbItem dbItem)
        {
            Name = dbItem.Name.S;
            Size = Int32.Parse(dbItem.Size.N);
            Id = dbItem.Id.S;
            House = dbItem.House.S;
            Wearings = dbItem.Wearings?.Ss.Select(d => DateTime.Parse(d)).ToList();
        }

        public string Name { get; set; }
        public int Size { get; set; }
        public string Id { get; set; }
        public string House { get; set; }
        public List<DateTime> Wearings { get; set; }
    }
}