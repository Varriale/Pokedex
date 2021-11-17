using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class PaginatedResource<T>
    {
        public int Count { get; set; }
        public List<NamedAPIResource<T>> Results { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public int limit { get
            {
                if (previous != null)
                    return int.Parse(previous.Split("limit=")[1]);
                if(next!=null)
                    return int.Parse(next.Split("limit=")[1]);
                return Count;
            }}
        public int totalPages
        {
            get
            {
                return (int)Math.Ceiling(((double)Count) / limit);
            }
        }
        public int page
        {
            get
            {
                if (previous != null)
                    return (int)Math.Ceiling(double.Parse(previous.Split("offset=")[1].Split("&")[0])/limit)+2;
                if (next != null)
                    return (int)Math.Ceiling(double.Parse(next.Split("offset=")[1].Split("&")[0]) / limit);
                return 1;
            }
        }

        public string GetLinkToPage(int page)
        {
            if (page <= totalPages && page>0)
            {
                if (previous != null)
                    return previous.Split("?")[0]+$"?offset={(page-1)*limit}&limit={limit}";
                if (next != null)
                    return next.Split("?")[0] + $"?offset={(page - 1) * limit}&limit={limit}";
            }
            return null;
        }
    }
}
