using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace WcfITPortugal.Viewmodels
{
    public class PortugalViewModel
    {

        public int? Page { get; set; }

        public IPagedList<PortugalViewModel> SearchResults { get; set; }
        //public IPagedList<anuncio> SearchResults { get; set; }


        public int? id { get; set; }

        public string email { get; set; }

 
        public string local_de_trabalho { get; set; }


        public string salario { get; set; }

        public int empresa_id { get; set; }


        public string titulo { get; set; }

    
        public string descricao { get; set; }

        public string identidade { get; set; }

        public int idioma_id { get; set; }

        public int anuncio_id { get; set; }

        public int numvisitas { get; set; }

        public DateTime dataanuncio { get; set; }
    }
}