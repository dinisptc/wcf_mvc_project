using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.ComponentModel.DataAnnotations;


namespace MvcTIC_IT.ViewModels
{
    public class EmpresaViewmodel
    {
        public int? Page { get; set; }

        public IPagedList<PortugalViewModel> SearchResults { get; set; }
        //public IPagedList<anuncio> SearchResults { get; set; }


        public int? id { get; set; }

        [Required(ErrorMessageResourceName = "email_required", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "email", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        [DataType(DataType.EmailAddress)]
        public string contacto { get; set; }

        [Required(ErrorMessageResourceName = "nome_da_empresa_required", ErrorMessageResourceType = typeof(Resources.Anuncios))]
        public string nome_da_empresa { get; set; }

        public string url { get; set; }
        public string logo { get; set; }
    }
}